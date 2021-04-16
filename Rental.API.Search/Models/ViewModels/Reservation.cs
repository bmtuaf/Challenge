using System;

namespace Rental.API.Orchestrator.Models.ViewModels
{
    public class Reservation
    {
        public int ID { get; set; }
        public string CPF { get; set; }
        public int VehicleID { get; set; }
        public decimal RentalPricePerHour { get; set; }
        public decimal RentalDailyPrice { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public decimal RentalTotalPrice { get; set; }
    }
}
