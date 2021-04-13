using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Rental.API.Reservations.Models.ViewModels
{
    public class Reservation
    {
        public int ID { get; set; }
        public string CPF { get; set; }
        public int VehicleID { get; set; }
        public decimal RentalPricePerHour { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public bool IsCarReturned { get; set; }
        public bool IsCarClean { get; set; }
        public bool IsFuelTankFull { get; set; }
        public bool IsCarDamaged { get; set; }
        public decimal RentalPricePerHourAfterReturn { get; set; }
    }
}
