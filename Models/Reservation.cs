using System;
using System.ComponentModel.DataAnnotations;

namespace RaumReservierungsSystem.Models
{
    public class Reservation
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        
        [Required]
        public DateTime Date { get; set; }

        [Required]
        public TimeSpan StartTime { get; set; }

        [Required]
        public TimeSpan EndTime { get; set; }

        [Required]
        public int RoomNumber { get; set; }

        [Required]
        [MinLength(10, ErrorMessage = "Die Bemerkung muss mindestens 10 Zeichen enthalten.")]
        [MaxLength(200, ErrorMessage = "Die Bemerkung darf maximal 200 Zeichen enthalten.")]
        [RegularExpression(@"^[a-zA-Z0-9\s]+$", ErrorMessage = "Die Bemerkung darf nur alphanumerische Zeichen enthalten.")]
        public string Remarks { get; set; }

        [Required]
        public string Participants { get; set; }

        public string PrivateKey { get; set; } = Guid.NewGuid().ToString();
        public string PublicKey { get; set; } = Guid.NewGuid().ToString();
    }
}
