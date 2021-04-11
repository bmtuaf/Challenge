namespace Rental.API.Vehicles.Profiles
{
    public class FuelTypeProfile : AutoMapper.Profile
    {
        public FuelTypeProfile()
        {
            CreateMap<DB.FuelType, Models.ViewModels.FuelType>();
        }
    }
}
