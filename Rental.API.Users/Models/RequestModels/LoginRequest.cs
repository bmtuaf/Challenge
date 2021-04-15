namespace Rental.API.Users.Models.RequestModels
{
    public class LoginRequest
    {
        public string UserName { get; set; }
        public string Password { get; set; }
        public char UserType { get; set; }
    }
}
