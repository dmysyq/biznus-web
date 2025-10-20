using Microsoft.AspNetCore.Mvc;
using biznus_web.Models;

namespace biznus_web.Controllers
{
    public class ContactController : Controller
    {
        private readonly ILogger<ContactController> _logger;


        public ContactController(ILogger<ContactController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            _logger.LogInformation("User accessed contact page");
            ViewData["Title"] = "Contact";
            var model = new ContactViewModel();
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Index(ContactViewModel model)
        {
            _logger.LogInformation("Contact form submitted. Name: {Name}, Email: {Email}", 
                model.Name, model.Email);
            
            if (ModelState.IsValid)
            {
                // Здесь можно добавить логику обработки формы
                // Например, отправка email или сохранение в базу данных
                
                _logger.LogInformation("Contact form submitted successfully. Name: {Name}, Email: {Email}", 
                    model.Name, model.Email);
                
                // Имитируем успешную отправку
                model.IsSuccess = true;
                model.SuccessMessage = "Thank you for your message! We'll get back to you soon.";
                
                // Перенаправляем на страницу успеха
                return RedirectToAction("Success", new { message = model.SuccessMessage });
            }

            _logger.LogWarning("Contact form validation failed. Name: {Name}, Email: {Email}", 
                model.Name, model.Email);
            
            ViewData["Title"] = "Contact";
            return View(model);
        }

        public IActionResult Success(string message)
        {
            _logger.LogInformation("User accessed contact success page");
            ViewData["Title"] = "Contact - Success";
            ViewBag.SuccessMessage = message;
            return View();
        }
    }
}
