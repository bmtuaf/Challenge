using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Rental.API.Vehicles.Models.RequestModels
{
    public class CarModelRequest
    {
        public string Name { get; set; }
        public int MakeID { get; set; }
        public int VehicleCategoryID { get; set; }
    }
}
