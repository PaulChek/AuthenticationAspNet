using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;


namespace Client.Controllers {
    [Route("[controller]")]
    [ApiController]
    public class ClientController : Controller {

        [HttpGet]
        public IActionResult Get() {
            return Ok(new { res = "OK" });
        }

        [Authorize]
        [HttpGet("secret")]
        public IActionResult GetSecret() {
            return Ok( new { super_secret_client = 12134343543});
        }
    }
}
