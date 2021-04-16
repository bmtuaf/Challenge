using System.ComponentModel.DataAnnotations;

namespace Rental.API.Vehicles.Models.RequestModels
{
    public class VehicleUpdateRequest
    {
        [Required]
        public int ID { get; set; }
        public int CarModelID { get; set; }
        public string LicensePlate { get; set; }
        public int ModelYear { get; set; }
        public decimal RentalPricePerHour { get; set; }
        public int FuelTypeID { get; set; }
        public int TrunkLimit { get; set; }
    }
}
