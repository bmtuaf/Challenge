using Rental.API.Orchestrator.Interfaces;
using Rental.API.Orchestrator.Models.RequestModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Rental.API.Orchestrator.Services
{
    public class ReservationOrchestratorService : IReservationOrchestratorService
    {
        private readonly IReservationsService reservationsService;
        private readonly IVehiclesService vehiclesService;

        public ReservationOrchestratorService(IReservationsService reservationsService, IVehiclesService vehiclesService)
        {
            this.reservationsService = reservationsService;
            this.vehiclesService = vehiclesService;
        }

        public async Task<(bool IsSuccess, string RentalContract)> PostReservationAsync(ReservationRequest request)
        {
            var notAvailableResults = await reservationsService.GetNotAvailableVehiclesAsync(new Models.RequestModels.SearchVehicleAvailability() { StartDate = request.StartDate, EndDate = request.EndDate });
            
            if (!notAvailableResults.IsSuccess)
            {
                return (false, null);
            }

            var reservationVehicle = await vehiclesService.PostReservationVehicleAsync(new VehicleReservationRequest() { CarModelID = request.CarModelID, notAvailableVehicles = notAvailableResults.VehiclesNotAvailable.LstNotAvailableVehicles });
            
            if (!reservationVehicle.IsSuccess)
            {
                return (false, null);
            }
            
            var reservation = await reservationsService.PostReservationAsync(new ReservationPersistRequest() { CPF = request.CPF, StartDate = request.StartDate, EndDate = request.EndDate, VehicleID = reservationVehicle.Vehicle.ID, RentalPricePerHour = reservationVehicle.Vehicle.CarModel.RentalPricePerHour });
            
            if (reservation.IsSuccess)
            {
                string contrato = $"Número da reserva {reservation.Reservation.ID}.\n\nEu, {request.CPF}, estou locando o veiculo {reservationVehicle.Vehicle.CarModel.Name}, placa {reservationVehicle.Vehicle.LicensePlate}, durante o período de {request.StartDate} até {request.EndDate}, pelo valor total de R${reservationVehicle.Vehicle.CarModel.RentalPricePerHour * (decimal)(request.EndDate - request.StartDate).TotalHours}.";
                return (true, contrato);
            }
            return (false, null);

        }
    }
}
