using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Rental.API.Users.DB
{
    public class Customer
    {
        [Key]
        public string CPF { get; set; }
        public string Name { get; set; }
        public DateTime Birthday { get; set; }
        public string CEP { get; set; }
        public string Address { get; set; }
        public int AddressNumber { get; set; }
        public string AdditionalInformation { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string PasswordHash { get; set; }
    }
}
