using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using CoreUlitity.configer;
using Microsoft.IdentityModel.Tokens;

namespace CoreSwagger
{
    public static class SwaggerJwt
    {
        /// <summary>
        /// 产生token
        /// </summary>
        /// <param name="userName"></param>
        /// <returns></returns>
        public static string CreateSecret(string userName)
        {
            List<Claim> claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name,userName),
                new Claim(ClaimTypes.Email,"1561156386@qq.com"),
                new Claim(ClaimTypes.Sid,$"sid"),
                new Claim(JwtRegisteredClaimNames.Sub,"sub")
            };
            var config = ConfigManager.Build.SwaggerConfig;
            claims.Add(new Claim("username", userName));
            claims.Add(new Claim("userid", $"{Guid.NewGuid()}"));
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(config.JwtSecret));
            //var expiertime = DateTime.Now.AddDays(1);
            var expiertime = DateTime.Now.AddSeconds(60);
            var token = new JwtSecurityToken(
                  issuer: config.Issuer,
                  claims: claims,
                  audience: string.IsNullOrWhiteSpace(config.Audience) ? config.Issuer : config.Audience,
                  notBefore: DateTime.Now,
                  expires: expiertime,
                  signingCredentials: new SigningCredentials(key, SecurityAlgorithms.HmacSha256)
              );
            var jwttoken = new JwtSecurityTokenHandler().WriteToken(token);
            var values = new ConcurrentDictionary<DateTime, string>();
            values.TryAdd(expiertime, jwttoken);
            return jwttoken;
        }

    }
}
