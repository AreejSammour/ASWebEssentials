using ASWebEssentials.Models;
using ASWebEssentials.Models.CartModels;

namespace ASWebEssentials.ViewModels
{
    public class CheckoutVM
    {
        public string? UserName { get; set; }
        public string? Email { get; set; }
        public string? Address { get; set; }
        public List<CartVM>? CartVMList {  get; set; }
        public int TotalItems {  get; set; }
        public decimal TotalPrice { get; set; } 
        public PaymentCard? PaymentCard { get; set; }
    }
}


