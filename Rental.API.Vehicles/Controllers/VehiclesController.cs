using Microsoft.AspNetCore.Mvc;
using Rental.API.Vehicles.Interfaces;
using Rental.API.Vehicles.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Rental.API.Vehicles.Controllers
{
    [ApiController]
    [Route("api/vehicles")]
    public class VehiclesController : ControllerBase
    {
        private readonly IVehicleProvider vehicleProvider;

        public VehiclesController(IVehicleProvider vehicleProvider)
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
    }
}
