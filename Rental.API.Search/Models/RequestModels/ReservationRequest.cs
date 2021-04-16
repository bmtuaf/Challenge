using System;
using System.ComponentModel.DataAnnotations;

namespace Rental.API.Orchestrator.Models.RequestModels
{
    public class ReservationRequest
    {
        [Required]
        public string CPF { get; set; }
        [Required]
        public int CarModelID { get; set; }
        [Required]
        public DateTime StartDate { get; set; }
        [Required]
        public DateTime EndDate { get; set; }
    }
}
