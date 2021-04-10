using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Rental.API.Users.Models.RequestModels
{
    public class OperatorRequest
    { 
        public string RegistrationNumber { get; set; }
        public string Name { get; set; }
        public string PasswordHash { get; set; }
    }
}
