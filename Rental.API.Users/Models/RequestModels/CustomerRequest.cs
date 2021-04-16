using System;
using System.ComponentModel.DataAnnotations;

namespace Rental.API.Users.Models.RequestModels
{
    public class CustomerRequest
    {
        [Required]
        public string CPF { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public DateTime Birthday { get; set; }
        [Required]
        public string CEP { get; set; }
        [Required]
        public string Address { get; set; }
        [Required]
        public int AddressNumber { get; set; }
        public string AdditionalInformation { get; set; }
        [Required]
        public string City { get; set; }
        [Required]
        public string State { get; set; }
        [Required]
        public string Email { get; set; }
        [Required]
        public string Password { get; set; }
        [Required]
        public string Phone { get; set; }
    }
}
