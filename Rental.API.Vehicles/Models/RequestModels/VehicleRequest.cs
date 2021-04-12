namespace Rental.API.Vehicles.Models.RequestModels
{
    public class VehicleRequest
    {
        public int CarModelID { get; set; }
        public string LicensePlate { get; set; }
        public int ModelYear { get; set; }
    }
}
