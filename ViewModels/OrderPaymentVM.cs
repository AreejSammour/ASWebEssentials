using ASWebEssentials.Models;
using ASWebEssentials.Models.OrderModels;

namespace ASWebEssentials.ViewModels
{
    public class OrderPaymentVM
    {
        public OrderItem? OrderItem { get; set; }
        public PaymentCard? PaymentCard { get; set; }
    }
}
