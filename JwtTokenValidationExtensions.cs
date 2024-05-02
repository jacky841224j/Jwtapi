using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace Jwtapi
{
    public static class JwtTokenValidation
    {
        public static IServiceCollection AddJwtTokenValidation(this IServiceCollection services)
        {
            var config = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json")
                .Build();

            var jwtSecret = config.GetSection("JwtConfig")["Secret"];

            var tokenValidationParams = new TokenValidationParameters
            {
                RequireExpirationTime = false,
                ValidateIssuer = false,
                ValidateAudience = false,

                //驗證IssuerSigningKey
                ValidateIssuerSigningKey = true,
                //以JwtConfig:Secret為Key，做為Jwt加密
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(jwtSecret)),

                //驗證時效
                ValidateLifetime = true,

                //設定token的過期時間可以以秒來計算，當token的過期時間低於五分鐘時使用。
                ClockSkew = TimeSpan.Zero
            };

            //services.AddSingleton(tokenValidationParams);

            return services;
        }
    }
}