namespace Rental.API.Vehicles.Profiles
{
    public class VehicleCategoryProfile : AutoMapper.Profile
    {
        public VehicleCategoryProfile()
        {
            CreateMap<DB.VehicleCategory, Models.ViewModels.VehicleCategory>();
        }
    }
}
