using ASWebEssentials.Models;
using ASWebEssentials.Models.ProductModels;

namespace ASWebEssentials.ViewModels
{
    public class HomeVM
    {
        public List<Feature>? Features { get; set; }
        public List<Services>? Services { get; set; }
        public List<Product>? Products { get; set; } 
    }
}
