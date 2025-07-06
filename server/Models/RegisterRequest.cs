using System.ComponentModel.DataAnnotations;

namespace JaMoveo.Models
{
    public class RegisterRequest
    {
        [Required, MaxLength(50)]
        public string Username { get; set; }

        [Required, MinLength(6)]
        public string Password { get; set; }

        [Required]
        public InstrumentItem Instrument { get; set; }
    }
}
