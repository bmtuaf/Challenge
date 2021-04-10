using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Rental.API.Vehicles.Profiles
{
    public class VehicleProfile : AutoMapper.Profile
    {
        public VehicleProfile()
        {
            CreateMap<DB.Vehicle, Models.Vehicle>();
        }
    }
}
