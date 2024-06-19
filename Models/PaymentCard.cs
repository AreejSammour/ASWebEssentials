using System.ComponentModel.DataAnnotations;

namespace ASWebEssentials.Models
{
    public class PaymentCard
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Card holder name is required.")]
        public string? CardHolderName { get; set; }

        [Required]
        [StringLength(16, MinimumLength = 16)]
        [Display(Name = "Card Number")]
        public string? CardNumber { get; set; }

        [Required(ErrorMessage = "Month of validity is required.")]
        [Range(1, 12, ErrorMessage = "Month must be between 1 and 12.")]
        public int MonthValid { get; set; }

        [Required(ErrorMessage = "Year of validity is required.")]
        [Range(2024, 2100, ErrorMessage = "Year must be between 2024 and 2100.")]
        public int YearValid { get; set; }

        [Required(ErrorMessage = "CVC is required.")]
        [RegularExpression(@"^\d{3,4}$", ErrorMessage = "CVC must be a 3 or 4-digit number.")]
        public string? CVC { get; set; }

        public AppUser? User { get; set; }

        public static implicit operator int(PaymentCard v)
        {
            throw new NotImplementedException();
        }
    }
}
