using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using biznus_web.Models;
using System.Security.Claims;
using System.Security.Cryptography;
using Microsoft.AspNetCore.Localization;
using Microsoft.Extensions.Localization;
using System.Text;

namespace biznus_web.Controllers
{
    public class AccountController : Controller
    {
        private readonly ILogger<AccountController> _logger;
        private static readonly Dictionary<string, AppUser> _users = new Dictionary<string, AppUser>();


        public AccountController(ILogger<AccountController> logger)
        {
            _logger = logger;

            SeedTestUsers();
        }

        [HttpGet]
        public IActionResult Login(string? returnUrl = null)
        {
            _logger.LogInformation("User accessed login page. ReturnUrl: {ReturnUrl}", returnUrl);
            ViewData["Title"] = "Login";
            var model = new LoginViewModel { ReturnUrl = returnUrl };
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            _logger.LogInformation("Login attempt for email: {Email}", model.Email);


            if (!ModelState.IsValid)
            {
                _logger.LogWarning("Login failed - invalid model state for email: {Email}", model.Email);
                return View(model);
            }

            var user = ValidateUser(model.Email, model.Password);
            if (user == null)
            {
                _logger.LogWarning("Login failed - invalid credentials for email: {Email}", model.Email);
                ModelState.AddModelError(string.Empty, "Invalid email or password.");
                return View(model);
            }

            // Создаем claims для аутентификации
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id),
                new Claim(ClaimTypes.Name, user.FullName),
                new Claim(ClaimTypes.Email, user.Email),
                new Claim("FirstName", user.FirstName),
                new Claim("LastName", user.LastName)
            };

            var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
            var claimsPrincipal = new ClaimsPrincipal(claimsIdentity);

            // Настраиваем cookie
            var authProperties = new Microsoft.AspNetCore.Authentication.AuthenticationProperties
            {
                IsPersistent = model.RememberMe,
                ExpiresUtc = model.RememberMe ? DateTime.UtcNow.AddDays(30) : DateTime.UtcNow.AddHours(24)
            };

            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, claimsPrincipal, authProperties);

            _logger.LogInformation("User {Email} successfully logged in", user.Email);

            if (!string.IsNullOrEmpty(model.ReturnUrl) && Url.IsLocalUrl(model.ReturnUrl))
            {
                _logger.LogInformation("Redirecting user {Email} to {ReturnUrl}", user.Email, model.ReturnUrl);
                return Redirect(model.ReturnUrl);
            }

            return RedirectToAction("Index", "Home");
        }

        [HttpGet]
        public IActionResult Register()
        {
            _logger.LogInformation("User accessed registration page");
            ViewData["Title"] = "Register";
            var model = new RegisterViewModel();
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            _logger.LogInformation("Registration attempt for email: {Email}", model.Email);
            
            if (!ModelState.IsValid)
            {
                _logger.LogWarning("Registration failed - invalid model state for email: {Email}", model.Email);
                return View(model);
            }

            if (!model.AgreeToTerms)
            {
                _logger.LogWarning("Registration failed - terms not agreed for email: {Email}", model.Email);
                ModelState.AddModelError(nameof(model.AgreeToTerms), "You must agree to the Terms and Conditions.");
                return View(model);
            }

            if (IsEmailExists(model.Email))
            {
                _logger.LogWarning("Registration failed - email already exists: {Email}", model.Email);
                ModelState.AddModelError(nameof(model.Email), "Email already exists.");
                return View(model);
            }

            var user = CreateUser(model);
            if (user == null)
            {
                _logger.LogError("Registration failed - unable to create user for email: {Email}", model.Email);
                ModelState.AddModelError(string.Empty, "Failed to create user account.");
                return View(model);
            }

            _logger.LogInformation("New user registered successfully: {Email}", model.Email);

            // Автоматически входим пользователя после регистрации
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id),
                new Claim(ClaimTypes.Name, user.FullName),
                new Claim(ClaimTypes.Email, user.Email),
                new Claim("FirstName", user.FirstName),
                new Claim("LastName", user.LastName)
            };

            var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
            var claimsPrincipal = new ClaimsPrincipal(claimsIdentity);

            var authProperties = new Microsoft.AspNetCore.Authentication.AuthenticationProperties
            {
                IsPersistent = true,
                ExpiresUtc = DateTime.UtcNow.AddDays(30)
            };

            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, claimsPrincipal, authProperties);

            return RedirectToAction("Index", "Home");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Logout()
        {
            var userEmail = User.FindFirst(ClaimTypes.Email)?.Value ?? "Unknown";
            _logger.LogInformation("User {Email} logging out", userEmail);
            
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            
            _logger.LogInformation("User {Email} successfully logged out", userEmail);
            return RedirectToAction("Index", "Home");
        }

        [HttpGet]
        public IActionResult Profile()
        {
            if (!User.Identity?.IsAuthenticated ?? true)
            {
                _logger.LogWarning("Unauthenticated user attempted to access profile page");
                return RedirectToAction("Login");
            }

            var userEmail = User.FindFirst(ClaimTypes.Email)?.Value ?? "Unknown";
            _logger.LogInformation("User {Email} accessed profile page", userEmail);
            
            ViewData["Title"] = "Profile";
            return View();
        }

        // Приватные методы для работы с пользователями
        private void SeedTestUsers()
        {
            if (!_users.Any())
            {
                var testUser1 = new AppUser
                {
                    Id = "test-user-1",
                    FirstName = "John",
                    LastName = "Doe",
                    Email = "test@example.com",
                    UserName = "test@example.com",
                    PasswordHash = HashPassword("password123"),
                    CreatedAt = DateTime.UtcNow,
                    IsActive = true
                };
                _users[testUser1.Id] = testUser1;

                var testUser2 = new AppUser
                {
                    Id = "test-user-2",
                    FirstName = "Jane",
                    LastName = "Smith",
                    Email = "jane@example.com",
                    UserName = "jane@example.com",
                    PasswordHash = HashPassword("test123"),
                    CreatedAt = DateTime.UtcNow,
                    IsActive = true
                };
                _users[testUser2.Id] = testUser2;

                _logger.LogInformation("Test users created: test@example.com and jane@example.com");
            }
        }

        private AppUser? ValidateUser(string email, string password)
        {
            var user = _users.Values.FirstOrDefault(u => u.Email.Equals(email, StringComparison.OrdinalIgnoreCase));
            if (user != null && VerifyPassword(password, user.PasswordHash))
            {
                return user;
            }
            return null;
        }

        private bool IsEmailExists(string email)
        {
            return _users.Values.Any(u => u.Email.Equals(email, StringComparison.OrdinalIgnoreCase));
        }

        private AppUser? CreateUser(RegisterViewModel model)
        {
            var user = new AppUser
            {
                Id = Guid.NewGuid().ToString(),
                FirstName = model.FirstName,
                LastName = model.LastName,
                Email = model.Email,
                UserName = model.Email,
                PasswordHash = HashPassword(model.Password),
                CreatedAt = DateTime.UtcNow,
                IsActive = true
            };

            _users[user.Id] = user;
            return user;
        }

        private string HashPassword(string password)
        {
            using var sha256 = SHA256.Create();
            var hashedBytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(password + "salt"));
            return BitConverter.ToString(hashedBytes).Replace("-", "").ToLower();
        }

        private bool VerifyPassword(string password, string hash)
        {
            return HashPassword(password) == hash;
        }
        public JsonResult Cookie(string culture)
        {
            Response.Cookies
                .Append(

                CookieRequestCultureProvider.DefaultCookieName,
                CookieRequestCultureProvider.MakeCookieValue(new RequestCulture(culture)),
                new CookieOptions { Expires = DateTimeOffset.Now.AddHours(1) }
                );
            return Json(culture);
        }
    }
}