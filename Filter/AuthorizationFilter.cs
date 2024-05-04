using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;
using System.IdentityModel.Tokens.Jwt;
using Jwtapi.Enum;

namespace Jwtapi.Filter
{
    public class Role : Attribute, IAuthorizationFilter
    {
        private readonly UserTypes _userTypes;

        public Role(UserTypes userTypes)
        {
            _userTypes = userTypes;
        }

        public void OnAuthorization(AuthorizationFilterContext context)
        {
            // 從 HTTP 請求的授權標頭中取得 JwtToken
            var jwtToken = context.HttpContext.Request.Headers["Authorization"].FirstOrDefault()?.Split(" ").Last();
            // 如果沒有 JWT，返回 401 未授權
            if (jwtToken == null)
            {
                context.Result = new UnauthorizedResult();
                return;
            }

            try
            {
                var tokenHandler = new JwtSecurityTokenHandler();
                // 解析 JWT
                var jwtTokenObject = tokenHandler.ReadJwtToken(jwtToken);

                // 取得 JWT 中的 Claims
                var claims = jwtTokenObject.Claims;
                var role = claims.Where(x => x.Type == "Role").Select(x => x.Value).FirstOrDefault();
                var roleNum = (int)System.Enum.Parse(typeof(UserTypes), role);

                if (roleNum < (int)_userTypes)
                {
                    context.Result = new UnauthorizedResult();
                    return;
                }
            }
            catch (Exception)
            {
                // JWT 驗證失敗，返回 401 未授權
                context.Result = new UnauthorizedResult();
                return;
            }
        }
    }

}
