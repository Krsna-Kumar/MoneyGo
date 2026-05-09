using MediatR;
using Microsoft.AspNetCore.Mvc;
using MoneyGo.Application.Customers.Commands.CustomerCommands;
using MoneyGo.Application.Customers.Commands.DeleteCustomer;
using MoneyGo.Application.Customers.Commands.UpdateCustomer;
using MoneyGo.Application.Customers.DTOs;
using MoneyGo.Application.Customers.Queries.GetCustomerById;
using MoneyGo.Application.Customers.Queries.GetCustomersByUserId;
using MoneyGo.Application.Transactions.Commands.AddCreditTransaction;
using MoneyGo.Application.Transactions.Commands.AddPaymentTransaction;
using MoneyGo.Application.Transactions.DTOs;
using MoneyGo.Application.Transactions.Queries.GetTransactionsById;

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

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetAllCustomers()
        {
            var result = await mediator.Send(new GetCustomersByUserIdQuery());
            return result.IsSuccess
                ? Ok(result.Value)
                : BadRequest(result.Error);
        }

        [HttpGet("{id:int}")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetCustomerById(int id)
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

        [HttpGet("{id:int}/transactions")]
        public async Task<IActionResult> GetAllTransactionById(int id)
        {
            var result = await mediator.Send(new GetTransactionsByIdQuery(id));
            return result.IsSuccess ? Ok(result.Value) : NotFound(result.Error);
        }

        [HttpPost("{id:int}/transactions/credit")]
        public async Task<IActionResult> CreateCreditTransaction(int custId, [FromBody] TransactionRequest creditTrn)
        {
            var creditResult = await mediator.Send(new AddCreditTransactionCommand(custId, creditTrn));
            return creditResult.IsSuccess ? Ok(creditResult.Value) : BadRequest(creditResult.Error);
        }

        [HttpPost("{id:int}/transactions.payment")]
        public async Task<IActionResult> CreatePaymentTransaction(int custId, [FromBody]TransactionRequest paymentTrn)
        {
            var paymentResult = await mediator.Send(new AddPaymentTransactionCommand(custId, paymentTrn));
            return paymentResult.IsSuccess ? Ok(paymentResult.Value) : BadRequest(paymentResult.Error);
        }
    }
}
