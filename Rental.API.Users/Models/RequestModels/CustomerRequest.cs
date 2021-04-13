using System;

namespace Rental.API.Users.Models.RequestModels
{
    public class CustomerRequest
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
        public string Email { get; set; }
        public string Password { get; set; }
        public string Phone { get; set; }
    }
}
