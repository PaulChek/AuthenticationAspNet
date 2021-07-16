using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Server.Controllers {
    [Route("[controller]")]
    [ApiController]
    public class OauthController : Controller {
        private readonly IConfiguration _configuration;

        public OauthController(IConfiguration configuration) {
            _configuration = configuration;
        }

        [HttpGet("authorize")]
        public IActionResult Auth(string response_type, string client_id, string redirect_uri, string scope, string state) {
            var query = new QueryBuilder();
            query.Add("redirectUri", redirect_uri);
            query.Add("state", state);
            return View(model: query.ToString());
        }
        
        [HttpPost("authorize")]
        public IActionResult Auth([FromForm] string username, string redirectUri, string state) {
            var query = new QueryBuilder();
            query.Add("code", "AABBBSSSSDD");
            query.Add("state", state);
           
            return Redirect($"{redirectUri}{query}");
        }
        
        [HttpGet("token")]
        public object Token(
            string grant_type, // flow of access_token request
            string code, // confirmation of the authentication process
            string redirect_uri,
            string client_id,
            string refresh_token) {
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
            Console.WriteLine("token get");

            var respObj = new {
                access_token = tokenString,
                token_type = "Bearer",
                raw_claim = "oauthTutorial",
                refresh_token = "RefreshTokenSampleValueSomething77"
            };

            //var res = JsonSerializer.Serialize(respObj);
            //var bytes = Encoding.ASCII.GetBytes(res);
            //await Response.Body.WriteAsync(bytes, 0, bytes.Length);


            return respObj;
        }
    }
}
