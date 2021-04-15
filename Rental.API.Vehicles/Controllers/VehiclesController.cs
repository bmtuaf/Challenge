using Microsoft.AspNetCore.Mvc;
using Rental.API.Vehicles.Interfaces;
using Rental.API.Vehicles.Models.RequestModels;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Rental.API.Vehicles.Controllers
{
    [ApiController]
    [Route("api/vehicles")]
    public class VehiclesController : ControllerBase
    {
        private readonly IVehiclesProvider vehicleProvider;

        public VehiclesController(IVehiclesProvider vehicleProvider)
        {
            this.vehicleProvider = vehicleProvider;
        }

        [HttpGet]
        public async Task<IActionResult> GetVehiclesAsync()
        {
            var result = await vehicleProvider.GetVehiclesAsync();
            if (result.IsSuccess)
            {
                return Ok(result.Vehicles);
            }
            return NotFound();
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetVehicleAsync(int id)
        {
            var result = await vehicleProvider.GetVehicleAsync(id);
            if (result.IsSuccess)
            {
                return Ok(result.Vehicle);
            }
            return NotFound();
        }

        [HttpPost("reservationvehicle")]
        public async Task<IActionResult> PostReservationVehicleAsync(VehicleReservationRequest vehicleReservationRequest)
        {
            var result = await vehicleProvider.PostReservationVehicleAsync(vehicleReservationRequest);
            if (result.IsSuccess)
            {
                return Ok(result.Vehicle);
            }
            return NotFound();
        }

        [HttpPost]
        public async Task<IActionResult> PostVehicleAsync(VehicleRequest vehicle)
        {
            var result = await vehicleProvider.PostVehicleAsync(vehicle);
            if (result.IsSuccess)
            {
                return Ok(result.Vehicle);
            }
            return NotFound();
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteVehicleAsync(int id)
        {
            var result = await vehicleProvider.DeleteVehicleAsync(id);
            if (result.IsSuccess)
            {
                return Ok();
            }
            return NotFound();
        }

        [HttpPut]
        public async Task<IActionResult> PutVehicleAsync(VehicleUpdateRequest vehicle)
        {
            var result = await vehicleProvider.PutVehicleAsync(vehicle);
            if (result.IsSuccess)
            {
                return Ok(result.Vehicle);
            }
            return NotFound();
        }
    }
}
