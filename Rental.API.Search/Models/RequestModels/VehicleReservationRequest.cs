using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Rental.API.Orchestrator.Models.RequestModels
{
    public class VehicleReservationRequest
    {
        public int CarModelID { get; set; }
        public List<int> notAvailableVehicles { get; set; }
    }
}
