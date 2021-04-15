using Rental.API.Orchestrator.Models.RequestModels;
using Rental.API.Orchestrator.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Rental.API.Orchestrator.Interfaces
{
    public interface IReservationsService
    {
        Task<(bool IsSuccess, IEnumerable<Reservation> Reservations, string ErrorMessage)> GetUsersReservationsAsync(SearchUserReservation search);
        Task<(bool IsSuccess, IEnumerable<Reservation> Reservations, string ErrorMessage)> GetUsersHistoricalReservationsAsync(SearchUserReservation search);
        Task<(bool IsSuccess, NotAvailableVehicles VehiclesNotAvailable, string ErrorMessage)> GetNotAvailableVehiclesAsync(SearchVehicleAvailability search);
        Task<(bool IsSuccess, Reservation Reservation, string ErrorMessage)> PostReservationAsync(ReservationPersistRequest reservation);
    }
}
