namespace ASWebEssentials.Models.ContactModel
{
    public class MessageRes
    {
        public int Id { get; set; } // Primary key
        public int AdminMessId { get; set; } // Foreign key referencing AdminMess
        public AdminMess? AdminMess { get; set; } // Navigation property
        public string? UserName { get; set; }
        public string? RespText { get; set; }
        public DateTime? ResDate { get; set; } = DateTime.Now;
    }
}
