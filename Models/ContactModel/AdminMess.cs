using System.ComponentModel.DataAnnotations;

namespace ASWebEssentials.Models.ContactModel
{
    public class AdminMess
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Please specify what your question is about")]
        public string? MessAbout { get; set; }

        [Required(ErrorMessage = "Please enter your question")]
        [MaxLength(2000, ErrorMessage = "Maximum length for the question is 1500 characters")]
        public string? MessBody { get; set; }
        public DateTime? CreatedDate { get; set; } = DateTime.Now;
        public string? Status { get; set; }
        public string? Recipient { get; set; }

        public List<MessageRes>? MessageRes { get; set; }
    }
}
