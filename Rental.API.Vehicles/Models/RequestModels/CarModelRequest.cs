namespace Rental.API.Vehicles.Models.RequestModels
{
    public class CarModelRequest
    {
        public string Name { get; set; }
        public int MakeID { get; set; }
        public int VehicleCategoryID { get; set; }
        public int FuelTypeID { get; set; }
        public int TrunkLimit { get; set; }
        public decimal RentalPricePerHour { get; set; }
    }
}
