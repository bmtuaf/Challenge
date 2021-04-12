namespace Rental.API.Users.Profiles
{
    public class UserProfile : AutoMapper.Profile
    {
        public UserProfile()
        {
            CreateMap<DB.User, Models.ViewModels.Customer>();
            CreateMap<DB.User, Models.ViewModels.Operator>();
        }
    }
}
