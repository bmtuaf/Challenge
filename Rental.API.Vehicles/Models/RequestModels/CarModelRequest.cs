namespace Rental.API.Vehicles.Models.RequestModels
{
    public class CarModelRequest
    {
        public string Name { get; set; }
        public int MakeID { get; set; }
        public int VehicleCategoryID { get; set; }
    }
}
