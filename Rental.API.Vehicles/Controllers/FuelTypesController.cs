using Microsoft.AspNetCore.Mvc;
using Rental.API.Vehicles.Interfaces;
using System.Threading.Tasks;
namespace Rental.API.Vehicles.Controllers
{
    [ApiController]
    [Route("api/fueltypes")]
    public class FuelTypesController : ControllerBase
    {
        private readonly IFuelTypesProvider fuelTypesProvider;

        public FuelTypesController(IFuelTypesProvider fuelTypesProvider)
        {
            this.fuelTypesProvider = fuelTypesProvider;
        }

        [HttpGet]
        public async Task<IActionResult> GetFuelTypesAsync()
        {
            var result = await fuelTypesProvider.GetFuelTypesAsync();
            if (result.IsSuccess)
            {
                return Ok(result.FuelTypes);
            }
            return NotFound();
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetFuelTypeAsync(int id)
        {
            var result = await fuelTypesProvider.GetFuelTypeAsync(id);
            if (result.IsSuccess)
            {
                return Ok(result.FuelType);
            }
            return NotFound();
        }
    }
}
