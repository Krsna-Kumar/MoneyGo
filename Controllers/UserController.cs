using MoneyGo.Application.Common.UpdateUser;
using MoneyGo.Core.Entities.Enums;

namespace MoneyGo.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController
        (IMediator mediator): ControllerBase
    {
        [HttpPost]
        public async Task<IActionResult> UpdateUserStatusBulk([FromBody] UserStatus status)
        {
            var result = await mediator.Send(new UpdateUserCommand(status));
            return result.IsSuccess
                ? Ok(result.Value)
                : BadRequest(result.Error);
        }
    }
}
