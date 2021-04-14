using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Rental.API.Search.Models.RequestModels
{
    public class SearchVehicleAvailability
    {
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
    }
}
