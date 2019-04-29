using System;
using System.Collections;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;

namespace DAF.DataAccess.Infrastructure
{
    public class Helper
    {
        /// <summary>
        /// Формирует строку подключения к базе данных.
        /// </summary>
        public static string GetConnectionString()
        {
            var machineName = Environment.MachineName;
            string connectionString;

            switch (machineName)
            {
                case "ROMANPC":
                    connectionString = "commi.ddns.net,1433";
                    break;
                    //return $@"";

                case "AQUA":
                    connectionString = "AQUA\\COMMIDB";
                    break;

                default:
                    connectionString = machineName;
                    break;
            }

            return $@"";
        }

        /// <summary>
        /// Генерирует токен
        /// </summary>
        public static string GenerateToken(IEnumerable<Claim> claims, TimeSpan lifeTime)
        {
            var secretKey = "a";

            var now = DateTime.Now;
            var token = new JwtSecurityToken(
                issuer: "Commi",
                audience: "https://commi.ddns.net",
                notBefore: now,
                claims: claims,
                expires: now.Add(lifeTime),
                signingCredentials:
                new SigningCredentials(new SymmetricSecurityKey(Encoding.ASCII.GetBytes(secretKey)), SecurityAlgorithms.HmacSha256));

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
