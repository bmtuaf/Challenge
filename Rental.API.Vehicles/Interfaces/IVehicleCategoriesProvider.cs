using Rental.API.Vehicles.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Rental.API.Vehicles.Interfaces
{
    public interface IVehicleCategoriesProvider
    {
        Task<(bool IsSuccess, IEnumerable<VehicleCategory> VehicleCategories, string ErrorMessage)> GetVehicleCategoriesAsync();
        Task<(bool IsSuccess, VehicleCategory VehicleCategory, string ErrorMessage)> GetVehicleCategoryAsync(int id);
    }
}
