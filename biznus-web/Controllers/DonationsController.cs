using Microsoft.AspNetCore.Mvc;
using biznus_web.Models;

namespace biznus_web.Controllers
{
    public class DonationsController : Controller
    {
        private readonly ILogger<DonationsController> _logger;

        public DonationsController(ILogger<DonationsController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            _logger.LogInformation("User accessed donations page");
            ViewData["Title"] = "Donations";
            var model = new DonationViewModel();
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Index(DonationViewModel model)
        {
            _logger.LogInformation("Donation form submitted. Amount: {Amount}, Donor: {DonorName}, Email: {DonorEmail}", 
                model.Amount, model.DonorName ?? "Anonymous", model.DonorEmail ?? "Not provided");
            
            if (ModelState.IsValid)
            {
                // Здесь можно добавить логику обработки пожертвования
                // Например, интеграция с платежной системой
                
                _logger.LogInformation("Donation processed successfully. Amount: ${Amount}, Donor: {DonorName}", 
                    model.Amount, model.DonorName ?? "Anonymous");
                
                // Имитируем успешное пожертвование
                model.IsSuccess = true;
                model.SuccessMessage = $"Thank you for your donation of ${model.Amount:F2}!";
                
                // Перенаправляем на страницу успеха
                return RedirectToAction("Success", new { amount = model.Amount, message = model.SuccessMessage });
            }

            _logger.LogWarning("Donation form validation failed. Amount: {Amount}, Donor: {DonorName}", 
                model.Amount, model.DonorName ?? "Anonymous");
            
            ViewData["Title"] = "Donations";
            return View(model);
        }

        public IActionResult Success(decimal amount, string message)
        {
            _logger.LogInformation("User accessed donation success page. Amount: ${Amount}", amount);
            ViewData["Title"] = "Donations - Success";
            ViewBag.SuccessMessage = message;
            ViewBag.Amount = amount;
            return View();
        }
    }
}
