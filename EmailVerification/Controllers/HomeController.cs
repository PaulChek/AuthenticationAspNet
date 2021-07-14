using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using NETCore.MailKit.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EmailVerification.Controllers {
    public class HomeController : Controller {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly IEmailService _emailService;

        public HomeController(UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager, IEmailService emailService) {
            _userManager = userManager;
            _signInManager = signInManager;
            _emailService = emailService;
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
            var user = new IdentityUser { UserName = username };
          var res =  await _userManager.CreateAsync(user, password);
            if (res.Succeeded) {

                var emailToken = await _userManager.GenerateEmailConfirmationTokenAsync(user);
               
                var link = Url.Action(nameof(VerifyEmail), "Home", new { userId = user.Id , token = emailToken }, Request.Scheme+"", Request.Host+"") ;
                
                _emailService.Send("paulchek777@gmail.com", "auth", link, false, null);

                return Ok("Check your email and verify!");
            }

            return RedirectToAction("Login");
        }

        [Route("[action]")]
        public async Task<IActionResult> VerifyEmail(string userId, string token) {
            var user = await _userManager.FindByIdAsync(userId);

            await _userManager.ConfirmEmailAsync(user, token);
            return View();
        }
    }
}
