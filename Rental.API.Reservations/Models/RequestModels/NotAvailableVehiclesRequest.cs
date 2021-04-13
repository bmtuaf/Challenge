using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Rental.API.Reservations.Models.RequestModels
{
    public class NotAvailableVehiclesRequest
    {        
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
    }
}
