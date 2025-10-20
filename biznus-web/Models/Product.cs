using System.ComponentModel.DataAnnotations;

namespace biznus_web.Models
{
    public class Product
    {
        public int Id { get; set; }
        
        [Required(ErrorMessage = "Название товара обязательно")]
        [StringLength(100, ErrorMessage = "Название не должно превышать 100 символов")]
        public string Name { get; set; } = string.Empty;
        
        [StringLength(500, ErrorMessage = "Описание не должно превышать 500 символов")]
        public string? Description { get; set; }
        
        [Required(ErrorMessage = "Цена обязательна")]
        [Range(0.01, double.MaxValue, ErrorMessage = "Цена должна быть больше 0")]
        public decimal Price { get; set; }
        
        public string? ImageUrl { get; set; }
        
        public string? Category { get; set; }
        
        public bool IsAvailable { get; set; } = true;
        
        public DateTime CreatedDate { get; set; } = DateTime.Now;
    }
}
