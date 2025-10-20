namespace biznus_web.Models
{
    public class HomeViewModel
    {
        public List<Product> FeaturedProducts { get; set; } = new List<Product>();
        public List<Category> Categories { get; set; } = new List<Category>();
        public string? WelcomeMessage { get; set; }
        public string? PageTitle { get; set; }
    }
}
