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
        private readonly IOperatorProvider operatorProvider;

        public UsersController(ICustomersProvider customersProvider, IOperatorProvider operatorProvider)
        {
            this.customersProvider = customersProvider;
            this.operatorProvider = operatorProvider;
        }

        [HttpPost("create/customer")]
        public async Task<IActionResult> RegisterCustomer([FromBody] CustomerRequest customerRequest)
        {
            var result = await customersProvider.PostCustomerAsync(customerRequest);

            if (result.IsSuccess)
            {
                return Ok(result);
            }

            return BadRequest(result);
        }

        [HttpPost("login")]
        public async Task<IActionResult> LoginUser([FromBody] LoginRequest login)
        {
            switch (login.UserType)
            {
                case 'C':
                    var result = await customersProvider.PostLoginAsync(login);
                    
                    if (result.IsSuccess)
                    {
                        return Ok(result);
                    }

                    return BadRequest(result); 
                    
                case 'O':
                    var resultOperator = await operatorProvider.PostLoginAsync(login);
                    
                    if (resultOperator.IsSuccess)
                    {
                        return Ok(resultOperator);
                    }

                    return BadRequest(resultOperator);

                default:
                    return BadRequest();
            }
        }

        [HttpPost("create/operator")]
        public async Task<IActionResult> RegisterOperator ([FromBody] OperatorRequest operatorRequest)
        {
            var result = await operatorProvider.PostOperatorAsync(operatorRequest);

            if (result.IsSuccess)
            {
                return Ok(result);
            }

            return BadRequest(result);
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
