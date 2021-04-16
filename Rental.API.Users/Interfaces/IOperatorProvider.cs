using Rental.API.Users.Models.RequestModels;
using Rental.API.Users.Models.ViewModels;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Rental.API.Users.Interfaces
{
    public interface IOperatorProvider
    {
        Task<(bool IsSuccess, IEnumerable<Operator> Operators, string ErrorMessage)> GetOperatorsAsync();
        Task<(bool IsSuccess, Operator Operator, string ErrorMessage)> GetOperatorAsync(string cpf);
        Task<AuthenticationOperatorResult> PostOperatorAsync(OperatorRequest operatorRequest);
        Task<(bool IsSuccess, Operator Operator, string ErrorMessage)> DeleteOperatorAsync(int id);
        Task<AuthenticationOperatorResult> PostLoginAsync(LoginRequest login);
    }
}
