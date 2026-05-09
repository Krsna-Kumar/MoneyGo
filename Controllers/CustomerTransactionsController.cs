namespace MoneyGo.Api.Controllers
{
    [Route("api/customers/{customerId:int}/transactions")]
    [ApiController]
    public class CustomerTransactionsController
        (IMediator mediator): ControllerBase
    {
        [HttpGet]
        public async Task<IActionResult> GetAllTransactionById(int id)
        {
            var result = await mediator.Send(new GetTransactionsByIdQuery(id));
            return result.IsSuccess ? Ok(result.Value) : NotFound(result.Error);
        }

        [HttpPost("/credit")]
        public async Task<IActionResult> CreateCreditTransaction(int custId, [FromBody] TransactionRequest creditTrn)
        {
            var creditResult = await mediator.Send(new AddCreditTransactionCommand(custId, creditTrn));
            return creditResult.IsSuccess ? Ok(creditResult.Value) : BadRequest(creditResult.Error);
        }

        [HttpPost("/payment")]
        public async Task<IActionResult> CreatePaymentTransaction(int custId, [FromBody] TransactionRequest paymentTrn)
        {
            var paymentResult = await mediator.Send(new AddPaymentTransactionCommand(custId, paymentTrn));
            return paymentResult.IsSuccess ? Ok(paymentResult.Value) : BadRequest(paymentResult.Error);
        }

        [HttpGet("/balance")]
        public async Task<IActionResult> GetBalanceById(int id)
        {
            var result = await mediator.Send(new GetBalanceByIdQuery(id));
            return result.IsSuccess ? Ok(result.Value) : NotFound(result.Error);
        }
    }
}
