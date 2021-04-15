using Rental.API.Orchestrator.Models.RequestModels;
using System.Threading.Tasks;

namespace Rental.API.Orchestrator.Interfaces
{
    public interface IReservationOrchestratorService
    {
        Task<(bool IsSuccess, string RentalContract)> PostReservationAsync(ReservationRequest request);
    }
}
