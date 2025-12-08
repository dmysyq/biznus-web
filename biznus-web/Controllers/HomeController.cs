using biznus_web.Models;
using biznus_web.Services;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Diagnostics;

namespace biznus_web.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IStringLocalizer<HomeController> _stringLocalizer;
        private readonly ApplicationDbContext _db;
        private readonly TranslationService _translationService;


        public HomeController(
            ILogger<HomeController> logger,
            IStringLocalizer<HomeController> stringLocalizer,
            ApplicationDbContext db,
            TranslationService translationService)
        {
            _logger = logger;
            _stringLocalizer = stringLocalizer;
            _db = db;
            _translationService = translationService;
        }

        public async Task<IActionResult> Index()
        {
            _logger.LogInformation("User accessed home page");
            _logger.LogInformation("Current culture: {Culture}", System.Globalization.CultureInfo.CurrentCulture.Name);
            _logger.LogInformation("Current UI culture: {UICulture}", System.Globalization.CultureInfo.CurrentUICulture.Name);
            
            var viewModel = new HomeViewModel
            {
                FeaturedProducts = await GetFeaturedProductsAsync(),
                Categories = await GetCategoriesAsync()
            };
            return View(viewModel);

        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        public JsonResult Cookie(string culture)
        {
            _logger.LogInformation("User changed culture to: {Culture}", culture);

            Response.Cookies.Append(
                CookieRequestCultureProvider.DefaultCookieName,
                CookieRequestCultureProvider.MakeCookieValue(new RequestCulture(culture)),
                new CookieOptions {Expires = DateTimeOffset.Now.AddHours(1) }
            );

            return Json(culture);
        }

        [HttpGet]
        [Route("/test-logging")]
        public IActionResult TestLogging()
        {
            _logger.LogTrace("Test Trace log - This is a trace message");
            _logger.LogDebug("Test Debug log - This is a debug message");
            _logger.LogInformation("Test Information log - This is an info message");
            _logger.LogWarning("Test Warning log - This is a warning message");
            _logger.LogError("Test Error log - This is an error message (test)");
            _logger.LogCritical("Test Critical log - This is a critical message (test)");

            return Json(new 
            { 
                message = "Test logs sent! Check Seq at http://localhost:5341",
                timestamp = DateTime.UtcNow,
                logs = new[]
                {
                    "Trace", "Debug", "Information", "Warning", "Error", "Critical"
                }
            });
        }



        private async Task<List<Product>> GetFeaturedProductsAsync()
        {
            var products = await _db.Products
                .Where(p => p.IsAvailable)
                .OrderBy(p => p.Id)
                .Take(4)
                .ToListAsync();
            
            return await _translationService.ApplyLocalizationAsync(products);
        }

        private async Task<List<Category>> GetCategoriesAsync()
        {
            return await _db.Categories
                .Where(c => c.IsActive)
                .OrderBy(c => c.Id)
                .ToListAsync();
        }
    }
}