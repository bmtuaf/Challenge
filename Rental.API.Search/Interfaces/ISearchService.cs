using Rental.API.Orchestrator.Models.RequestModels;
using System.Threading.Tasks;

namespace Rental.API.Orchestrator.Interfaces
{
    public interface ISearchService
    {
        Task<(bool IsSuccess, dynamic SearchResults)> SearchUserActiveReservationsAsync(SearchUserReservation search);
        Task<(bool IsSuccess, dynamic SearchResults)> SearchUserHistoricalReservationsAsync(SearchUserReservation search);
        Task<(bool IsSuccess, dynamic SearchResults)> SearchVehiclesAvailableAsync(SearchVehicleAvailability search);
    }
}
