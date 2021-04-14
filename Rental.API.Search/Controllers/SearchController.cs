using Microsoft.AspNetCore.Mvc;
using Rental.API.Search.Interfaces;
using Rental.API.Search.Models.RequestModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Rental.API.Search.Controllers
{
    [ApiController]
    [Route("api/search")]
    public class SearchController : ControllerBase
    {
        private readonly ISearchService searchService;

        public SearchController(ISearchService searchService)
        {
            this.searchService = searchService;
        }

        [HttpPost]
        [Route("reservations/active")]
        public async Task<IActionResult> SearchUserActiveReservationsAsync(SearchUserReservation search)
        {
            var result = await searchService.SearchUserActiveReservationsAsync(search);
            if (result.IsSuccess)
            {
                return Ok(result.SearchResults);
            }
            return NotFound();
        }

        [HttpPost]
        [Route("reservations/historical")]
        public async Task<IActionResult> SearchUserHistoricalReservationsAsync(SearchUserReservation search)
        {
            var result = await searchService.SearchUserHistoricalReservationsAsync(search);
            if (result.IsSuccess)
            {
                return Ok(result.SearchResults);
            }
            return NotFound();
        }

        [HttpPost]
        [Route("vehicles/available")]
        public async Task<IActionResult> SearchVehiclesAvailableAsync(SearchVehicleAvailability search)
        {
            var result = await searchService.SearchVehiclesAvailableAsync(search);
            if (result.IsSuccess)
            {
                return Ok(result.SearchResults);
            }
            return NotFound();
        }
    }
}
