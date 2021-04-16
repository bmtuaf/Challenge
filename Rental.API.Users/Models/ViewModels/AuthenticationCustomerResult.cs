namespace Rental.API.Users.Models.ViewModels
{
    public class AuthenticationCustomerResult
    {
        public bool IsSuccess { get; set; }
        public Customer Customer { get; set; }        
        public string ErrorMessage { get; set; }
        public string Token { get; set; }        
    }
}
