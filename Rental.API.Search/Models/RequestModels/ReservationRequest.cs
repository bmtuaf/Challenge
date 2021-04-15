using System;

namespace Rental.API.Orchestrator.Models.RequestModels
{
    public class ReservationRequest
    {
        public string CPF { get; set; }
        public int CarModelID { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
    }
}
