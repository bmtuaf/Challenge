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
    [Route("api/makes")]
    public class MakesController : ControllerBase
    {
        private readonly IMakesProvider makesProvider;

        public MakesController(IMakesProvider makesProvider)
        {
            this.makesProvider = makesProvider;
        }

        [HttpGet]
        public async Task<IActionResult> GetMakesAsync()
        {
            var result = await makesProvider.GetMakesAsync();
            if (result.IsSuccess)
            {
                return Ok(result.Makes);
            }
            return NotFound();
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetMakeAsync(int id)
        {
            var result = await makesProvider.GetMakeAsync(id);
            if (result.IsSuccess)
            {
                return Ok(result.Make);
            }
            return NotFound();
        }
        
        [HttpPost]
        public async Task<IActionResult> GetMakeAsync(string name)
        {
            var result = await makesProvider.PostMakeAsync(name);
            if (result.IsSuccess)
            {
                return Ok(result.Make);
            }
            return NotFound();
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteMakeAsync(int id)
        {
            var result = await makesProvider.DeleteMakeAsync(id);
            if (result.IsSuccess)
            {
                return Ok();
            }
            return NotFound();
        }

        [HttpPut]
        public async Task<IActionResult> PutMakeAsync([FromBody] Make make)
        {
            var result = await makesProvider.PutMakeAsync(make);
            if (result.IsSuccess)
            {
                return Ok(result.Make);
            }
            return NotFound();
        }
    }
    
}
