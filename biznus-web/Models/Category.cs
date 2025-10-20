using System.ComponentModel.DataAnnotations;

namespace biznus_web.Models
{
    public class Category
    {
        public int Id { get; set; }
        
        [Required(ErrorMessage = "Название категории обязательно")]
        [StringLength(50, ErrorMessage = "Название категории не должно превышать 50 символов")]
        public string Name { get; set; } = string.Empty;
        
        [StringLength(50, ErrorMessage = "Slug не должно превышать 50 символов")]
        public string Slug { get; set; } = string.Empty;
        
        [StringLength(200, ErrorMessage = "Описание не должно превышать 200 символов")]
        public string? Description { get; set; }
        
        public string? ImageUrl { get; set; }
        
        public bool IsActive { get; set; } = true;
        
        public List<Product> Products { get; set; } = new List<Product>();
    }
}
