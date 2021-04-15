using System;

namespace Rental.API.Orchestrator.Models.RequestModels
{
    public class SearchVehicleAvailability
    {
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
    }
}
