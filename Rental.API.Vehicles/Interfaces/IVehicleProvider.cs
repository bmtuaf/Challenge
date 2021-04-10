using Rental.API.Vehicles.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Rental.API.Vehicles.Interfaces
{
    public interface IVehicleProvider
    {
        Task<(bool IsSuccess, IEnumerable<Vehicle> Vehicles, string ErrorMessage)> GetVehiclesAsync();
        Task<(bool IsSuccess, Vehicle Vehicle, string ErrorMessage)> GetVehicleAsync(int id);
    }
}
