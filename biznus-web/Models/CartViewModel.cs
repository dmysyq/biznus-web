using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace biznus_web.Models
{
    public class CartViewModel
    {
        public List<CartItemViewModel> Items { get; set; } = new List<CartItemViewModel>();
        public decimal Total => Items.Sum(i => i.Subtotal);
    }

    public class CartItemViewModel
    {
        public int ProductId { get; set; }
        public string ProductName { get; set; } = string.Empty;
        public string? ProductImageUrl { get; set; }
        public decimal Price { get; set; }
        public int Quantity { get; set; }
        public decimal Subtotal => Price * Quantity;
    }

    public class CheckoutViewModel
    {
        [Required(ErrorMessage = "Имя обязательно")]
        [StringLength(50, ErrorMessage = "Имя не должно превышать 50 символов")]
        [Display(Name = "Имя")]
        public string FirstName { get; set; } = string.Empty;

        [Required(ErrorMessage = "Фамилия обязательна")]
        [StringLength(50, ErrorMessage = "Фамилия не должна превышать 50 символов")]
        [Display(Name = "Фамилия")]
        public string LastName { get; set; } = string.Empty;

        [Required(ErrorMessage = "Email обязателен")]
        [EmailAddress(ErrorMessage = "Неверный формат email")]
        [Display(Name = "Email")]
        public string Email { get; set; } = string.Empty;

        [Required(ErrorMessage = "Телефон обязателен")]
        [Phone(ErrorMessage = "Неверный формат телефона")]
        [Display(Name = "Телефон")]
        public string Phone { get; set; } = string.Empty;

        [Required(ErrorMessage = "Адрес обязателен")]
        [StringLength(200, ErrorMessage = "Адрес не должен превышать 200 символов")]
        [Display(Name = "Адрес доставки")]
        public string Address { get; set; } = string.Empty;

        [StringLength(100, ErrorMessage = "Город не должен превышать 100 символов")]
        [Display(Name = "Город")]
        public string? City { get; set; }

        [StringLength(20, ErrorMessage = "Почтовый индекс не должен превышать 20 символов")]
        [Display(Name = "Почтовый индекс")]
        public string? PostalCode { get; set; }

        [StringLength(500, ErrorMessage = "Комментарий не должен превышать 500 символов")]
        [Display(Name = "Комментарий к заказу")]
        public string? Comment { get; set; }

        public List<CartItemViewModel> CartItems { get; set; } = new List<CartItemViewModel>();
        public decimal Total { get; set; }
    }
}

