using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

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
