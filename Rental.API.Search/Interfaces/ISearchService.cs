using Rental.API.Search.Models.RequestModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Rental.API.Search.Interfaces
{
    public interface ISearchService
    {
        Task<(bool IsSuccess, dynamic SearchResults)> SearchUserActiveReservationsAsync(SearchUserReservation search);
        Task<(bool IsSuccess, dynamic SearchResults)> SearchUserHistoricalReservationsAsync(SearchUserReservation search);
        Task<(bool IsSuccess, dynamic SearchResults)> SearchVehiclesAvailableAsync(SearchVehicleAvailability search);
    }
}
