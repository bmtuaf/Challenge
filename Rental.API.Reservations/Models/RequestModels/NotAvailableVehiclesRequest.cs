using System;
using System.ComponentModel.DataAnnotations;

namespace Rental.API.Reservations.Models.RequestModels
{
    public class NotAvailableVehiclesRequest
    {
        [Required]
        public DateTime StartDate { get; set; }
        [Required]
        public DateTime EndDate { get; set; }
    }
}
