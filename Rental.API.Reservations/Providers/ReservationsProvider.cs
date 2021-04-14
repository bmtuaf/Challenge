using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Rental.API.Reservations.DB;
using Rental.API.Reservations.Interfaces;
using Rental.API.Reservations.Models.RequestModels;
using Rental.API.Reservations.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Rental.API.Reservations.Providers
{
    public class ReservationsProvider : IReservationsProvider
    {
        private readonly ReservationsDbContext dBContext;
        private readonly ILogger<ReservationsProvider> logger;
        private readonly IMapper mapper;

        public ReservationsProvider(ReservationsDbContext dBContext, ILogger<ReservationsProvider> logger, IMapper mapper)
        {
            this.dBContext = dBContext;
            this.logger = logger;
            this.mapper = mapper;
        }

        public async Task<(bool IsSuccess, NotAvailableVehicles NotAvailableVehicles, string ErrorMessage)> GetNotAvailableVehiclesAsync(NotAvailableVehiclesRequest notAvailable)
        {
            try
            {
                var notAvailableVehicles = await dBContext.Reservations
                                        .Where(x => (x.StartDate <= notAvailable.StartDate && x.EndDate >= notAvailable.StartDate) ||
                                                    (x.StartDate <= notAvailable.EndDate && x.EndDate >= notAvailable.EndDate) ||
                                                    (x.StartDate >= notAvailable.StartDate && x.EndDate <= notAvailable.EndDate)) 
                                        .Select(x => x.VehicleID)
                                        .ToListAsync();

                var result = new NotAvailableVehicles() { LstNotAvailableVehicles = notAvailableVehicles };

                if (notAvailableVehicles != null && notAvailableVehicles.Any())
                {                    
                    return (true, result, null);
                }
                return (false, null, "Not found");
            }
            catch (Exception ex)
            {
                logger?.LogError(ex.ToString());
                return (false, null, ex.Message);
            }
        }

        public async Task<(bool IsSuccess, Models.ViewModels.Reservation Reservation, string ErrorMessage)> GetReservationAsync(int id)
        {
            try
            {
                var reservation = await dBContext.Reservations.FirstOrDefaultAsync(r => r.ID == id);

                if (reservation != null)
                {
                    var result = mapper.Map<DB.Reservation, Models.ViewModels.Reservation>(reservation);
                    return (true, result, null);
                }
                return (false, null, "Not found");
            }
            catch (Exception ex)
            {
                logger?.LogError(ex.ToString());
                return (false, null, ex.Message);
            }
        }

        public async Task<(bool IsSuccess, IEnumerable<Models.ViewModels.Reservation> Reservations, string ErrorMessage)> GetReservationsAsync()
        {
            try
            {
                var reservations = await dBContext.Reservations.ToListAsync();
                if (reservations != null && reservations.Any())
                {
                    var result = mapper.Map<IEnumerable<DB.Reservation>, IEnumerable<Models.ViewModels.Reservation>>(reservations);
                    return (true, result, null);
                }
                return (false, null, "Not found");
            }
            catch (Exception ex)
            {
                logger?.LogError(ex.ToString());
                return (false, null, ex.Message);
            }
        }

        public async Task<(bool IsSuccess, IEnumerable<Models.ViewModels.Reservation> Reservations, string ErrorMessage)> GetUsersActiveReservationsAsync(string cpf)
        {
            try
            {
                var reservations = await dBContext.Reservations.Where(r => r.CPF == cpf && r.IsCarReturned == false).ToListAsync();
                if (reservations != null && reservations.Any())
                {
                    var result = mapper.Map<IEnumerable<DB.Reservation>, IEnumerable<Models.ViewModels.Reservation>>(reservations);
                    return (true, result, null);
                }
                return (false, null, "Not found");
            }
            catch (Exception ex)
            {
                logger?.LogError(ex.ToString());
                return (false, null, ex.Message);
            }
        }

        public async Task<(bool IsSuccess, IEnumerable<Models.ViewModels.Reservation> Reservations, string ErrorMessage)> GetUsersHistoricalReservationsAsync(string cpf)
        {
            try
            {
                var reservations = await dBContext.Reservations.Where(r => r.CPF == cpf && r.IsCarReturned == true).ToListAsync();
                if (reservations != null && reservations.Any())
                {
                    var result = mapper.Map<IEnumerable<DB.Reservation>, IEnumerable<Models.ViewModels.Reservation>>(reservations);
                    return (true, result, null);
                }
                return (false, null, "Not found");
            }
            catch (Exception ex)
            {
                logger?.LogError(ex.ToString());
                return (false, null, ex.Message);
            }
        }

        public async Task<(bool IsSuccess, Models.ViewModels.Reservation Reservation, string ErrorMessage)> PostReservationAsync(ReservationRequest reservation)
        {
            try
            {
                var newReservation = new DB.Reservation()
                {
                    CPF = reservation.CPF,
                    VehicleID = reservation.VehicleID,
                    RentalPricePerHour = reservation.RentalPricePerHour,
                    StartDate = reservation.StartDate,
                    EndDate = reservation.EndDate,
                    IsCarReturned = false,
                    IsCarClean = true,
                    IsCarDamaged = false,
                    IsFuelTankFull = true,
                    RentalPricePerHourAfterReturn = reservation.RentalPricePerHour
                };
                dBContext.Add(newReservation);
                if (await dBContext.SaveChangesAsync() > 0)
                {
                    newReservation = await dBContext.Reservations.FirstOrDefaultAsync(r => r.ID == newReservation.ID);
                    var result = mapper.Map<DB.Reservation, Models.ViewModels.Reservation>(newReservation);
                    return (true, result, null);
                }
                return (false, null, "Failed to insert record.");
            }
            catch (Exception ex)
            {
                logger?.LogError(ex.ToString());
                return (false, null, ex.Message);
            }
        }

        public async Task<(bool IsSuccess, Models.ViewModels.Reservation Reservation, string ErrorMessage)> PostVehicleReturnAsync(ReturnRequest returnRequest)
        {
            try
            {
                decimal additionalCost = 1;

                if (!returnRequest.IsCarClean)
                {
                    additionalCost += (decimal)0.3;
                }
                if (!returnRequest.IsFuelTankFull)
                {
                    additionalCost += (decimal)0.3;
                }
                if (returnRequest.IsCarDamaged)
                {
                    additionalCost += (decimal)0.3;
                }

                var reservation = await dBContext.Reservations.FirstOrDefaultAsync(r => r.ID == returnRequest.ID);
                reservation.IsCarClean = returnRequest.IsCarClean;
                reservation.IsCarDamaged = returnRequest.IsCarDamaged;
                reservation.IsFuelTankFull = returnRequest.IsFuelTankFull;
                reservation.IsCarReturned = true;
                reservation.RentalPricePerHourAfterReturn = reservation.RentalPricePerHour * additionalCost;

                dBContext.Update(reservation);
                if (await dBContext.SaveChangesAsync() > 0)
                {
                    var result = mapper.Map<DB.Reservation, Models.ViewModels.Reservation>(reservation);
                    return (true, result, null);
                }
                return (false, null, "Failed to insert record.");
            }
            catch (Exception ex)
            {
                logger?.LogError(ex.ToString());
                return (false, null, ex.Message);
            }
        }
    }
}
