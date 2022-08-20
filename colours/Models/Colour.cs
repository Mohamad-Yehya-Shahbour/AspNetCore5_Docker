using System.ComponentModel.DataAnnotations;

namespace colours.Models
{
    public class Colour
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
    }
}
