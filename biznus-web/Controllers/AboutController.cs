using Microsoft.AspNetCore.Mvc;

namespace biznus_web.Controllers
{
    public class AboutController : Controller
    {
        private readonly ILogger<AboutController> _logger;

        public AboutController(ILogger<AboutController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            _logger.LogInformation("User accessed about page");
            return View();
        }
    }
}