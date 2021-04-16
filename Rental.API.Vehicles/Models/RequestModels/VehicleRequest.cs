using System.ComponentModel.DataAnnotations;

namespace Rental.API.Vehicles.Models.RequestModels
{
    public class VehicleRequest
    {
        [Required]
        public int CarModelID { get; set; }
        [Required]
        public string LicensePlate { get; set; }
        [Required]
        public int ModelYear { get; set; }
    }
}
