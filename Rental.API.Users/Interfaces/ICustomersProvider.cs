using Rental.API.Users.Models.RequestModels;
using Rental.API.Users.Models.ViewModels;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Rental.API.Users.Interfaces
{
    public interface ICustomersProvider
    {
        Task<(bool IsSuccess, IEnumerable<Customer> Customers, string ErrorMessage)> GetCustomersAsync();
        Task<(bool IsSuccess, Customer Customer, string ErrorMessage)> GetCustomerAsync(string cpf);
        Task<AuthenticationCustomerResult> PostCustomerAsync(CustomerRequest customer);
        Task<(bool IsSuccess, Customer Customer, string ErrorMessage)> DeleteCustomerAsync(int id);
        Task<AuthenticationCustomerResult> PostLoginAsync(LoginRequest login);
    }
}
