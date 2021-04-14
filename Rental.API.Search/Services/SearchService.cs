using Rental.API.Search.Interfaces;
using Rental.API.Search.Models.RequestModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Rental.API.Search.Services
{
    public class SearchService : ISearchService
    {
        private readonly IReservationsService reservationsService;

        public SearchService(IReservationsService reservationsService)
        {
            this.reservationsService = reservationsService;
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
            if (notAvailableResults.IsSuccess)
            {
                var result = new
                {
                    //Fix
                    //Add logic for available cars
                    VehiclesNotAvailable = notAvailableResults.VehiclesNotAvailable
                };
                return (true, result);
            }
            return (false, null);
        }
    }
}
