using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Rental.API.Reservations.Models.RequestModels
{
    public class ReturnRequest
    {
        public int ID { get; set; }
        public bool IsCarReturned { get; set; }
        public bool IsCarClean { get; set; }
        public bool IsFuelTankFull { get; set; }
        public bool IsCarDamaged { get; set; }
        public decimal RentalPricePerHourAfterReturn { get; set; }
    }
}
