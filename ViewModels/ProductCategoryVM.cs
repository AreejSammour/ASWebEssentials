using ASWebEssentials.Models.ProductModels;

namespace ASWebEssentials.ViewModels
{
    public class ProductCategoryVM
    {
        public List<Product>? Products { get; set; }
        public List<Category>? Categories { get; set; }
        public string? SelectedCategory { get; set; }
        public List<Product>? SelectedProducts { get; set; }
    }
}
