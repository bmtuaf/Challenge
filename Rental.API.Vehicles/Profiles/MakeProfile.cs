namespace Rental.API.Vehicles.Profiles
{
    public class MakeProfile : AutoMapper.Profile
    {
        public MakeProfile()
        {
            CreateMap<DB.Make, Models.ViewModels.Make>();
        }
    }
}
