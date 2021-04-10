using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Rental.API.Vehicles.Models.RequestModels
{
    public class VehicleRequest
    {
        public int CarModelID { get; set; }
        public string LicensePlate { get; set; }
        public int ModelYear { get; set; }
        public decimal RentalPricePerHour { get; set; }
        public int FuelTypeID { get; set; }
        public int TrunkLimit { get; set; }
    }
}
