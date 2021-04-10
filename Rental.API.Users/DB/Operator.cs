using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Rental.API.Users.DB
{
    public class Operator
    {
        [Key]
        public string RegistrationNumber { get; set; }
        public string Name { get; set; }
        public string PasswordHash { get; set; }
    }
}
