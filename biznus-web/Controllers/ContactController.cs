using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using biznus_web.Models;

namespace biznus_web.Controllers
{
    public class ContactController : Controller
    {
        private readonly ILogger<ContactController> _logger;
        private readonly ApplicationDbContext _db;

        public ContactController(
            ILogger<ContactController> logger,
            ApplicationDbContext db)
        {
            _logger = logger;
            _db = db;
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
        public async Task<IActionResult> Index(ContactViewModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    // Сохраняем сообщение в БД
                    var contactMessage = new ContactMessage
                    {
                        Name = model.Name,
                        Email = model.Email,
                        Message = model.Message,
                        CreatedAt = DateTime.UtcNow,
                        IsRead = false
                    };

                    _db.ContactMessages.Add(contactMessage);
                    await _db.SaveChangesAsync();

                    _logger.LogInformation("Contact message saved to database. ID: {Id}, Name: {Name}, Email: {Email}", 
                        contactMessage.Id, model.Name, model.Email);
                    
                    // Перенаправляем на страницу успеха
                    return RedirectToAction("Success");
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "Error saving contact message. Name: {Name}, Email: {Email}", 
                        model.Name, model.Email);
                    ModelState.AddModelError(string.Empty, "Произошла ошибка при отправке сообщения. Пожалуйста, попробуйте позже.");
                }
            }

            _logger.LogWarning("Contact form validation failed. Name: {Name}, Email: {Email}", 
                model.Name, model.Email);
            
            ViewData["Title"] = "Contact";
            return View(model);
        }

        public IActionResult Success()
        {
            _logger.LogInformation("User accessed contact success page");
            ViewData["Title"] = "Contact - Success";
            return View();
        }
    }
}
