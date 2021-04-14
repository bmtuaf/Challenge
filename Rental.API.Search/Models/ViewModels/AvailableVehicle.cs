using Rental.API.Vehicles.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Rental.API.Search.Models.ViewModels
{
    public class AvailableVehicle
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public int TrunkLimit { get; set; }
        public decimal RentalPricePerHour { get; set; }
        public decimal RentalDailyPrice { get; set; }
        public decimal RentalTotalPrice { get; set; }
        public string ImagePath { get; set; }
        public virtual Make Make { get; set; }
        public virtual VehicleCategory VehicleCategory { get; set; }
        public virtual FuelType FuelType { get; set; }
    }
}
