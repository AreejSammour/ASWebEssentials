namespace ASWebEssentials.Models.ContactModel
{
    public class TicketRes
    {
        public int Id { get; set; }
        public int TicketId { get; set; }
        public Ticket? Ticket { get; set; }
        public string? UserName { get; set; }
        public string? RespText {  get; set; }
        public DateTime? ResDate { get; set; } = DateTime.Now;
    }
}
