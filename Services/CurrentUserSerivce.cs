using MoneyGo.Application.Common.Interfaces;
using System.IdentityModel.Tokens.Jwt;

public class CurrentUserSerivce(IHttpContextAccessor _httpContextAccessor) : ICurrentUserService
{
    public int UserId
    {
        get
        {
            var userIdClaim = _httpContextAccessor.HttpContext?.User?
                .FindFirst(JwtRegisteredClaimNames.Sub)?.Value;

            if (int.TryParse(userIdClaim, out var userId))
            {
                return userId;
            }
            return 0;
        }
    }
}
