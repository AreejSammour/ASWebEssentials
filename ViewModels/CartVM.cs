namespace ASWebEssentials.ViewModels
{
    public class CartVM
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? ProdName { get; set; }
        public int CartItemId { get; set; }
        public string? Image {  get; set; }
        public decimal ItemPrice { get; set; }
        public decimal TotalItemPrice { get; set; }
        public int Quantity { get; set; }
    }
}
