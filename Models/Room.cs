namespace RaumReservierungsSystem.Models
{
    public class Room
    {
        public int Id { get; set; } // Primärschlüssel
        public int RoomNumber { get; set; }
        public bool IsAvailable { get; set; } = true;
    }
}
