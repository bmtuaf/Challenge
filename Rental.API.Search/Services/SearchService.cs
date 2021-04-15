using Rental.API.Orchestrator.Interfaces;
using Rental.API.Orchestrator.Models.RequestModels;
using Rental.API.Orchestrator.Models.ViewModels;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Rental.API.Orchestrator.Services
{
    public class SearchService : ISearchService
    {
        private readonly IReservationsService reservationsService;
        private readonly IVehiclesService vehiclesService;

        public SearchService(IReservationsService reservationsService, IVehiclesService vehiclesService)
        {
            this.reservationsService = reservationsService;
            this.vehiclesService = vehiclesService;
        }

        public async Task<(bool IsSuccess, dynamic SearchResults)> SearchUserActiveReservationsAsync(SearchUserReservation search)
        {
            var reservationsResult = await reservationsService.GetUsersReservationsAsync(search);
            if (reservationsResult.IsSuccess)
            {
                var result = new
                {
                    Reservations = reservationsResult.Reservations
                };
                return (true, result);
            }
            return (false, null);
        }

        public async Task<(bool IsSuccess, dynamic SearchResults)> SearchUserHistoricalReservationsAsync(SearchUserReservation search)
        {
            var reservationsResult = await reservationsService.GetUsersHistoricalReservationsAsync(search);
            if (reservationsResult.IsSuccess)
            {
                var result = new
                {
                    Reservations = reservationsResult.Reservations
                };
                return (true, result);
            }
            return (false, null);
        }

        public async Task<(bool IsSuccess, dynamic SearchResults)> SearchVehiclesAvailableAsync(SearchVehicleAvailability search)
        {
            var notAvailableResults = await reservationsService.GetNotAvailableVehiclesAsync(search);

            if (!notAvailableResults.IsSuccess)
            {
                notAvailableResults.VehiclesNotAvailable = new NotAvailableVehicles();
                notAvailableResults.VehiclesNotAvailable.LstNotAvailableVehicles = new List<int> { -1 };
            }
            
            var availableModels = await vehiclesService.GetAvailableCarModelsAsync(notAvailableResults.VehiclesNotAvailable.LstNotAvailableVehicles);
            
            if (availableModels.IsSuccess)
            {
                var result = new
                {
                    TotalRentalHours = (search.EndDate - search.StartDate).TotalHours,
                    AvailableModels = availableModels.AvailableCarModels
                };
                return (true, result);
            }
            return (false, null);
        }
    }
}
