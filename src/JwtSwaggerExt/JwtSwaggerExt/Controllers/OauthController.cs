using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Threading.Tasks;
using CoreSwagger;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace JwtSwaggerExt.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OauthController : ControllerBase
    {
        /// <summary>
        /// 获取token
        /// </summary>
        /// <param name="username"></param>
        /// <returns></returns>
        [Route("GetToken")]
        [HttpPost]
        public object GetToken(string username="")
        {
            var token = SwaggerJwt.CreateSecret(username);
            return new { token = token, expiertime = 60 };
        }

        /// <summary>
        /// 解析token
        /// </summary>
        /// <param name="token"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("GetUser")]
        public object GetUser(string token)
        {
            string tokenstr = token.Replace("Bearer ", "");
            var handler = new JwtSecurityTokenHandler();
            var payload = handler.ReadJwtToken(tokenstr).Payload;
            var claims = payload.Claims;
            var nbfstr = claims.First(claim => claim.Type == "nbf")?.Value;
            var expstr = claims.First(claim => claim.Type == "exp")?.Value;
            var info = new
            {
                username = claims.First(claim => claim.Type == "username")?.Value,
                userid = claims.First(claim => claim.Type == "userid")?.Value,
                nbf = nbfstr,
                exp = expstr
            };

            return info;
        }
    }
}
