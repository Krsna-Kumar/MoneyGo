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
            var result = await mediator.Send(new UpdateUserCommand(request.Status));
            return result.IsSuccess
                ? NoContent()
                : BadRequest(result.Error);
        }
    }
}
