using Microsoft.EntityFrameworkCore;

namespace Rental.API.Vehicles.DB
{
    public class VehiclesDBContext : DbContext
    {
        public DbSet<Vehicle> Vehicles { get; set; }
        public DbSet<Make> Makes { get; set; }
        public DbSet<CarModel> CarModels { get; set; }
        public DbSet<VehicleCategory> VehicleCategories { get; set; }
        public DbSet<FuelType> FuelTypes { get; set; }

        public VehiclesDBContext(DbContextOptions options) : base (options)
        {

        }
    }
}
