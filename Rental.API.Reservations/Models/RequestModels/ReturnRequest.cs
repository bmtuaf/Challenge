using System.ComponentModel.DataAnnotations;

namespace Rental.API.Reservations.Models.RequestModels
{
    public class ReturnRequest
    {
        [Required]
        public int ID { get; set; }
        [Required]
        public bool IsCarReturned { get; set; }
        [Required]
        public bool IsCarClean { get; set; }
        [Required]
        public bool IsFuelTankFull { get; set; }
        [Required]
        public bool IsCarDamaged { get; set; }
        public decimal RentalPricePerHourAfterReturn { get; set; }
    }
}
