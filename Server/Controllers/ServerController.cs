using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Server.Controllers {
    [Route("/")]
    [ApiController]
    public class ServerController : ControllerBase {
        private readonly IConfiguration _configuration;

        public ServerController(IConfiguration configuration) {
            _configuration = configuration;
        }

        [HttpGet]
        public IActionResult Get() {
            return Ok(new { Message = "Hello" });
        } 

        [Authorize]
        [HttpGet("secret")]
        public IActionResult GetSecret() {
            return Ok(new { secret = $"{new Random().Next(999999,10000000).ToString("X")}"   });
        } 
        [HttpGet("gettoken")]
        public IActionResult GetToken() {
            var claims = new[] {
                new Claim(JwtRegisteredClaimNames.Sub, "_id"),
                new Claim("SuckMyDick", "yes")
            };
            var sk = new SymmetricSecurityKey(Encoding.ASCII.GetBytes("secret_words_fdafwfwfwerfdfgasyudgsaudsdsddQDUYd"));

            var signCredentials = new SigningCredentials(sk, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                _configuration.GetConnectionString("Issuer"),
                _configuration.GetConnectionString("Audience"),
                claims,
                notBefore: DateTime.Now,
                expires: DateTime.Now.AddHours(2),
                signCredentials
                );
            var tokenString = new JwtSecurityTokenHandler().WriteToken(token);



            return Ok(new { secret = $"{new Random().Next(999999,10000000).ToString("X")}", token = tokenString });
        }
    }
}
