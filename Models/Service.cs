using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ASWebEssentials.Models
{
    public class Service
    {
        [Key]
        public int Id { get; set; }
        public required string Name { get; set; }
        public required string Description { get; set; }
        public List<string>? KeyOfferings { get; set; }
        public List<string>? WhyChooseUs { get; set; }
        public List<string>? OurProcess { get; set; }
        public string? GetInTouch {  get; set; }
    }
}
