using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Rental.API.Vehicles.Profiles
{
    public class VehicleCategoryProfile : AutoMapper.Profile
    {
        public VehicleCategoryProfile()
        {
            CreateMap<DB.VehicleCategory, Models.VehicleCategory>();
        }
    }
}
