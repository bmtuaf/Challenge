using System.ComponentModel.DataAnnotations;

namespace Rental.API.Users.Models.RequestModels
{
    public class OperatorUpdateRequest
    {
        [Required]
        public string RegistrationNumber { get; set; }
        public string Name { get; set; }
        public string Password { get; set; }
    }
}
