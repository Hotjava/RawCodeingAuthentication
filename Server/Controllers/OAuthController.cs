using Microsoft.AspNetCore.Mvc;

namespace Server.Controllers
{
    public class OAuthController : Controller
    {
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Login(string username)
        {
            return View();
        }

        [HttpPost]
        public IActionResult Token()
        {
            return View();
        }
    }
}
