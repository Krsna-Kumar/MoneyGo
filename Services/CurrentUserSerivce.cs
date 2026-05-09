using MoneyGo.Application.Common.Interfaces;
using System.IdentityModel.Tokens.Jwt;

namespace MoneyGo.Api.Services
{
    public class CurrentUserSerivce
        (IHttpContextAccessor _httpContextAccessor): ICurrentUserService
    {
        public int UserId
        {
            get
            {
                var claims = _httpContextAccessor.HttpContext?.User.FindFirst
                    (JwtRegisteredClaimNames.Sub);

                return int.Parse(claims!.Value);
            }
        }
    }
}
