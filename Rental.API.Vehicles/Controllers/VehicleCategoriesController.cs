using Microsoft.AspNetCore.Mvc;
using Rental.API.Vehicles.Interfaces;
using System.Threading.Tasks;

namespace Rental.API.Vehicles.Controllers
{
    [ApiController]
    [Route("api/vehiclecategories")]
    public class VehicleCategoriesController : ControllerBase
    {
        private readonly IVehicleCategoriesProvider vehicleCategoriesProvider;

        public VehicleCategoriesController(IVehicleCategoriesProvider vehicleCategoriesProvider)
        {
            this.vehicleCategoriesProvider = vehicleCategoriesProvider;
        }

        [HttpGet]
        public async Task<IActionResult> GetVehicleCategoriesAsync()
        {
            var result = await vehicleCategoriesProvider.GetVehicleCategoriesAsync();
            if (result.IsSuccess)
            {
                return Ok(result.VehicleCategories);
            }
            return NotFound();
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetVehicleCategoryAsync(int id)
        {
            var result = await vehicleCategoriesProvider.GetVehicleCategoryAsync(id);
            if (result.IsSuccess)
            {
                return Ok(result.VehicleCategory);
            }
            return NotFound();
        }
    }
}
