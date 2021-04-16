using System.ComponentModel.DataAnnotations;

namespace Rental.API.Vehicles.Models.RequestModels
{
    public class CarModelRequest
    {
        [Required]
        public string Name { get; set; }
        [Required]
        public int MakeID { get; set; }
        [Required]
        public int VehicleCategoryID { get; set; }
        [Required]
        public int FuelTypeID { get; set; }
        [Required]
        public int TrunkLimit { get; set; }
        [Required]
        public decimal RentalPricePerHour { get; set; }
    }
}
