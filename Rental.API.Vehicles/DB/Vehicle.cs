using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Rental.API.Vehicles.DB
{
    public class Vehicle
    {
        public int ID { get; set; }
        public int MakeID { get; set; }
        public int ModelID { get; set; }
        public string Plate { get; set; }
        public int ModelYear { get; set; }
        public decimal RentalPrice { get; set; }
        public int FuelTypeID { get; set; }
        public int TrunkLimit { get; set; }
        public int CategoryID { get; set; }

    }
}
