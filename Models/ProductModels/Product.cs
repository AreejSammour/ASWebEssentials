using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace ASWebEssentials.Models.ProductModels
{
    public class Product
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(200, MinimumLength = 1, ErrorMessage = "The Product Name value must be between 1-200 characters")]
        public string? ProdName { get; set; }

        [Required]
        [StringLength(int.MaxValue, MinimumLength = 5, ErrorMessage = "The Product Description value must be at least 5 characters")]
		public string? ShortDesc { get; set; }
		public string? DetailDesc { get; set; }
		public List<string?>? Features { get; set; }

		[Required]
        public string? ImageUrl { get; set; }
        public List<Option>? Options { get; set; }

        public List<Category>? Categories { get; set; }
    }
}
