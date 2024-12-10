using Microsoft.EntityFrameworkCore;
using RaumReservierungsSystem.Models;

namespace RaumReservierungsSystem.Data
{
    public class ApplicationDbContext : DbContext
    {
        public DbSet<Reservation> Reservations { get; set; }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }
    }
}
