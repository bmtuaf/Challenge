using Rental.API.Vehicles.Models.RequestModels;
using Rental.API.Vehicles.Models.ViewModels;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Rental.API.Vehicles.Interfaces
{
    public interface IVehiclesProvider
    {
        Task<(bool IsSuccess, IEnumerable<Vehicle> Vehicles, string ErrorMessage)> GetVehiclesAsync();
        Task<(bool IsSuccess, Vehicle Vehicle, string ErrorMessage)> GetVehicleAsync(int id);
        Task<(bool IsSuccess, Vehicle Vehicle, string ErrorMessage)> PostVehicleAsync(VehicleRequest vehicle);
        Task<(bool IsSuccess, Vehicle Vehicle, string ErrorMessage)> DeleteVehicleAsync(int id);
        Task<(bool IsSuccess, Vehicle Vehicle, string ErrorMessage)> PutVehicleAsync(VehicleUpdateRequest vehicle);
        Task<(bool IsSuccess, Vehicle Vehicle, string ErrorMessage)> PostReservationVehicleAsync(VehicleReservationRequest vehicleReservationRequest);
    }
}
