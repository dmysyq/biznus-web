using Microsoft.EntityFrameworkCore;

namespace biznus_web.Models
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<AppUser> Users { get; set; } = null!;
        public DbSet<Product> Products { get; set; } = null!;
        public DbSet<Category> Categories { get; set; } = null!;
        public DbSet<Translation> Translations { get; set; } = null!;
        public DbSet<CartItem> CartItems { get; set; } = null!;
        public DbSet<ContactMessage> ContactMessages { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Индексы для переводов
            modelBuilder.Entity<Translation>()
                .HasIndex(t => new { t.Key, t.Culture, t.Scope })
                .IsUnique(false);

            modelBuilder.Entity<Category>()
                .HasMany(c => c.Products)
                .WithOne(p => p.CategoryRef)
                .HasForeignKey(p => p.CategoryId)
                .OnDelete(DeleteBehavior.SetNull);

            modelBuilder.Entity<CartItem>()
                .HasOne(c => c.Product)
                .WithMany()
                .HasForeignKey(c => c.ProductId)
                .OnDelete(DeleteBehavior.Cascade);

            // Стартовые данные для быстрого теста
            // Пользователи регистрируются через форму регистрации
            modelBuilder.Entity<Category>().HasData(
                new Category { Id = 1, Name = "Tents", Slug = "tents", Description = "Reliable tents for camping and outdoor adventures", IsActive = true },
                new Category { Id = 2, Name = "Packs", Slug = "packs", Description = "Durable backpacks and travel packs", IsActive = true },
                new Category { Id = 3, Name = "Accessories", Slug = "accessories", Description = "Essential accessories for outdoor activities", IsActive = true },
                new Category { Id = 4, Name = "Gift Cards", Slug = "gift-cards", Description = "Perfect gifts for any outdoor enthusiast", IsActive = true }
            );

            modelBuilder.Entity<Product>().HasData(
                new Product { Id = 1, Name = "White Tent", Price = 200.00m, ImageUrl = "~/images/patrick-hendry-edguygu93yw-unsplash.jpg", Category = "Tents", CategoryId = 1, Description = "A reliable tent for your outdoor adventures", IsAvailable = true },
                new Product { Id = 2, Name = "Tin Coffee Tumbler", Price = 35.00m, ImageUrl = "~/images/ryan-holloway-jydmuaxmib4-unsplash.jpg", Category = "Accessories", CategoryId = 3, Description = "Keep your drinks hot or cold with this tumbler", IsAvailable = true },
                new Product { Id = 3, Name = "Blue Canvas Pack", Price = 95.00m, ImageUrl = "~/images/denisse-leon-j7cjwufjmg4-unsplash.jpg", Category = "Packs", CategoryId = 2, Description = "Durable canvas pack for hiking and travel", IsAvailable = true },
                new Product { Id = 4, Name = "Gift Card", Price = 25.00m, ImageUrl = "~/images/acme-gift-card.jpg", Category = "Gift Cards", CategoryId = 4, Description = "Perfect gift for any outdoor enthusiast", IsAvailable = true },
                new Product { Id = 5, Name = "Green Canvas Pack", Price = 125.00m, ImageUrl = "~/images/jakob-owens-o_bhy3tnsyu-unsplash.jpg", Category = "Packs", CategoryId = 2, Description = "Stylish green canvas pack for adventures", IsAvailable = true },
                new Product { Id = 6, Name = "Red Tent", Price = 250.00m, ImageUrl = "~/images/felix-rostig-umv2wr-vbq8-unsplash.jpg", Category = "Tents", CategoryId = 1, Description = "Spacious family tent for camping", IsAvailable = true }
            );

            // Переводы для товаров
            var now = DateTime.UtcNow;
            modelBuilder.Entity<Translation>().HasData(
                // Product 1 - White Tent
                new Translation { Id = 1, Key = "Product_1_Name", Culture = "en-US", Value = "White Tent", Scope = "Product", UpdatedAt = now },
                new Translation { Id = 2, Key = "Product_1_Name", Culture = "ru-RU", Value = "Белая палатка", Scope = "Product", UpdatedAt = now },
                new Translation { Id = 3, Key = "Product_1_Name", Culture = "kk-KZ", Value = "Ақ шатыр", Scope = "Product", UpdatedAt = now },
                new Translation { Id = 4, Key = "Product_1_Name", Culture = "fr-FR", Value = "Tente blanche", Scope = "Product", UpdatedAt = now },
                new Translation { Id = 5, Key = "Product_1_Description", Culture = "en-US", Value = "A reliable tent for your outdoor adventures", Scope = "Product", UpdatedAt = now },
                new Translation { Id = 6, Key = "Product_1_Description", Culture = "ru-RU", Value = "Надёжная палатка для ваших приключений на природе", Scope = "Product", UpdatedAt = now },
                new Translation { Id = 7, Key = "Product_1_Description", Culture = "kk-KZ", Value = "Табиғаттағы шытырман оқиғаларға арналған сенімді шатыр", Scope = "Product", UpdatedAt = now },
                new Translation { Id = 8, Key = "Product_1_Description", Culture = "fr-FR", Value = "Une tente fiable pour vos aventures en plein air", Scope = "Product", UpdatedAt = now },
                
                // Product 2 - Tin Coffee Tumbler
                new Translation { Id = 9, Key = "Product_2_Name", Culture = "en-US", Value = "Tin Coffee Tumbler", Scope = "Product", UpdatedAt = now },
                new Translation { Id = 10, Key = "Product_2_Name", Culture = "ru-RU", Value = "Жестяная кофейная кружка", Scope = "Product", UpdatedAt = now },
                new Translation { Id = 11, Key = "Product_2_Name", Culture = "kk-KZ", Value = "Қалайы кофе стаканы", Scope = "Product", UpdatedAt = now },
                new Translation { Id = 12, Key = "Product_2_Name", Culture = "fr-FR", Value = "Tasse à café en étain", Scope = "Product", UpdatedAt = now },
                new Translation { Id = 13, Key = "Product_2_Description", Culture = "en-US", Value = "Keep your drinks hot or cold with this tumbler", Scope = "Product", UpdatedAt = now },
                new Translation { Id = 14, Key = "Product_2_Description", Culture = "ru-RU", Value = "Сохраняйте напитки горячими или холодными с этой кружкой", Scope = "Product", UpdatedAt = now },
                new Translation { Id = 15, Key = "Product_2_Description", Culture = "kk-KZ", Value = "Бұл стаканмен сусындарды ыстық немесе суық сақтаңыз", Scope = "Product", UpdatedAt = now },
                new Translation { Id = 16, Key = "Product_2_Description", Culture = "fr-FR", Value = "Gardez vos boissons chaudes ou froides avec cette tasse", Scope = "Product", UpdatedAt = now },
                
                // Product 3 - Blue Canvas Pack
                new Translation { Id = 17, Key = "Product_3_Name", Culture = "en-US", Value = "Blue Canvas Pack", Scope = "Product", UpdatedAt = now },
                new Translation { Id = 18, Key = "Product_3_Name", Culture = "ru-RU", Value = "Синий холщовый рюкзак", Scope = "Product", UpdatedAt = now },
                new Translation { Id = 19, Key = "Product_3_Name", Culture = "kk-KZ", Value = "Көк кенеп рюкзак", Scope = "Product", UpdatedAt = now },
                new Translation { Id = 20, Key = "Product_3_Name", Culture = "fr-FR", Value = "Sac en toile bleue", Scope = "Product", UpdatedAt = now },
                new Translation { Id = 21, Key = "Product_3_Description", Culture = "en-US", Value = "Durable canvas pack for hiking and travel", Scope = "Product", UpdatedAt = now },
                new Translation { Id = 22, Key = "Product_3_Description", Culture = "ru-RU", Value = "Прочный холщовый рюкзак для походов и путешествий", Scope = "Product", UpdatedAt = now },
                new Translation { Id = 23, Key = "Product_3_Description", Culture = "kk-KZ", Value = "Жүру және саяхат үшін берік кенеп рюкзак", Scope = "Product", UpdatedAt = now },
                new Translation { Id = 24, Key = "Product_3_Description", Culture = "fr-FR", Value = "Sac en toile durable pour la randonnée et les voyages", Scope = "Product", UpdatedAt = now },
                
                // Product 4 - Gift Card
                new Translation { Id = 25, Key = "Product_4_Name", Culture = "en-US", Value = "Gift Card", Scope = "Product", UpdatedAt = now },
                new Translation { Id = 26, Key = "Product_4_Name", Culture = "ru-RU", Value = "Подарочная карта", Scope = "Product", UpdatedAt = now },
                new Translation { Id = 27, Key = "Product_4_Name", Culture = "kk-KZ", Value = "Сыйлық картасы", Scope = "Product", UpdatedAt = now },
                new Translation { Id = 28, Key = "Product_4_Name", Culture = "fr-FR", Value = "Carte cadeau", Scope = "Product", UpdatedAt = now },
                new Translation { Id = 29, Key = "Product_4_Description", Culture = "en-US", Value = "Perfect gift for any outdoor enthusiast", Scope = "Product", UpdatedAt = now },
                new Translation { Id = 30, Key = "Product_4_Description", Culture = "ru-RU", Value = "Идеальный подарок для любого любителя активного отдыха", Scope = "Product", UpdatedAt = now },
                new Translation { Id = 31, Key = "Product_4_Description", Culture = "kk-KZ", Value = "Кез келген табиғат сүйгішіне арналған мінсіз сыйлық", Scope = "Product", UpdatedAt = now },
                new Translation { Id = 32, Key = "Product_4_Description", Culture = "fr-FR", Value = "Cadeau parfait pour tout amateur de plein air", Scope = "Product", UpdatedAt = now },
                
                // Product 5 - Green Canvas Pack
                new Translation { Id = 33, Key = "Product_5_Name", Culture = "en-US", Value = "Green Canvas Pack", Scope = "Product", UpdatedAt = now },
                new Translation { Id = 34, Key = "Product_5_Name", Culture = "ru-RU", Value = "Зелёный холщовый рюкзак", Scope = "Product", UpdatedAt = now },
                new Translation { Id = 35, Key = "Product_5_Name", Culture = "kk-KZ", Value = "Жасыл кенеп рюкзак", Scope = "Product", UpdatedAt = now },
                new Translation { Id = 36, Key = "Product_5_Name", Culture = "fr-FR", Value = "Sac en toile verte", Scope = "Product", UpdatedAt = now },
                new Translation { Id = 37, Key = "Product_5_Description", Culture = "en-US", Value = "Stylish green canvas pack for adventures", Scope = "Product", UpdatedAt = now },
                new Translation { Id = 38, Key = "Product_5_Description", Culture = "ru-RU", Value = "Стильный зелёный холщовый рюкзак для приключений", Scope = "Product", UpdatedAt = now },
                new Translation { Id = 39, Key = "Product_5_Description", Culture = "kk-KZ", Value = "Шытырман оқиғаларға арналған стильді жасыл кенеп рюкзак", Scope = "Product", UpdatedAt = now },
                new Translation { Id = 40, Key = "Product_5_Description", Culture = "fr-FR", Value = "Sac en toile verte élégant pour les aventures", Scope = "Product", UpdatedAt = now },
                
                // Product 6 - Red Tent
                new Translation { Id = 41, Key = "Product_6_Name", Culture = "en-US", Value = "Red Tent", Scope = "Product", UpdatedAt = now },
                new Translation { Id = 42, Key = "Product_6_Name", Culture = "ru-RU", Value = "Красная палатка", Scope = "Product", UpdatedAt = now },
                new Translation { Id = 43, Key = "Product_6_Name", Culture = "kk-KZ", Value = "Қызыл шатыр", Scope = "Product", UpdatedAt = now },
                new Translation { Id = 44, Key = "Product_6_Name", Culture = "fr-FR", Value = "Tente rouge", Scope = "Product", UpdatedAt = now },
                new Translation { Id = 45, Key = "Product_6_Description", Culture = "en-US", Value = "Spacious family tent for camping", Scope = "Product", UpdatedAt = now },
                new Translation { Id = 46, Key = "Product_6_Description", Culture = "ru-RU", Value = "Просторная семейная палатка для кемпинга", Scope = "Product", UpdatedAt = now },
                new Translation { Id = 47, Key = "Product_6_Description", Culture = "kk-KZ", Value = "Кемпинг үшін кең отбасылық шатыр", Scope = "Product", UpdatedAt = now },
                new Translation { Id = 48, Key = "Product_6_Description", Culture = "fr-FR", Value = "Tente familiale spacieuse pour le camping", Scope = "Product", UpdatedAt = now }
            );
        }
    }
}

