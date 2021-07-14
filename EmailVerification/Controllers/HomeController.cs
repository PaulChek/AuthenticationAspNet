using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EmailVerification.Controllers {
    public class HomeController : Controller {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly SignInManager<IdentityUser> _signInManager;

        public HomeController(UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager) {
            _userManager = userManager;
            _signInManager = signInManager;
        }

        public IActionResult Index() {
            return View();
        }
        [Authorize]
        [Route("[action]")]
        public IActionResult Secret() {
            return View();
        }
        [Route("[action]")]
        public IActionResult Login() {
            return View();
        }
        [Route("[action]")]
        public IActionResult Reg() {
            return View();
        }
        [Route("[action]")]
        public async Task<IActionResult> LogOutAsync() {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Login");
        }

        [Route("[action]")]
        [HttpPost]
        public async Task<IActionResult> LoginAsync(string username, string password) {
            var user = await _userManager.FindByNameAsync(username);

            await _signInManager.PasswordSignInAsync(user, password, false, false);

            return RedirectToAction("Secret");
        }
        [Route("[action]")]
        [HttpPost]
        public async Task<IActionResult> RegAsync(string username, string password) {

            await _userManager.CreateAsync(new IdentityUser { UserName = username }, password);

            return RedirectToAction("Login");
        }
    }
}
