using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using biznus_web.Models;
using biznus_web.Models.DTOs;
using biznus_web.Services;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace biznus_web.Controllers.Api
{
    [ApiController]
    [Route("api/[controller]")]
    public class ApiAuthController : ControllerBase
    {
        private readonly ApplicationDbContext _db;
        private readonly JwtService _jwtService;
        private readonly ILogger<ApiAuthController> _logger;

        public ApiAuthController(
            ApplicationDbContext db,
            JwtService jwtService,
            ILogger<ApiAuthController> logger)
        {
            _db = db;
            _jwtService = jwtService;
            _logger = logger;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequest request)
        {
            _logger.LogInformation("API login attempt for email: {Email}", request.Email);

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var user = await _db.Users
                .FirstOrDefaultAsync(u => u.Email == request.Email && u.IsActive);

            if (user == null || !VerifyPassword(request.Password, user.PasswordHash))
            {
                _logger.LogWarning("API login failed - invalid credentials for email: {Email}", request.Email);
                return Unauthorized(new { message = "Invalid email or password" });
            }

            // Обновляем время последнего входа
            user.LastLoginAt = DateTime.UtcNow;
            await _db.SaveChangesAsync();

            var token = _jwtService.GenerateToken(user);
            var refreshToken = _jwtService.GenerateRefreshToken();

            _logger.LogInformation("API login successful for user: {Email}", user.Email);

            return Ok(new LoginResponse
            {
                Token = token,
                RefreshToken = refreshToken,
                Expires = DateTime.UtcNow.AddHours(24),
                User = new UserInfo
                {
                    Id = user.Id,
                    Email = user.Email,
                    FirstName = user.FirstName,
                    LastName = user.LastName,
                    FullName = user.FullName
                }
            });
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterRequest request)
        {
            _logger.LogInformation("API registration attempt for email: {Email}", request.Email);

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (await _db.Users.AnyAsync(u => u.Email == request.Email))
            {
                return Conflict(new { message = "Email already exists" });
            }

            var user = new AppUser
            {
                Id = Guid.NewGuid().ToString(),
                FirstName = request.FirstName,
                LastName = request.LastName,
                Email = request.Email,
                UserName = request.Email,
                PasswordHash = HashPassword(request.Password),
                CreatedAt = DateTime.UtcNow,
                IsActive = true
            };

            _db.Users.Add(user);
            await _db.SaveChangesAsync();

            _logger.LogInformation("API registration successful for user: {Email}", user.Email);

            var token = _jwtService.GenerateToken(user);
            var refreshToken = _jwtService.GenerateRefreshToken();

            return CreatedAtAction(nameof(Login), new LoginResponse
            {
                Token = token,
                RefreshToken = refreshToken,
                Expires = DateTime.UtcNow.AddHours(24),
                User = new UserInfo
                {
                    Id = user.Id,
                    Email = user.Email,
                    FirstName = user.FirstName,
                    LastName = user.LastName,
                    FullName = user.FullName
                }
            });
        }

        [HttpGet("me")]
        [Microsoft.AspNetCore.Authorization.Authorize]
        public async Task<IActionResult> GetCurrentUser()
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (string.IsNullOrEmpty(userId))
            {
                return Unauthorized();
            }

            var user = await _db.Users.FindAsync(userId);
            if (user == null)
            {
                return NotFound();
            }

            return Ok(new UserInfo
            {
                Id = user.Id,
                Email = user.Email,
                FirstName = user.FirstName,
                LastName = user.LastName,
                FullName = user.FullName
            });
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
    }

    public class RegisterRequest
    {
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
    }
}

