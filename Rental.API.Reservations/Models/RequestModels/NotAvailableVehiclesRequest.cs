using System;

namespace Rental.API.Reservations.Models.RequestModels
{
    public class NotAvailableVehiclesRequest
    {        
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
    }
}
