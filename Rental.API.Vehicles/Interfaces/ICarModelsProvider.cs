﻿using Rental.API.Vehicles.Models.RequestModels;
using Rental.API.Vehicles.Models.ViewModels;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Rental.API.Vehicles.Interfaces
{
    public interface ICarModelsProvider
    {
        Task<(bool IsSuccess, IEnumerable<CarModel> CarModels, string ErrorMessage)> GetCarModelsAsync();
        Task<(bool IsSuccess, CarModel CarModel, string ErrorMessage)> GetCarModelAsync(int id);
        Task<(bool IsSuccess, CarModel CarModel, string ErrorMessage)> PostCarModelAsync(CarModelRequest carModel);
        Task<(bool IsSuccess, CarModel CarModel, string ErrorMessage)> DeleteCarModelAsync(int id);
        Task<(bool IsSuccess, CarModel CarModel, string ErrorMessage)> PutCarModelAsync(CarModelUpdateRequest carModel);
    }
}
