using System.ComponentModel.DataAnnotations;

namespace Commander.Models
{
    public class Command
    {
        [Key] // This attribute is used to specify that a property is a primary key
        public int Id { get; set; }

        [Required] // This attribute is used to specify that a property is required
        [MaxLength(250)] // This attribute is used to specify the maximum length of a string property
        public string HowTo { get; set; }

        [Required]
        public string Line { get; set; }

        [Required]
        public string Platform { get; set; }
    }
}
