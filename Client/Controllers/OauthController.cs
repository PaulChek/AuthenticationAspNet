using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Client.Controllers {
    [Route("[controller]")]
    [ApiController]
    public class OauthController : ControllerBase {
        [HttpGet]
        public IActionResult Get() {
            return Ok("Happy new Year!!");
        }
    }
}
