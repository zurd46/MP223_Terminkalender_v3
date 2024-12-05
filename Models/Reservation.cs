using System;

namespace RaumReservierungsSystem.Models
{
    public class Reservation
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public DateTime Date { get; set; }
        public TimeSpan StartTime { get; set; }
        public TimeSpan EndTime { get; set; }
        public int RoomNumber { get; set; }
        public string Remarks { get; set; }
        public string Participants { get; set; }

        public string PrivateKey { get; set; } = Guid.NewGuid().ToString();
        public string PublicKey { get; set; } = Guid.NewGuid().ToString();
    }
}
