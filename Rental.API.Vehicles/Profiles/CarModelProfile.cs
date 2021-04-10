using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

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
