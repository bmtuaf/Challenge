using Microsoft.EntityFrameworkCore;

namespace Rental.API.Reservations.DB
{
    public class ReservationsDbContext : DbContext
    {
        public DbSet<Reservation> Reservations { get; set; }
        public ReservationsDbContext(DbContextOptions options) : base (options)
        {

        }
    }
}
