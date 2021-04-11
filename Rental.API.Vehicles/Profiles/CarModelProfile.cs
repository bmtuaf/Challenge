namespace Rental.API.Vehicles.Profiles
{
    public class CarModelProfile : AutoMapper.Profile
    {
        public CarModelProfile()
        {
            CreateMap<DB.CarModel, Models.ViewModels.CarModel>();
        }
    }
}
