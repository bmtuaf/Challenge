namespace Rental.API.Vehicles.Models.ViewModels
{
    public class Vehicle
    {
        public int ID { get; set; }
        public string LicensePlate { get; set; }
        public int ModelYear { get; set; }
        public virtual CarModel CarModel { get; set; }        
    }
}
