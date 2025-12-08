using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;
using biznus_web.Models;
using biznus_web.Models.DTOs;
using biznus_web.Services;
using System.Globalization;

namespace biznus_web.Controllers.Api
{
    [ApiController]
    [Route("api/[controller]")]
    public class ApiProductsController : ControllerBase
    {
        private readonly ApplicationDbContext _db;
        private readonly TranslationService _translationService;
        private readonly ILogger<ApiProductsController> _logger;

        public ApiProductsController(
            ApplicationDbContext db,
            TranslationService translationService,
            ILogger<ApiProductsController> logger)
        {
            _db = db;
            _translationService = translationService;
            _logger = logger;
        }

        [HttpGet]
        public async Task<IActionResult> GetProducts(
            [FromQuery] string? category = null,
            [FromQuery] string? search = null,
            [FromQuery] int page = 1,
            [FromQuery] int pageSize = 12,
            [FromQuery] string? culture = null)
        {
            _logger.LogInformation("API: GetProducts - Category: {Category}, Search: {Search}, Page: {Page}", 
                category ?? "All", search ?? "None", page);

            culture ??= CultureInfo.CurrentUICulture.Name;

            var query = _db.Products.AsQueryable();

            if (!string.IsNullOrEmpty(category))
            {
                query = query.Where(p => p.Category == category);
            }

            if (!string.IsNullOrEmpty(search))
            {
                query = query.Where(p =>
                    p.Name.Contains(search) ||
                    (p.Description != null && p.Description.Contains(search)));
            }

            query = query.Where(p => p.IsAvailable).OrderBy(p => p.Id);

            var totalProducts = await query.CountAsync();
            var products = await query
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            var localizedProducts = await _translationService.ApplyLocalizationAsync(products, culture);

            var result = localizedProducts.Select(p => new ProductDto
            {
                Id = p.Id,
                Name = p.Name,
                Description = p.Description,
                Price = p.Price,
                ImageUrl = p.ImageUrl,
                Category = p.Category,
                CategoryId = p.CategoryId,
                IsAvailable = p.IsAvailable,
                CreatedDate = p.CreatedDate
            }).ToList();

            return Ok(new
            {
                Products = result,
                TotalCount = totalProducts,
                Page = page,
                PageSize = pageSize,
                TotalPages = (int)Math.Ceiling((double)totalProducts / pageSize)
            });
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetProduct(int id, [FromQuery] string? culture = null)
        {
            _logger.LogInformation("API: GetProduct - ID: {ProductId}", id);

            culture ??= CultureInfo.CurrentUICulture.Name;

            var product = await _db.Products.FindAsync(id);
            if (product == null)
            {
                return NotFound(new { message = "Product not found" });
            }

            var localizedProduct = await _translationService.ApplyLocalizationAsync(product, culture);

            var result = new ProductDto
            {
                Id = localizedProduct.Id,
                Name = localizedProduct.Name,
                Description = localizedProduct.Description,
                Price = localizedProduct.Price,
                ImageUrl = localizedProduct.ImageUrl,
                Category = localizedProduct.Category,
                CategoryId = localizedProduct.CategoryId,
                IsAvailable = localizedProduct.IsAvailable,
                CreatedDate = localizedProduct.CreatedDate
            };

            return Ok(result);
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> CreateProduct([FromBody] CreateProductRequest request)
        {
            _logger.LogInformation("API: CreateProduct - Name: {Name}", request.Name);

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var product = new Product
            {
                Name = request.Name,
                Description = request.Description,
                Price = request.Price,
                ImageUrl = request.ImageUrl,
                Category = request.Category,
                CategoryId = request.CategoryId,
                IsAvailable = request.IsAvailable,
                CreatedDate = DateTime.UtcNow
            };

            _db.Products.Add(product);
            await _db.SaveChangesAsync();

            _logger.LogInformation("API: Product created - ID: {ProductId}", product.Id);

            return CreatedAtAction(nameof(GetProduct), new { id = product.Id }, new ProductDto
            {
                Id = product.Id,
                Name = product.Name,
                Description = product.Description,
                Price = product.Price,
                ImageUrl = product.ImageUrl,
                Category = product.Category,
                CategoryId = product.CategoryId,
                IsAvailable = product.IsAvailable,
                CreatedDate = product.CreatedDate
            });
        }

        [HttpPut("{id}")]
        [Authorize]
        public async Task<IActionResult> UpdateProduct(int id, [FromBody] UpdateProductRequest request)
        {
            _logger.LogInformation("API: UpdateProduct - ID: {ProductId}", id);

            var product = await _db.Products.FindAsync(id);
            if (product == null)
            {
                return NotFound(new { message = "Product not found" });
            }

            product.Name = request.Name ?? product.Name;
            product.Description = request.Description ?? product.Description;
            product.Price = request.Price ?? product.Price;
            product.ImageUrl = request.ImageUrl ?? product.ImageUrl;
            product.Category = request.Category ?? product.Category;
            product.CategoryId = request.CategoryId ?? product.CategoryId;
            product.IsAvailable = request.IsAvailable ?? product.IsAvailable;

            await _db.SaveChangesAsync();

            _logger.LogInformation("API: Product updated - ID: {ProductId}", product.Id);

            return Ok(new ProductDto
            {
                Id = product.Id,
                Name = product.Name,
                Description = product.Description,
                Price = product.Price,
                ImageUrl = product.ImageUrl,
                Category = product.Category,
                CategoryId = product.CategoryId,
                IsAvailable = product.IsAvailable,
                CreatedDate = product.CreatedDate
            });
        }

        [HttpDelete("{id}")]
        [Authorize]
        public async Task<IActionResult> DeleteProduct(int id)
        {
            _logger.LogInformation("API: DeleteProduct - ID: {ProductId}", id);

            var product = await _db.Products.FindAsync(id);
            if (product == null)
            {
                return NotFound(new { message = "Product not found" });
            }

            _db.Products.Remove(product);
            await _db.SaveChangesAsync();

            _logger.LogInformation("API: Product deleted - ID: {ProductId}", id);

            return NoContent();
        }
    }

    public class CreateProductRequest
    {
        public string Name { get; set; } = string.Empty;
        public string? Description { get; set; }
        public decimal Price { get; set; }
        public string? ImageUrl { get; set; }
        public string? Category { get; set; }
        public int? CategoryId { get; set; }
        public bool IsAvailable { get; set; } = true;
    }

    public class UpdateProductRequest
    {
        public string? Name { get; set; }
        public string? Description { get; set; }
        public decimal? Price { get; set; }
        public string? ImageUrl { get; set; }
        public string? Category { get; set; }
        public int? CategoryId { get; set; }
        public bool? IsAvailable { get; set; }
    }
}

