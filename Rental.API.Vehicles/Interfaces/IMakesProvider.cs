using Rental.API.Vehicles.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Rental.API.Vehicles.Interfaces
{
    public interface IMakesProvider
    {
        Task<(bool IsSuccess, IEnumerable<Make> Makes, string ErrorMessage)> GetMakesAsync();
        Task<(bool IsSuccess, Make Make, string ErrorMessage)> GetMakeAsync(int id);
        Task<(bool IsSuccess, Make Make, string ErrorMessage)> PostMakeAsync(string name);
        Task<(bool IsSuccess, Make Make, string ErrorMessage)> DeleteMakeAsync(int id);
        Task<(bool IsSuccess, Make Make, string ErrorMessage)> PutMakeAsync(Make make);
    }
}
