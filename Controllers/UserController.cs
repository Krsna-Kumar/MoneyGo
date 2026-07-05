using MoneyGo.Application.Common.UpdateUser;

namespace MoneyGo.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController
        (IMediator mediator) : ControllerBase
    {
        [HttpPut("update-status")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> UpdateUserStatusBulk([FromBody] UserStatusRequest request)
        {
            var command = new UpdateUserCommand(request.Status);
            var result = await mediator.Send(command);
            return result.IsSuccess
                ? NoContent()
                : BadRequest(result.Error);
        }
    }
}
