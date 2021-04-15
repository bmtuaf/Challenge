using System.Collections.Generic;

namespace Rental.API.Orchestrator.Models.RequestModels
{
    public class VehicleReservationRequest
    {
        public int CarModelID { get; set; }
        public List<int> notAvailableVehicles { get; set; }
    }
}
