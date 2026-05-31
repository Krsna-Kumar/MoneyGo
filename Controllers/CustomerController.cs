using Microsoft.AspNetCore.Authorization;
using MoneyGo.Application.Customers.Commands.BulkCreateCustomer;

namespace MoneyGo.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    [Authorize]
    public class CustomerController
        (IMediator mediator): ControllerBase
    {
        
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Create([FromBody] CustomerRequest customer)
        {
            var command = new AddCustomerCommand(customer);
            var result = await mediator.Send(command);
            return result.IsSuccess 
                ? Ok(result.Value) 
                : BadRequest(result.Error);
        }

        [HttpPost("bulk-create")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> BulkCreate([FromBody] IEnumerable<CustomerRequest> customers)
        {
            var command = new BulkAddCustomerCommand(customers);
            var result = await mediator.Send(command);
            return result.IsSuccess 
                ? Ok(result.Value) 
                : BadRequest(result.Error);
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetAllCustomers()
        {
            var query = new GetCustomersByUserIdQuery();
            var result = await mediator.Send(query);
            return result.IsSuccess
                ? Ok(result.Value)
                : BadRequest(result.Error);
        }

        [HttpGet("{id:int}")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetCustomerById(int id)
        {
            var query = new GetCustomerByIdQuery(id);
            var result = await mediator.Send(query);
            return result.IsSuccess 
                ? Ok(result.Value) 
                : NotFound(result.Error);
        }

        [HttpPut("{id:int}")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Update(int id, [FromBody] CustomerRequest customerDetails)
        {
            var command = new UpdateCustomerCommand(id, customerDetails);
            var result = await mediator.Send(command);
            return result.IsSuccess
                ? Ok(result.Value)
                : NotFound(result.Error);
        }

        [HttpDelete("{id:int}")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Delete(int id)
        {
            var command = new DeleteCustomerCommand(id);
            var result = await mediator.Send(command);
            return result.IsSuccess
                ? Ok(result.Value)
                : NotFound(result.Error);
        }
    }
}
