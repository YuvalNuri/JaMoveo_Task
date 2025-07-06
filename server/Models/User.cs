using System.ComponentModel.DataAnnotations;

namespace JaMoveo.Models
{
    public class User
    {
        [Key]
        [Required, MaxLength(50)]
        public string Username { get; set; }

        [Required]
        public byte[] PasswordHash { get; set; }

        [Required]
        public byte[] PasswordSalt { get; set; }

        public InstrumentItem Instrument { get; set; }

        public bool IsAdmin { get; set; }
    }
}
