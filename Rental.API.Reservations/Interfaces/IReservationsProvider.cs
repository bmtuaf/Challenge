using Rental.API.Reservations.Models.RequestModels;
using Rental.API.Reservations.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Rental.API.Reservations.Interfaces
{
    public interface IReservationsProvider
    {
        Task<(bool IsSuccess, IEnumerable<Reservation> Reservations, string ErrorMessage)> GetReservationsAsync();
        Task<(bool IsSuccess, Reservation Reservation, string ErrorMessage)> GetReservationAsync(int id);
        Task<(bool IsSuccess, IEnumerable<Reservation> Reservations, string ErrorMessage)> GetUsersActiveReservationsAsync(string cpf);
        Task<(bool IsSuccess, IEnumerable<Reservation> Reservations, string ErrorMessage)> GetUsersHistoricalReservationsAsync(string cpf);
        Task<(bool IsSuccess, Reservation Reservation, string ErrorMessage)> PostReservationAsync(ReservationRequest reservation);
        Task<(bool IsSuccess, NotAvailableVehicles NotAvailableVehicles, string ErrorMessage)> GetNotAvailableVehiclesAsync(NotAvailableVehiclesRequest notAvailable);
        Task<(bool IsSuccess, Reservation Reservation, string ErrorMessage)> PostVehicleReturnAsync(ReturnRequest returnRequest);
    }
}
