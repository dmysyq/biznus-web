using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;

namespace biznus_web.Controllers
{
    public class AboutController : Controller
    {
        private readonly ILogger<AboutController> _logger;
        private readonly IStringLocalizer<AboutController> _stringLocalizer;

        public AboutController(ILogger<AboutController> logger, IStringLocalizer<AboutController> stringLocalizer)
        {
            _logger = logger;
            _stringLocalizer = stringLocalizer;
        }

        public IActionResult Index()
        {
            _logger.LogInformation("User accessed about page");
            return View();
        }
    }
}