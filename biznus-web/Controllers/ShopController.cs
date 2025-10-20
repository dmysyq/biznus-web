using Microsoft.AspNetCore.Mvc;
using biznus_web.Models;

namespace biznus_web.Controllers
{
    public class ShopController : Controller
    {
        private readonly ILogger<ShopController> _logger;

        public ShopController(ILogger<ShopController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index(string category, string search, int page = 1)
        {
            _logger.LogInformation("User accessed shop page. Category: {Category}, Search: {Search}, Page: {Page}", 
                category ?? "All", search ?? "None", page);
            
            var viewModel = new ShopViewModel
            {
                Products = GetProducts(category, search),
                Categories = GetCategories(),
                SelectedCategory = category,
                SearchTerm = search,
                CurrentPage = page,
                PageSize = 12
            };

            // Простая пагинация
            var totalProducts = viewModel.Products.Count;
            viewModel.TotalPages = (int)Math.Ceiling((double)totalProducts / viewModel.PageSize);
            
            viewModel.Products = viewModel.Products
                .Skip((page - 1) * viewModel.PageSize)
                .Take(viewModel.PageSize)
                .ToList();

            return View(viewModel);
        }

        public IActionResult Product(int id)
        {
            _logger.LogInformation("User accessed product page. Product ID: {ProductId}", id);
            
            var product = GetProductById(id);
            if (product == null)
            {
                _logger.LogWarning("Product not found. Product ID: {ProductId}", id);
                return NotFound();
            }

            _logger.LogInformation("Product found: {ProductName} (ID: {ProductId})", product.Name, id);
            return View(product);
        }

        private List<Product> GetProducts(string? category = null, string? search = null)
        {
            var products = new List<Product>
            {
                new Product { Id = 1, Name = "White Tent", Price = 200.00m, ImageUrl = "~/images/patrick-hendry-edguygu93yw-unsplash.jpg", Category = "Tents", Description = "A reliable tent for your outdoor adventures" },
                new Product { Id = 2, Name = "Tin Coffee Tumbler", Price = 35.00m, ImageUrl = "~/images/ryan-holloway-jydmuaxmib4-unsplash.jpg", Category = "Accessories", Description = "Keep your drinks hot or cold with this tumbler" },
                new Product { Id = 3, Name = "Blue Canvas Pack", Price = 95.00m, ImageUrl = "~/images/denisse-leon-j7cjwufjmg4-unsplash.jpg", Category = "Packs", Description = "Durable canvas pack for hiking and travel" },
                new Product { Id = 4, Name = "Gift Card", Price = 25.00m, ImageUrl = "~/images/acme-gift-card.jpg", Category = "Gift Cards", Description = "Perfect gift for any outdoor enthusiast" },
                new Product { Id = 5, Name = "Green Canvas Pack", Price = 125.00m, ImageUrl = "~/images/jakob-owens-o_bhy3tnsyu-unsplash.jpg", Category = "Packs", Description = "Stylish green canvas pack for adventures" },
                new Product { Id = 6, Name = "Red Tent", Price = 250.00m, ImageUrl = "~/images/felix-rostig-umv2wr-vbq8-unsplash.jpg", Category = "Tents", Description = "Spacious family tent for camping" }
            };

            if (!string.IsNullOrEmpty(category))
            {
                products = products.Where(p => p.Category == category).ToList();
            }

            if (!string.IsNullOrEmpty(search))
            {
                products = products.Where(p => 
                    p.Name.Contains(search, StringComparison.OrdinalIgnoreCase) ||
                    p.Description?.Contains(search, StringComparison.OrdinalIgnoreCase) == true).ToList();
            }

            return products;
        }

        private Product? GetProductById(int id)
        {
            return GetProducts().FirstOrDefault(p => p.Id == id);
        }

        private List<Category> GetCategories()
        {
            return new List<Category>
            {
                new Category { Id = 1, Name = "Tents", Description = "Reliable tents for camping and outdoor adventures" },
                new Category { Id = 2, Name = "Packs", Description = "Durable backpacks and travel packs" },
                new Category { Id = 3, Name = "Accessories", Description = "Essential accessories for outdoor activities" },
                new Category { Id = 4, Name = "Gift Cards", Description = "Perfect gifts for any outdoor enthusiast" }
            };
        }
    }
}
