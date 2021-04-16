namespace Rental.API.Orchestrator.Models.ViewModels
{
    public class AvailableCarModels
    {        
        public int ID { get; set; }
        public string Name { get; set; }
        public int TrunkLimit { get; set; }
        public decimal RentalPricePerHour { get; set; }
        public decimal RentalDailyPrice { get { return RentalPricePerHour * 24; } }
        public string ImagePath { get; set; }
        public virtual Make Make { get; set; }
        public virtual VehicleCategory VehicleCategory { get; set; }
        public virtual FuelType FuelType { get; set; }
    }
}
