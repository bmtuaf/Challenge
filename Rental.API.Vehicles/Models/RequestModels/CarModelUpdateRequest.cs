namespace Rental.API.Vehicles.Models.RequestModels
{
    public class CarModelUpdateRequest
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public int MakeID { get; set; }
        public int VehicleCategoryID { get; set; }
    }
}
