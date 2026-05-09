using MoneyGo.Application.Common.LoginUser;
using MoneyGo.Application.Common.RegisterUser;

namespace MoneyGo.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public class AuthController
        (IMediator mediator): ControllerBase
    {
        [HttpPost("register")]
        public async Task<IActionResult> RegisterUser([FromBody] string username, string password)
        {
            var registeredUser = await mediator.Send(new RegisterUserCommand(username, password));
            return registeredUser.IsSuccess
                ? Ok(registeredUser.Value)
                : BadRequest(registeredUser.Error);
        }

        [HttpGet("login")]
        public async Task<IActionResult> LoginUser([FromBody] string username, string password)
        {
            var loggedInUser = await mediator.Send(new LoginUserCommnad(username, password));
            return loggedInUser.IsSuccess
                ? Ok(loggedInUser.Value)
                : BadRequest(loggedInUser.Error);
        }
    }
}
