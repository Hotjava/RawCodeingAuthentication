using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

namespace Server.Controllers
{
    public class HomeController : Controller
    {

        public HomeController()
        {
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

        public IActionResult Authenticate()
        {

            var claims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Name, "TestUser"),
                new Claim(JwtRegisteredClaimNames.Iss, "Localhost"),
                new Claim(ClaimTypes.Actor,"Test")
            };

          

            var signinCredential = new SigningCredentials(
                new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Constants.SecretKey)),
                SecurityAlgorithms.HmacSha256
                );
            var token = new JwtSecurityToken(
                Constants.Issuer,
                Constants.Issuer,
                claims,
                notBefore: DateTime.Now,
                expires: DateTime.Now.AddHours(1),
                signinCredential
            );

            var tokenJson = new JwtSecurityTokenHandler().WriteToken(token);
            
            
            return Ok(new {access_token=tokenJson});
        }



    }
}
