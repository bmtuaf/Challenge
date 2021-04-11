namespace Rental.API.Vehicles.Profiles
{
    public class VehicleProfile : AutoMapper.Profile
    {
        public VehicleProfile()
        {
            CreateMap<DB.Vehicle, Models.ViewModels.Vehicle>();
        }
    }
}
