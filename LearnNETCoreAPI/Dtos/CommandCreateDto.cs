using System.ComponentModel.DataAnnotations;

namespace Commander.Dtos
{
    public class CommandCreateDto
    {
        [Required] // This attribute is used to specify that a property is required
        [MaxLength(250)] // This attribute is used to specify the maximum length of a string property
        public string HowTo { get; set; }

        [Required]
        public string Line { get; set; }

        [Required]
        public string Platform { get; set; }
    }
}
