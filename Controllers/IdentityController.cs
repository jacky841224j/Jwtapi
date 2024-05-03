using Jwtapi.Contracts;
using Jwtapi.Dto;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Jwtapi.Controllers
{
    [ApiController]
    [Route("[controller]/[action]")]
    public class IdentityController : ControllerBase
    {
        private readonly JwtConfig _jwtConfig;

        public IdentityController(IOptions<JwtConfig> jwtConfig)
        {
            _jwtConfig = jwtConfig.Value;
        }

        [HttpGet]
        public IdentityResultDto Login()
        {
            return GenerateToken("tian");
        }

        private IdentityResultDto GenerateToken(string userId)
        {
            var claims = new List<Claim>
            {
                new Claim("UID", userId),
            };

            var securityToken = new JwtSecurityToken(
                issuer: _jwtConfig.Issuer,
                audience: _jwtConfig.Audience,
                claims: claims,
                notBefore: _jwtConfig.NotBefore,
                expires: _jwtConfig.Expiration,
                signingCredentials: _jwtConfig.SigningCredentials
            );

            var access_token = new JwtSecurityTokenHandler().WriteToken(securityToken);

            return new IdentityResultDto()
            {
                AccessToken = access_token,
                //Expires = expireTime.ToUnixTimeMilliseconds(),
                //RefreshToken = refreshToken,
                //CompanyID = adminUser.CompanyID,
                //Account = adminUser.Account,
                //NickName = adminUser.NickName,
                //UserType = adminUser.UserType
            };
        }
    }
}
