using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Rental.API.Orchestrator.Models.RequestModels
{
    public class VehicleReservationRequest
    {
        [Required]
        public int CarModelID { get; set; }
        [Required]
        public List<int> notAvailableVehicles { get; set; }
    }
}
