using System;

namespace Rental.API.Orchestrator.Models.RequestModels
{
    public class ReservationPersistRequest
    {
        public string CPF { get; set; }
        public int VehicleID { get; set; }
        public decimal RentalPricePerHour { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
    }
}
