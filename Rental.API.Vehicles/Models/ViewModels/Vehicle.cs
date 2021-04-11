namespace Rental.API.Vehicles.Models.ViewModels
{
    public class Vehicle
    {
        public int ID { get; set; }
        public string LicensePlate { get; set; }
        public int ModelYear { get; set; }
        public decimal RentalPricePerHour { get; set; }
        public int TrunkLimit { get; set; }
        public virtual FuelType FuelType { get; set; }
        public virtual CarModel CarModel { get; set; }        
    }
}
