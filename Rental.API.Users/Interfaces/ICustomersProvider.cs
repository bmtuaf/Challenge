using Rental.API.Users.Models.RequestModels;
using Rental.API.Users.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Rental.API.Users.Interfaces
{
    public interface ICustomersProvider
    {
        Task<(bool IsSuccess, IEnumerable<Customer> Vehicles, string ErrorMessage)> GetCustomersAsync();
        Task<(bool IsSuccess, Customer Customer, string ErrorMessage)> GetCustomerAsync(string cpf);
        Task<(bool IsSuccess, Customer Customer, string ErrorMessage)> PostVehicleAsync(CustomerRequest customer);
        Task<(bool IsSuccess, Customer Customer, string ErrorMessage)> DeleteVehicleAsync(int id);
        Task<(bool IsSuccess, Customer Customer, string ErrorMessage)> PutVehicleAsync(CustomerUpdateRequest vehicle);
    }
}
