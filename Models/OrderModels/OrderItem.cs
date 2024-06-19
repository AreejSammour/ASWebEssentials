using ASWebEssentials.Models.CartModels;

namespace ASWebEssentials.Models.OrderModels
{
    public class OrderItem
    {
        public int Id { get; set; }
        public int CartId { get; set; }
        public Cart? Cart { get; set; }
        public string? OrderItemId { get; set; }
        public int OrdersId { get; set; }
        public Orders? Orders { get; set; }
        public DateTime PlacedDate { get; set; } = DateTime.Now;
        public string? Status { get; set; }
    }
}
