using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.AspNetCore.Mvc;

namespace Server.Controllers
{
    public class OAuthController : Controller
    {
        [HttpGet]
        public IActionResult Login(
            string response_type, // authorization flow type
            string client_id, // client id
            string redirect_uri,
            string scope, // what information do you need
            string state ) // random string generated to confirm that we are going back to same client
        {

            var qry = new QueryBuilder(); 
            qry.Add("redirect_uri",redirect_uri);
            qry.Add("state", state);

            return View(model:qry.ToString());
        }

        [HttpPost]
        public IActionResult Login(string username, 
            string redirect_uri, 
            string state)
        {
            const string code = "BABABA";

            var qry = new QueryBuilder();
            
            qry.Add("code", code);
            qry.Add("state", state);
            return Redirect($"{redirect_uri}{qry.ToString()}");
        }

        [HttpPost]
        public IActionResult Token(
            string grant_type, 
            string code, 
            string redirect_uri,
            string client_id
            
            )
        {
            return View();
        }
    }
}
