using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Rental.API.Vehicles.DB
{
    public class VehiclesDBContext : DbContext
    {
        public DbSet<Vehicle> Vehicles { get; set; }
        public DbSet<Make> Makes { get; set; }
        public DbSet<CarModel> CarModels { get; set; }

        public VehiclesDBContext(DbContextOptions options) : base (options)
        {

        }
    }
}
