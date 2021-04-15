using Rental.API.Orchestrator.Models.RequestModels;
using Rental.API.Orchestrator.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Rental.API.Orchestrator.Interfaces
{
    public interface IVehiclesService
    {
        Task<(bool IsSuccess, IEnumerable<AvailableCarModels> AvailableCarModels, string ErrorMessage)> GetAvailableCarModelsAsync(List<int> search);
        Task<(bool IsSuccess, Vehicle Vehicle, string ErrorMessage)> PostReservationVehicleAsync(VehicleReservationRequest reservationRequest);
    }
}
