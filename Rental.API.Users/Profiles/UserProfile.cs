namespace Rental.API.Users.Profiles
{
    public class UserProfile : AutoMapper.Profile
    {
        public UserProfile()
        {
            CreateMap<DB.User, Models.ViewModels.User>();
            CreateMap<DB.User, Models.ViewModels.Operator>();
        }
    }
}
