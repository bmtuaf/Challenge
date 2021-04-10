using Rental.API.Vehicles.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Rental.API.Vehicles.Interfaces
{
    public interface IFuelTypesProvider
    {
        Task<(bool IsSuccess, IEnumerable<FuelType> FuelTypes, string ErrorMessage)> GetFuelTypesAsync();
        Task<(bool IsSuccess, FuelType FuelType, string ErrorMessage)> GetFuelTypeAsync(int id);
    }
}
