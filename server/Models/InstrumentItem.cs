using System.ComponentModel.DataAnnotations;

namespace JaMoveo.Models
{
    public class InstrumentItem
    {
        [Key]
        [Required]
        public string Name { get; set; }
    }
}
