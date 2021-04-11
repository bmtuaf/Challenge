using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Rental.API.Users.Models.RequestModels
{
    public class LoginRequest
    {
        public string UserName { get; set; }
        public string Password { get; set; }
    }
}
