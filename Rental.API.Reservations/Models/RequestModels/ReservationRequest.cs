using System;

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
