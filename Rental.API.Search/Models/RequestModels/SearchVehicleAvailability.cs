using System;
using System.ComponentModel.DataAnnotations;

namespace Rental.API.Orchestrator.Models.RequestModels
{
    public class SearchVehicleAvailability
    {
        [Required]
        public DateTime StartDate { get; set; }
        [Required]
        public DateTime EndDate { get; set; }
    }
}
