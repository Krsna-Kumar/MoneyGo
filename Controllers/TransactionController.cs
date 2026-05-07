using MediatR;
using Microsoft.AspNetCore.Mvc;
using MoneyGo.Application.Transactions.Commands.CreateTransaction;
using MoneyGo.Application.Transactions.DTOs;

namespace MoneyGo.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TransactionController
        (IMediator mediator): ControllerBase
    {
        [HttpPost("{custId:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Create(int custId, [FromBody] TransactionRequest transaction)
        {
            var result = await mediator.Send(new CreateTransactionCommand(custId, transaction));
            return result.IsSuccess ? Ok(result.Value) : BadRequest(result.Error);
        }
    }
}
