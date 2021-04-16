namespace Rental.API.Users.Models.ViewModels
{
    public class AuthenticationOperatorResult
    {
        public bool IsSuccess { get; set; }
        public Operator Operator { get; set; }
        public string ErrorMessage { get; set; }
        public string Token { get; set; }
    }
}
