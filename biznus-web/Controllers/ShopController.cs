using Microsoft.AspNetCore.Mvc;
using biznus_web.Models;
using biznus_web.Services;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace biznus_web.Controllers
{
    public class ShopController : Controller
    {
        private readonly ILogger<ShopController> _logger;
        private readonly ApplicationDbContext _db;
        private readonly TranslationService _translationService;

        public ShopController(
            ILogger<ShopController> logger,
            ApplicationDbContext db,
            TranslationService translationService)
        {
            _logger = logger;
            _db = db;
            _translationService = translationService;
        }

        public async Task<IActionResult> Index(string category, string search, int page = 1)
        {
            _logger.LogInformation("User accessed shop page. Category: {Category}, Search: {Search}, Page: {Page}", 
                category ?? "All", search ?? "None", page);
            
            var productsQuery = _db.Products.AsQueryable();

            if (!string.IsNullOrEmpty(category))
            {
                productsQuery = productsQuery.Where(p => p.Category == category);
            }

            if (!string.IsNullOrEmpty(search))
            {
                productsQuery = productsQuery.Where(p =>
                    p.Name.Contains(search) ||
                    (p.Description != null && p.Description.Contains(search)));
            }

            productsQuery = productsQuery.Where(p => p.IsAvailable).OrderBy(p => p.Id);

            const int pageSize = 12;
            var totalProducts = await productsQuery.CountAsync();
            var products = await productsQuery
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            var localizedProducts = await _translationService.ApplyLocalizationAsync(products);

            var viewModel = new ShopViewModel
            {
                Products = localizedProducts,
                Categories = await GetCategoriesAsync(),
                SelectedCategory = category,
                SearchTerm = search,
                CurrentPage = page,
                PageSize = pageSize,
                TotalPages = (int)Math.Ceiling((double)totalProducts / pageSize)
            };

            return View(viewModel);
        }

        public async Task<IActionResult> Product(int id)
        {
            _logger.LogInformation("User accessed product page. Product ID: {ProductId}", id);
            
            var product = await _db.Products.FirstOrDefaultAsync(p => p.Id == id);
            if (product == null)
            {
                _logger.LogWarning("Product not found. Product ID: {ProductId}", id);
                return NotFound();
            }

            var localizedProduct = await _translationService.ApplyLocalizationAsync(product);
            _logger.LogInformation("Product found: {ProductName} (ID: {ProductId})", localizedProduct.Name, id);
            return View(localizedProduct);
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
