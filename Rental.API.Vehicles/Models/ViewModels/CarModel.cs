namespace Rental.API.Vehicles.Models.ViewModels
{
    public class CarModel
    {
        public int ID { get; set; }        
        public string Name { get; set; }
        public int TrunkLimit { get; set; }
        public decimal RentalPricePerHour { get; set; }
        public string ImagePath { get; set; }
        public virtual Make Make { get; set; }
        public virtual VehicleCategory VehicleCategory { get; set; }
        public virtual FuelType FuelType { get; set; }
    }
}
