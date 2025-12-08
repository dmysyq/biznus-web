using biznus_web.Models;
using Microsoft.EntityFrameworkCore;
using System.Text.Json;

namespace biznus_web.Services
{
    public class CartService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly ApplicationDbContext _db;
        private readonly TranslationService _translationService;
        private const string CartSessionKey = "ShoppingCart";

        public CartService(
            IHttpContextAccessor httpContextAccessor,
            ApplicationDbContext db,
            TranslationService translationService)
        {
            _httpContextAccessor = httpContextAccessor;
            _db = db;
            _translationService = translationService;
        }

        private Dictionary<int, int> GetCart()
        {
            var session = _httpContextAccessor.HttpContext?.Session;
            if (session == null)
                return new Dictionary<int, int>();

            var cartJson = session.GetString(CartSessionKey);
            if (string.IsNullOrEmpty(cartJson))
                return new Dictionary<int, int>();

            try
            {
                return JsonSerializer.Deserialize<Dictionary<int, int>>(cartJson) ?? new Dictionary<int, int>();
            }
            catch
            {
                return new Dictionary<int, int>();
            }
        }

        private void SaveCart(Dictionary<int, int> cart)
        {
            var session = _httpContextAccessor.HttpContext?.Session;
            if (session == null)
                return;

            var cartJson = JsonSerializer.Serialize(cart);
            session.SetString(CartSessionKey, cartJson);
        }

        public async Task<CartViewModel> GetCartAsync(string? culture = null)
        {
            var cart = GetCart();
            var viewModel = new CartViewModel();

            if (!cart.Any())
                return viewModel;

            var productIds = cart.Keys.ToList();
            var products = await _db.Products
                .Where(p => productIds.Contains(p.Id) && p.IsAvailable)
                .ToListAsync();

            culture ??= System.Globalization.CultureInfo.CurrentUICulture.Name;

            foreach (var product in products)
            {
                var localizedProduct = await _translationService.ApplyLocalizationAsync(product, culture);
                viewModel.Items.Add(new CartItemViewModel
                {
                    ProductId = localizedProduct.Id,
                    ProductName = localizedProduct.Name,
                    ProductImageUrl = localizedProduct.ImageUrl,
                    Price = localizedProduct.Price,
                    Quantity = cart[product.Id]
                });
            }

            return viewModel;
        }

        public async Task<bool> AddToCartAsync(int productId, int quantity = 1)
        {
            var product = await _db.Products.FindAsync(productId);
            if (product == null || !product.IsAvailable)
                return false;

            var cart = GetCart();
            if (cart.ContainsKey(productId))
            {
                cart[productId] += quantity;
            }
            else
            {
                cart[productId] = quantity;
            }

            SaveCart(cart);
            return true;
        }

        public void RemoveFromCart(int productId)
        {
            var cart = GetCart();
            cart.Remove(productId);
            SaveCart(cart);
        }

        public void UpdateQuantity(int productId, int quantity)
        {
            if (quantity <= 0)
            {
                RemoveFromCart(productId);
                return;
            }

            var cart = GetCart();
            if (cart.ContainsKey(productId))
            {
                cart[productId] = quantity;
                SaveCart(cart);
            }
        }

        public void ClearCart()
        {
            var session = _httpContextAccessor.HttpContext?.Session;
            session?.Remove(CartSessionKey);
        }

        public int GetCartItemCount()
        {
            var cart = GetCart();
            return cart.Values.Sum();
        }
    }
}

