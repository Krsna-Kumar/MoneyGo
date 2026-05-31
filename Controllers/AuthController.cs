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
        public async Task<IActionResult> RegisterUser([FromBody] AuthRequest request)
        {
            var command = new RegisterUserCommand(request.Username, request.Password);

            var result = await mediator.Send(command);

            return result.IsSuccess
                ? Ok(result.Value)
                : BadRequest(result.Error);
        }

        [HttpPost("login")]
        public async Task<IActionResult> LoginUser([FromBody] AuthRequest request)
        {
            var command = new LoginUserCommnad(request.Username, request.Password);

            var result = await mediator.Send(command);

            return result.IsSuccess
                ? Ok(result.Value)
                : Unauthorized(result.Error);
        }

    }
}
