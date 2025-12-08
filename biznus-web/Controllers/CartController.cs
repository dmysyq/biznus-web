using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using biznus_web.Models;
using biznus_web.Services;
using System.Globalization;

namespace biznus_web.Controllers
{
    public class CartController : Controller
    {
        private readonly CartService _cartService;
        private readonly ILogger<CartController> _logger;

        public CartController(
            CartService cartService,
            ILogger<CartController> logger)
        {
            _cartService = cartService;
            _logger = logger;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            _logger.LogInformation("User accessed cart page");
            var culture = CultureInfo.CurrentUICulture.Name;
            var cart = await _cartService.GetCartAsync(culture);
            return View(cart);
        }

        [HttpPost]
        [IgnoreAntiforgeryToken]
        public async Task<IActionResult> Add(int productId, int quantity = 1)
        {
            _logger.LogInformation("Adding product {ProductId} to cart with quantity {Quantity}", productId, quantity);

            if (quantity <= 0)
            {
                return BadRequest(new { message = "Quantity must be greater than 0" });
            }

            var success = await _cartService.AddToCartAsync(productId, quantity);
            if (!success)
            {
                return BadRequest(new { message = "Product not found or not available" });
            }

            var cartCount = _cartService.GetCartItemCount();
            return Json(new { success = true, cartCount = cartCount });
        }

        [HttpPost]
        public IActionResult Remove(int productId)
        {
            _logger.LogInformation("Removing product {ProductId} from cart", productId);
            _cartService.RemoveFromCart(productId);
            return RedirectToAction("Index");
        }

        [HttpPost]
        public IActionResult Update(int productId, int quantity)
        {
            _logger.LogInformation("Updating product {ProductId} quantity to {Quantity}", productId, quantity);
            _cartService.UpdateQuantity(productId, quantity);
            return RedirectToAction("Index");
        }

        [HttpGet]
        public async Task<IActionResult> Checkout()
        {
            _logger.LogInformation("User accessed checkout page");
            var culture = CultureInfo.CurrentUICulture.Name;
            var cart = await _cartService.GetCartAsync(culture);

            if (!cart.Items.Any())
            {
                _logger.LogWarning("User tried to checkout with empty cart");
                return RedirectToAction("Index");
            }

            var viewModel = new CheckoutViewModel
            {
                CartItems = cart.Items,
                Total = cart.Total
            };

            // Если пользователь авторизован, заполняем данные из профиля
            if (User.Identity?.IsAuthenticated == true)
            {
                viewModel.FirstName = User.FindFirst("FirstName")?.Value ?? "";
                viewModel.LastName = User.FindFirst("LastName")?.Value ?? "";
                viewModel.Email = User.FindFirst(System.Security.Claims.ClaimTypes.Email)?.Value ?? "";
            }

            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ProcessOrder(CheckoutViewModel model)
        {
            _logger.LogInformation("Processing order for email: {Email}", model.Email);

            var culture = CultureInfo.CurrentUICulture.Name;
            var cart = await _cartService.GetCartAsync(culture);

            if (!cart.Items.Any())
            {
                _logger.LogWarning("User tried to process order with empty cart");
                return RedirectToAction("Index");
            }

            model.CartItems = cart.Items;
            model.Total = cart.Total;

            if (!ModelState.IsValid)
            {
                _logger.LogWarning("Order validation failed for email: {Email}", model.Email);
                return View("Checkout", model);
            }

            // Здесь можно сохранить заказ в БД, но пока делаем заглушку
            _logger.LogInformation("Order processed successfully for email: {Email}, Total: {Total}", 
                model.Email, model.Total);

            // Очищаем корзину
            _cartService.ClearCart();

            return RedirectToAction("OrderSuccess", new { orderId = Guid.NewGuid().ToString() });
        }

        [HttpGet]
        public IActionResult OrderSuccess(string orderId)
        {
            _logger.LogInformation("Order success page displayed. OrderId: {OrderId}", orderId);
            ViewData["OrderId"] = orderId;
            return View();
        }

        [HttpGet]
        public IActionResult GetCartCount()
        {
            var count = _cartService.GetCartItemCount();
            return Json(new { count = count });
        }
    }
}

