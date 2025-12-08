using System.ComponentModel.DataAnnotations;

namespace biznus_web.Models
{
    /// <summary>
    /// Переводы для динамического контента (товары, карточки, админ-редактируемые тексты).
    /// Статические тексты остаются в .resx.
    /// </summary>
    public class Translation
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(150)]
        public string Key { get; set; } = string.Empty;

        [Required]
        [MaxLength(10)]
        public string Culture { get; set; } = "ru-RU";

        [Required]
        [MaxLength(2000)]
        public string Value { get; set; } = string.Empty;

        /// <summary>
        /// Дополнительный признак области/страницы (например, Home, Shop, Product).
        /// </summary>
        [MaxLength(100)]
        public string? Scope { get; set; }

        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
    }
}

