namespace Rental.API.Reservations.Profiles
{
    public class ReservationsProfile : AutoMapper.Profile
    {
        public ReservationsProfile()
        {
            CreateMap<DB.Reservation, Models.ViewModels.Reservation>();
        }
    }
}
