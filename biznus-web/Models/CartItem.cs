using System.ComponentModel.DataAnnotations;

namespace biznus_web.Models
{
    /// <summary>
    /// Примитивная корзина: товары, выбранные пользователем или сессией.
    /// </summary>
    public class CartItem
    {
        public int Id { get; set; }

        [MaxLength(64)]
        public string? UserId { get; set; }

        [MaxLength(64)]
        public string? SessionId { get; set; }

        public int ProductId { get; set; }
        public Product? Product { get; set; }

        [Range(1, 999)]
        public int Quantity { get; set; } = 1;

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }
}

