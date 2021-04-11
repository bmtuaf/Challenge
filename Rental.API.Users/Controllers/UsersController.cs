using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Rental.API.Users.Interfaces;
using Rental.API.Users.Models.RequestModels;

namespace Rental.API.Users.Controllers
{
    [ApiController]
    [Route("api/users")]
    public class UsersController : ControllerBase
    {
        private readonly ICustomersProvider customersProvider;
        

        public UsersController(ICustomersProvider customersProvider)
        {
            this.customersProvider = customersProvider;
        }

        [HttpPost("create/customer")]
        public async Task<IActionResult> RegisterCustomer([FromBody] CustomerRequest customerRequest)
        {
            var result = await customersProvider.PostCustomerAsync(customerRequest);

            if (result.IsSuccess)
            {
                return Ok(result.Customer);
            }

            return NotFound();
        }

        [HttpPost("login/user")]
        public async Task<IActionResult> LoginUser([FromBody] LoginRequest login)
        {
            var result = await customersProvider.PostLoginAsync(login);

            if (result.IsSuccess)
            {
                return Ok(result.Customer);
            }

            return NotFound();
        }

        //[HttpPost("create/operator")]
        //public async Task<IActionResult> RegisterOperator([FromBody] OperatorRequest operatorRequest)
        //{
        //    var result = await customersProvider.PostCustomerAsync(customerRequest);

        //    if (result.IsSuccess)
        //    {
        //        return Ok(result.Customer);
        //    }

        //    return NotFound();
        //}
    }
}
