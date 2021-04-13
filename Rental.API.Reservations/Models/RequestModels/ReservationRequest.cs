using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Rental.API.Reservations.Models.RequestModels
{
    public class ReservationRequest
    {
        public string CPF { get; set; }
        public int VehicleID { get; set; }
        public decimal RentalPricePerHour { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
    }
}
