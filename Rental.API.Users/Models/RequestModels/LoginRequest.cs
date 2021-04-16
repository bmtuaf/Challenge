using System.ComponentModel.DataAnnotations;

namespace Rental.API.Users.Models.RequestModels
{
    public class LoginRequest
    {
        [Required]
        public string UserName { get; set; }
        [Required]
        public string Password { get; set; }
        [Required]
        public char UserType { get; set; }
    }
}
