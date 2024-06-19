namespace ASWebEssentials.Models.OrderModels
{
    public class Orders
    {
        public int Id { get; set; }
        public AppUser? User { get; set; }
        public List<OrderItem>? OrderItem { get; set; } 
    }
}
