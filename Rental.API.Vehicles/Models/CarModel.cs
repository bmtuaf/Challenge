using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Rental.API.Vehicles.Models
{
    public class CarModel
    {
        public int ID { get; set; }        
        public string Name { get; set; }
        public virtual Make Make { get; set; }
        public virtual VehicleCategory VehicleCategory { get; set; }
    }
}
