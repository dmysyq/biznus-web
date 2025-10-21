using biznus_web.Models;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using System.Diagnostics;

namespace biznus_web.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IStringLocalizer<HomeController> _stringLocalizer;


        public HomeController(ILogger<HomeController> logger, IStringLocalizer<HomeController> stringLocalizer)
        {
            _logger = logger;
            _stringLocalizer = stringLocalizer;
        }

        public IActionResult Index()
        {
            var test = _stringLocalizer["Home"];
            var servingSince = _stringLocalizer["ServingSince"];
            _logger.LogInformation("User accessed home page. Current culture: {Culture}", System.Globalization.CultureInfo.CurrentCulture.Name);
            _logger.LogInformation("UI Culture: {UICulture}", System.Globalization.CultureInfo.CurrentUICulture.Name);
            _logger.LogInformation("String localizer test Home: {TestValue}", test);
            _logger.LogInformation("String localizer test ServingSince: {ServingSinceValue}", servingSince);
            _logger.LogInformation("ResourceNotFound: {ResourceNotFound}", _stringLocalizer["Home"].ResourceNotFound);
            
            // Добавляем тестовые данные в ViewData для отладки
            ViewData["TestHome"] = test;
            ViewData["TestServingSince"] = servingSince;
            ViewData["CurrentCulture"] = System.Globalization.CultureInfo.CurrentCulture.Name;
            ViewData["CurrentUICulture"] = System.Globalization.CultureInfo.CurrentUICulture.Name;
            
            var viewModel = new HomeViewModel
            {
                FeaturedProducts = GetFeaturedProducts(),
                Categories = GetCategories()
            };
            return View(viewModel);
        }

        [HttpPost]
        public JsonResult Cookie(string culture)
        {
            _logger.LogInformation("User changed culture to: {Culture}", culture);
            
            Response.Cookies.Append(
                CookieRequestCultureProvider.DefaultCookieName,
                CookieRequestCultureProvider.MakeCookieValue(new RequestCulture(culture)),
                new CookieOptions { Expires = DateTimeOffset.Now.AddHours(1) }
            );

            return Json(culture);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        private List<Product> GetFeaturedProducts()
        {
            return new List<Product>
            {
                new Product { Id = 1, Name = "White Tent", Price = 200.00m, ImageUrl = "~/images/patrick-hendry-edguygu93yw-unsplash.jpg", Category = "Tents", Description = "A reliable tent for your outdoor adventures" },
                new Product { Id = 2, Name = "Tin Coffee Tumbler", Price = 35.00m, ImageUrl = "~/images/ryan-holloway-jydmuaxmib4-unsplash.jpg", Category = "Accessories", Description = "Keep your drinks hot or cold with this tumbler" },
                new Product { Id = 3, Name = "Blue Canvas Pack", Price = 95.00m, ImageUrl = "~/images/denisse-leon-j7cjwufjmg4-unsplash.jpg", Category = "Packs", Description = "Durable canvas pack for hiking and travel" },
                new Product { Id = 4, Name = "Gift Card", Price = 25.00m, ImageUrl = "~/images/acme-gift-card.jpg", Category = "Gift Cards", Description = "Perfect gift for any outdoor enthusiast" }
            };
        }

        private List<Category> GetCategories()
        {
            return new List<Category>
            {
                new Category { Id = 1, Name = "Tents", Slug = "tents", Description = "Reliable tents for camping and outdoor adventures" },
                new Category { Id = 2, Name = "Packs", Slug = "packs", Description = "Durable backpacks and travel packs" },
                new Category { Id = 3, Name = "Accessories", Slug = "accessories", Description = "Essential accessories for outdoor activities" },
                new Category { Id = 4, Name = "Gift Cards", Slug = "gift-cards", Description = "Perfect gifts for any outdoor enthusiast" }
            };
        }
    }
}