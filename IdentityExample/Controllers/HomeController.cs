using IdentityExample.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace IdentityExample.Controllers
{
    public class HomeController : Controller
    {
        private readonly UserManager<IdentityUser> _userManager;
        private SignInManager<IdentityUser> _signinManger;

        public HomeController(
            UserManager<IdentityUser> userManager,
            SignInManager<IdentityUser> singinManger)
        {
            _userManager = userManager;
            _signinManger = singinManger;
        }
        

        public IActionResult Index()
        {
            return View();
        }

        [Authorize]
        public IActionResult Secret()
        {
            return View("Secret");
        }
        public IActionResult Login()
        {
            return View("Login");
        }
        [HttpPost]
        public async Task<IActionResult> Login(string username, string password)
        {
            var user = await _userManager.FindByNameAsync(username);

            if (user != null)
            {
                var result = await _signinManger.PasswordSignInAsync(user,password, false,false);
                if (result.Succeeded)
                {
                    return RedirectToAction("Index");
                }
                return View("Error");
            }

            return View("Error");
        }

        public IActionResult Register()
        {
            return View("register");
        }
        [HttpPost]
        public async Task<IActionResult> Register(string username, string password)
        {
            var newUser = new IdentityUser(username)
            {
                Email = "",
            };
            var result = await _userManager.CreateAsync(newUser, password);

            if (result.Succeeded)
            {
                return RedirectToAction("Index");
            }
            else
            {
                return View("Error");
            }
           
        }

        [HttpGet]
        public async Task<IActionResult> Logout()
        {
            await _signinManger.SignOutAsync();
            return RedirectToAction("Index");
        }
    }
}
