using Jwtapi.Contracts;
using Jwtapi.Dto;
using Jwtapi.Enum;
using Jwtapi.Filter;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

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
        public IdentityResultDto Login(string role)
        {
            return GenerateToken("tian", role);
        }

        /// <summary>
        /// 取得JwtToken資訊
        /// </summary>
        /// <param name="token"></param>
        [HttpGet]
        [Role(UserTypes.Admin)]
        private IEnumerable<Claim> GetJwtToken(string token)
        {
            // 建立 JwtSecurityTokenHandler 實例
            var tokenHandler = new JwtSecurityTokenHandler();

            // 解析 JWT
            var jwtTokenObject = tokenHandler.ReadJwtToken(token);

            // 解析 JWT 中的 Claims
            var claims = jwtTokenObject.Claims;

            // 輸出所有 Claims
            foreach (var claim in claims)
            {
                Console.WriteLine($"Claim Type: {claim.Type}, Claim Value: {claim.Value}");
            }

            return claims;
        }

        private IdentityResultDto GenerateToken(string userId,string role)
        {
            var claims = new List<Claim>
            {
                new Claim("UID", userId),
                new Claim("Role",role),
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
                AccessToken = $"Bearer {access_token}",
            };
        }
    }
}
