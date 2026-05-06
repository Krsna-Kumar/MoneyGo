using MediatR;
using Microsoft.AspNetCore.Mvc;
using MoneyGo.Application.Customers.Commands.CreateCustomer;
using MoneyGo.Application.Customers.Commands.CustomerCommands;
using MoneyGo.Application.Customers.Queries.GetCustomerById;

namespace MoneyGo.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public class CustomerController
        (IMediator mediator): ControllerBase
    {
        
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Create([FromBody] CreateCustomerRequest customer)
        {
            var result = await mediator.Send(new AddCustomerCommand(customer));
            return result.IsSuccess ? Ok(result.Value) : BadRequest(result.Error);
        }

        [HttpGet("{id:int:min:1}")]
        public async Task<IActionResult> GetById([FromBody] int id)
        {
            var result = await mediator.Send(new GetCustomerByIdQuery(id));
            return result.IsSuccess ? Ok(result.Value) : NotFound(result.Error);
        }
    }
}
