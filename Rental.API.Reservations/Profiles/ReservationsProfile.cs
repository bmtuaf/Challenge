using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

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
