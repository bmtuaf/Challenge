using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Rental.API.Reservations.Interfaces;
using Rental.API.Reservations.Models.RequestModels;
using System.Threading.Tasks;

namespace Rental.API.Reservations.Controllers
{
    [ApiController]
    [Route("api/reservations")]
    public class ReservationsController : ControllerBase
    {
        private readonly IReservationsProvider reservationsProvider;

        public ReservationsController(IReservationsProvider reservationsProvider)
        {
            this.reservationsProvider = reservationsProvider;
        }

        [HttpGet]
        [Authorize(Roles = "Operator")]
        public async Task<IActionResult> GetReservationsAsync()
        {
            var result = await reservationsProvider.GetReservationsAsync();
            if (result.IsSuccess)
            {
                return Ok(result.Reservations);
            }
            return NotFound();
        }

        [HttpGet("{id}")]
        [Authorize(Roles = "Customer, Operator")]
        public async Task<IActionResult> GetReservationAsync(int id)
        {
            var result = await reservationsProvider.GetReservationAsync(id);
            if (result.IsSuccess)
            {
                return Ok(result.Reservation);
            }
            return NotFound();
        }

        [HttpGet("user/active/{cpf}")]
        public async Task<IActionResult> GetUsersActiveReservationsAsync(string cpf)
        {
            var result = await reservationsProvider.GetUsersActiveReservationsAsync(cpf);
            if (result.IsSuccess)
            {
                return Ok(result.Reservations);
            }
            return NotFound();
        }

        [HttpGet("user/historical/{cpf}")]
        public async Task<IActionResult> GetUsersHistoricalReservationsAsync(string cpf)
        {
            var result = await reservationsProvider.GetUsersHistoricalReservationsAsync(cpf);
            if (result.IsSuccess)
            {
                return Ok(result.Reservations);
            }
            return NotFound();
        }

        [HttpPost]        
        public async Task<IActionResult> PostReservationAsync(ReservationRequest request)
        {
            var result = await reservationsProvider.PostReservationAsync(request);
            if (result.IsSuccess)
            {
                return Ok(result.Reservation);
            }
            return NotFound();
        }

        [HttpPost("notavailablevehicles")]
        public async Task<IActionResult> GetNotAvailableVehiclesAsync(NotAvailableVehiclesRequest request)
        {
            var result = await reservationsProvider.GetNotAvailableVehiclesAsync(request);
            if (result.IsSuccess)
            {
                return Ok(result.NotAvailableVehicles);
            }
            return NotFound();
        }

        [HttpPost("{id}/return")]
        [Authorize(Roles = "Operator")]
        public async Task<IActionResult> PostVehicleReturnAsync(int id, ReturnRequest request)
        {
            request.ID = id;
            var result = await reservationsProvider.PostVehicleReturnAsync(request);
            if (result.IsSuccess)
            {
                return Ok(result.Reservation);
            }
            return NotFound();
        }

    }
}
