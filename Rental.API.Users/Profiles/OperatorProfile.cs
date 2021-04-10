using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Rental.API.Users.Profiles
{
    public class OperatorProfile : AutoMapper.Profile
    {
        public OperatorProfile()
        {
            CreateMap<DB.Operator, Models.ViewModels.Operator>();
        }
    }
}
