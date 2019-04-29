using System.Text;
using Microsoft.IdentityModel.Tokens;

namespace DAF.DataAccess.Infrastructure
{
    /// <summary>
    /// Представляет фабрику настроек JWT.
    /// </summary>
    public static class JwtOptions
    {
        /// <summary>
        /// Возвращает настройки подсистемы аутентификации JWT.
        /// </summary>
        public static TokenValidationParameters GetOptions()
        {
            var parameters = new TokenValidationParameters
            {
                ValidateIssuer = true,
                ValidIssuer = "Commi",
                ValidateAudience = true,
                ValidAudience = "https://commi.ddns.net",
                ValidateLifetime = true,
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes("ah50gM6Ktj7F3gbRHskYzrjHDUYuWaPmOLr4BsSovaIIB1e6on4bnwUrkvEfE4PxKarpOF50YEL420anMF6TbtHHK6Ic338mdN0yk7akZ1YG3LsJwRDA987Djw4qfNiK"))
            };

            return parameters;
        }
    }
}