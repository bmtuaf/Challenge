using System;
using System.ComponentModel.DataAnnotations;

namespace Rental.API.Reservations.Models.RequestModels
{
    public class ReservationRequest
    {
        [Required]
        public string CPF { get; set; }
        [Required]
        public int VehicleID { get; set; }
        [Required]
        public decimal RentalPricePerHour { get; set; }
        [Required]
        public DateTime StartDate { get; set; }
        [Required]
        public DateTime EndDate { get; set; }
    }
}
