using Rental.API.Vehicles.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Rental.API.Vehicles.Interfaces
{
    public interface ICarModelsProvider
    {
        Task<(bool IsSuccess, IEnumerable<CarModel> CarModels, string ErrorMessage)> GetCarModelsAsync();
        Task<(bool IsSuccess, CarModel CarModel, string ErrorMessage)> GetCarModelAsync(int id);
    }
}
