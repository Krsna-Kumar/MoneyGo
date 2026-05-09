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
        public async Task<IActionResult> RegisterUser([FromBody] AuthRequest registerRequest)
        {
            var registeredUser = await mediator.Send(new RegisterUserCommand
                (registerRequest.Username, registerRequest.Password));
            return registeredUser.IsSuccess
                ? Ok(registeredUser.Value)
                : BadRequest(registeredUser.Error);
        }

        [HttpGet("login")]
        public async Task<IActionResult> LoginUser([FromBody] AuthRequest loginInRequest)
        {
            var loggedInUser = await mediator.Send(new LoginUserCommnad
                (loginInRequest.Username, loginInRequest.Password));
            return loggedInUser.IsSuccess
                ? Ok(loggedInUser.Value)
                : BadRequest(loggedInUser.Error);
        }
    }
}
