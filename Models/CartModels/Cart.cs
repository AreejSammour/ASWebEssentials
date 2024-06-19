using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace ASWebEssentials.Models.CartModels
{
    public class Cart
    {
        public int Id { get; set; }
        public List<CartItem>? Items { get; set;}

        [Range(0.01, double.MaxValue)]
        [DataType(DataType.Currency)]
        [Column(TypeName = "decimal(18, 2)")]
        public decimal TotalPrice { get; set; }
        public int TotalQuantity { get; set; }
        public string? CartStatus { get; set; }
        public AppUser? User { get; set; }
    }
}
