using Microsoft.AspNetCore.Identity;
using System;

namespace Rental.API.Users.DB
{
    public class User : IdentityUser
    {       
        public string CPF { get; set; }
        public string Name { get; set; }
        public DateTime Birthday { get; set; }
        public string CEP { get; set; }
        public string Address { get; set; }
        public int AddressNumber { get; set; }
        public string AdditionalInformation { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string RegistrationNumber { get; set; }
        public string Phone { get; set; }
    }
}
