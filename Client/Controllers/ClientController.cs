using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;


namespace Client.Controllers {
    [Route("[controller]")]
    [ApiController]
    public class ClientController : ControllerBase {

        [HttpGet]
        public IActionResult Get() {
            return Ok(new { res = "OK" });
        }

        [Authorize]
        [HttpGet("secret")]
        public IActionResult GetSecret() {
            return Ok(new { res = "Secret Very" });
        }
    }
}
