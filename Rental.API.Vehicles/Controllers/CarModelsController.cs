using Microsoft.AspNetCore.Mvc;
using Rental.API.Vehicles.Interfaces;
using Rental.API.Vehicles.Models.RequestModels;
using Rental.API.Vehicles.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Rental.API.Vehicles.Controllers
{
    [ApiController]
    [Route("api/carmodels")]
    public class CarModelsController : ControllerBase
    {
        private readonly ICarModelsProvider carModelsProvider;

        public CarModelsController(ICarModelsProvider carModelsProvider)
        {
            this.carModelsProvider = carModelsProvider;
        }

        [HttpGet]
        public async Task<IActionResult> GetCarModelsAsync()
        {
            var result = await carModelsProvider.GetCarModelsAsync();
            if (result.IsSuccess)
            {
                return Ok(result.CarModels);
            }
            return NotFound();
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetCarModelAsync(int id)
        {
            var result = await carModelsProvider.GetCarModelAsync(id);
            if (result.IsSuccess)
            {
                return Ok(result.CarModel);
            }
            return NotFound();
        }

        [HttpPost]
        public async Task<IActionResult> PostCarModelAsync(CarModelRequest carModel)
        {
            var result = await carModelsProvider.PostCarModelAsync(carModel);
            if (result.IsSuccess)
            {
                return Ok(result.CarModel);
            }
            return NotFound();
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteCarModelAsync(int id)
        {
            var result = await carModelsProvider.DeleteCarModelAsync(id);
            if (result.IsSuccess)
            {
                return Ok();
            }
            return NotFound();
        }

        [HttpPut]
        public async Task<IActionResult> PutCarModelAsync(CarModelUpdateRequest carModel)
        {
            var result = await carModelsProvider.PutCarModelAsync(carModel);
            if (result.IsSuccess)
            {
                return Ok(result.CarModel);
            }
            return NotFound();
        }
    }
}
