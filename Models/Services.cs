using System.ComponentModel.DataAnnotations;

namespace ASWebEssentials.Models
{
    public class Services
    {
        [Key]
        public int? Id { get; set; }
        [Required]
        public string? Name { get; set; }
        public string? Descr1 { get; set; }
        public string? Descr2 { get; set;}
        public string? Image {  get; set;}
        public int ServiceId { get; set; }
        public Service Service { get; set; }

    }
}
