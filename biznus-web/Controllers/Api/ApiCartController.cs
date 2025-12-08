using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;
using biznus_web.Models;
using biznus_web.Models.DTOs;
using biznus_web.Services;

namespace biznus_web.Controllers.Api
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class ApiCartController : ControllerBase
    {
        private readonly ApplicationDbContext _db;
        private readonly TranslationService _translationService;
        private readonly ILogger<ApiCartController> _logger;

        public ApiCartController(
            ApplicationDbContext db,
            TranslationService translationService,
            ILogger<ApiCartController> logger)
        {
            _db = db;
            _translationService = translationService;
            _logger = logger;
        }

        [HttpGet]
        public async Task<IActionResult> GetCart([FromQuery] string? culture = null)
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (string.IsNullOrEmpty(userId))
            {
                return Unauthorized();
            }

            _logger.LogInformation("API: GetCart - UserId: {UserId}", userId);

            culture ??= System.Globalization.CultureInfo.CurrentUICulture.Name;

            var cartItems = await _db.CartItems
                .Include(c => c.Product)
                .Where(c => c.UserId == userId)
                .ToListAsync();

            var result = new List<CartItemDto>();
            foreach (var item in cartItems)
            {
                if (item.Product == null) continue;
                var localizedProduct = await _translationService.ApplyLocalizationAsync(item.Product, culture);
                result.Add(new CartItemDto
                {
                    Id = item.Id,
                    ProductId = item.ProductId,
                    Product = new ProductDto
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
                    },
                    Quantity = item.Quantity,
                    CreatedAt = item.CreatedAt
                });
            }

            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> AddToCart([FromBody] AddToCartRequest request)
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (string.IsNullOrEmpty(userId))
            {
                return Unauthorized();
            }

            _logger.LogInformation("API: AddToCart - UserId: {UserId}, ProductId: {ProductId}, Quantity: {Quantity}", 
                userId, request.ProductId, request.Quantity);

            var product = await _db.Products.FindAsync(request.ProductId);
            if (product == null)
            {
                return NotFound(new { message = "Product not found" });
            }

            var existingItem = await _db.CartItems
                .FirstOrDefaultAsync(c => c.UserId == userId && c.ProductId == request.ProductId);

            if (existingItem != null)
            {
                existingItem.Quantity += request.Quantity;
            }
            else
            {
                var cartItem = new CartItem
                {
                    UserId = userId,
                    ProductId = request.ProductId,
                    Quantity = request.Quantity,
                    CreatedAt = DateTime.UtcNow
                };
                _db.CartItems.Add(cartItem);
            }

            await _db.SaveChangesAsync();

            _logger.LogInformation("API: Item added to cart - UserId: {UserId}, ProductId: {ProductId}", 
                userId, request.ProductId);

            return Ok(new { message = "Item added to cart" });
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateCartItem(int id, [FromBody] int quantity)
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (string.IsNullOrEmpty(userId))
            {
                return Unauthorized();
            }

            _logger.LogInformation("API: UpdateCartItem - UserId: {UserId}, ItemId: {ItemId}, Quantity: {Quantity}", 
                userId, id, quantity);

            var cartItem = await _db.CartItems
                .FirstOrDefaultAsync(c => c.Id == id && c.UserId == userId);

            if (cartItem == null)
            {
                return NotFound(new { message = "Cart item not found" });
            }

            if (quantity <= 0)
            {
                _db.CartItems.Remove(cartItem);
            }
            else
            {
                cartItem.Quantity = quantity;
            }

            await _db.SaveChangesAsync();

            return Ok(new { message = "Cart item updated" });
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> RemoveFromCart(int id)
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (string.IsNullOrEmpty(userId))
            {
                return Unauthorized();
            }

            _logger.LogInformation("API: RemoveFromCart - UserId: {UserId}, ItemId: {ItemId}", userId, id);

            var cartItem = await _db.CartItems
                .FirstOrDefaultAsync(c => c.Id == id && c.UserId == userId);

            if (cartItem == null)
            {
                return NotFound(new { message = "Cart item not found" });
            }

            _db.CartItems.Remove(cartItem);
            await _db.SaveChangesAsync();

            return Ok(new { message = "Item removed from cart" });
        }

        [HttpDelete("clear")]
        public async Task<IActionResult> ClearCart()
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (string.IsNullOrEmpty(userId))
            {
                return Unauthorized();
            }

            _logger.LogInformation("API: ClearCart - UserId: {UserId}", userId);

            var cartItems = await _db.CartItems
                .Where(c => c.UserId == userId)
                .ToListAsync();

            _db.CartItems.RemoveRange(cartItems);
            await _db.SaveChangesAsync();

            return Ok(new { message = "Cart cleared" });
        }
    }
}

