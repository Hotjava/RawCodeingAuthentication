using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace RawCodingAuthentication.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index() => View();
        
        [Authorize (policy: "DoB")]
        public IActionResult Secret()
        {
            return View();
        }

        public IActionResult AccessDenied()
        {
            return View();
        }

        public IActionResult Authentication()
        {
            var claimIdentity = new ClaimsIdentity(new List<Claim>
            {
                new Claim("Name", "TestUser")
            }, "CookieAuth");

            var secondIdentiy = new ClaimsIdentity(new List<Claim>
            {
                new Claim("Dob", "Teee"),
                new Claim(ClaimTypes.Actor,"Test")
            }, "CookieAuth");

            var user = new ClaimsPrincipal(new List<ClaimsIdentity>
            {
                claimIdentity,
                secondIdentiy
            });
            
            HttpContext.SignInAsync(user);

            return RedirectToAction("Secret");
        }
    }
}
