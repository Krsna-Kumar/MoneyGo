using MediatR;
using Microsoft.AspNetCore.Mvc;
using MoneyGo.Application.Customers.Commands.CustomerCommands;
using MoneyGo.Application.Customers.Commands.DeleteCustomer;
using MoneyGo.Application.Customers.Commands.UpdateCustomer;
using MoneyGo.Application.Customers.DTOs;
using MoneyGo.Application.Customers.Queries.GetCustomerById;
using MoneyGo.Application.Customers.Queries.GetCustomersByUserId;

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
        public async Task<IActionResult> Create([FromBody] CustomerRequest customer)
        {
            var result = await mediator.Send(new AddCustomerCommand(customer));
            return result.IsSuccess ? Ok(result.Value) : BadRequest(result.Error);
        }

        [HttpGet("by-userid/{userId:int}")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetCustomersByUserId(int userId)
        {
            var result = await mediator.Send(new GetCustomersByUserIdQuery(userId));
            return result.IsSuccess
                ? Ok(result.Value)
                : NotFound(result.Error);
        }

        [HttpGet("{id:int}")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetById(int id)
        {
            var result = await mediator.Send(new GetCustomerByIdQuery(id));
            return result.IsSuccess 
                ? Ok(result.Value) 
                : NotFound(result.Error);
        }

        [HttpPut("{id:int}")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Update(int id, [FromBody] CustomerRequest customerDetails)
        {
            var result = await mediator.Send(new UpdateCustomerCommand(id, customerDetails));
            return result.IsSuccess
                ? Ok(result.Value)
                : NotFound(result.Error);
        }

        [HttpDelete("{id:int}")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await mediator.Send(new DeleteCustomerCommand(id));
            return result.IsSuccess
                ? Ok(result.Value)
                : NotFound(result.Error);
        }
    }
}
