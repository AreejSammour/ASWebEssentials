using System.ComponentModel.DataAnnotations;

namespace ASWebEssentials.Models.ContactModel
{
    public class Ticket
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Please enter your full name")]
        public string? FullName { get; set; }

        [Required(ErrorMessage = "Please enter your email")]
        [EmailAddress(ErrorMessage = "Please enter a valid email address")]
        public string? Email { get; set; }
        public string? PhoneNumber { get; set; }

        [Required(ErrorMessage = "Please specify what your question is about")]
        public  string? About { get; set; }

        [Required(ErrorMessage = "Please enter your question")]
        [MaxLength(2000, ErrorMessage = "Maximum length for the question is 1500 characters")]
        public string? Quest {  get; set; }
        public string? UserName { get; set; }
        public DateTime? CreatedDate { get; set; } = DateTime.Now;
        public string? Status { get; set; }  

        public List<TicketRes>? TicketRes { get; set; }

    }
}
