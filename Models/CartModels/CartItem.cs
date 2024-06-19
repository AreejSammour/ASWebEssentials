using ASWebEssentials.Models.ProductModels;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ASWebEssentials.Models.CartModels
{
    public class CartItem
    {
        public int Id { get; set; }
        [Required]
        public string? Name { get; set; }
        [Required]
        [Range(1, int.MaxValue)]
        public int Quantity { get; set; }
        [Required]

        [ForeignKey("Product")]
        public int? ProdId { get; set; }
        public Product? Product { get; set; }
        [Required]

        [ForeignKey("Cart")]

        public int? CartId {  get; set; }
        public Cart? Cart { get; set; }

        [Range(0.01, double.MaxValue)]
        [DataType(DataType.Currency)]
        [Column(TypeName = "decimal(18, 2)")]
        public decimal QuantPrice { get; set; }
        public DateTime CreatedDateTime { get; set; } = DateTime.Now;
    }
}
