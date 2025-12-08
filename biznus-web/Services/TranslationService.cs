using biznus_web.Models;
using Microsoft.EntityFrameworkCore;
using System.Globalization;

namespace biznus_web.Services
{
    /// <summary>
    /// Сервис для работы с переводами из БД
    /// </summary>
    public class TranslationService
    {
        private readonly ApplicationDbContext _db;

        public TranslationService(ApplicationDbContext db)
        {
            _db = db;
        }

        /// <summary>
        /// Получить перевод по ключу, культуре и области
        /// </summary>
        public async Task<string?> GetTranslationAsync(string key, string? scope = null, string? culture = null)
        {
            culture ??= CultureInfo.CurrentUICulture.Name;
            
            var translation = await _db.Translations
                .FirstOrDefaultAsync(t => 
                    t.Key == key && 
                    t.Culture == culture && 
                    (scope == null || t.Scope == scope));

            return translation?.Value;
        }

        /// <summary>
        /// Получить локализованное название товара
        /// </summary>
        public async Task<string> GetProductNameAsync(int productId, string defaultName, string? culture = null)
        {
            culture ??= CultureInfo.CurrentUICulture.Name;
            var key = $"Product_{productId}_Name";
            var translation = await GetTranslationAsync(key, "Product", culture);
            return translation ?? defaultName;
        }

        /// <summary>
        /// Получить локализованное описание товара
        /// </summary>
        public async Task<string?> GetProductDescriptionAsync(int productId, string? defaultDescription, string? culture = null)
        {
            culture ??= CultureInfo.CurrentUICulture.Name;
            var key = $"Product_{productId}_Description";
            var translation = await GetTranslationAsync(key, "Product", culture);
            return translation ?? defaultDescription;
        }

        /// <summary>
        /// Применить локализацию к списку товаров
        /// </summary>
        public async Task<List<Product>> ApplyLocalizationAsync(List<Product> products, string? culture = null)
        {
            culture ??= CultureInfo.CurrentUICulture.Name;
            var localizedProducts = new List<Product>();

            foreach (var product in products)
            {
                var localizedProduct = new Product
                {
                    Id = product.Id,
                    Name = await GetProductNameAsync(product.Id, product.Name, culture),
                    Description = await GetProductDescriptionAsync(product.Id, product.Description, culture),
                    Price = product.Price,
                    ImageUrl = product.ImageUrl,
                    Category = product.Category,
                    CategoryId = product.CategoryId,
                    CategoryRef = product.CategoryRef,
                    IsAvailable = product.IsAvailable,
                    CreatedDate = product.CreatedDate
                };
                localizedProducts.Add(localizedProduct);
            }

            return localizedProducts;
        }

        /// <summary>
        /// Применить локализацию к одному товару
        /// </summary>
        public async Task<Product> ApplyLocalizationAsync(Product product, string? culture = null)
        {
            culture ??= CultureInfo.CurrentUICulture.Name;
            
            return new Product
            {
                Id = product.Id,
                Name = await GetProductNameAsync(product.Id, product.Name, culture),
                Description = await GetProductDescriptionAsync(product.Id, product.Description, culture),
                Price = product.Price,
                ImageUrl = product.ImageUrl,
                Category = product.Category,
                CategoryId = product.CategoryId,
                CategoryRef = product.CategoryRef,
                IsAvailable = product.IsAvailable,
                CreatedDate = product.CreatedDate
            };
        }
    }
}

