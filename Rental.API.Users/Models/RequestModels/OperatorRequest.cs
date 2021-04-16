using System.ComponentModel.DataAnnotations;

namespace Rental.API.Users.Models.RequestModels
{
    public class OperatorRequest
    {
        [Required]
        public string RegistrationNumber { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Password { get; set; }
    }
}
