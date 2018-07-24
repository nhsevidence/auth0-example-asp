using System.Security.Claims;
using System.Web;
using System.Web.Mvc;
using Auth0ExampleAsp.ViewModels;
using Microsoft.Owin.Security;
using Microsoft.Owin.Security.Cookies;

namespace Auth0ExampleAsp.Controllers
{
    public class AccountController : Controller
    {
        public ActionResult Login(string returnUrl)
        {
            HttpContext.GetOwinContext().Authentication.Challenge(new AuthenticationProperties
                {
                    RedirectUri = returnUrl ?? Url.Action("Index", "Home")
                },
                "Auth0");
            return new HttpUnauthorizedResult();
        }

        [Authorize]
        public void Logout()
        {
            HttpContext.GetOwinContext().Authentication.SignOut(CookieAuthenticationDefaults.AuthenticationType);
            HttpContext.GetOwinContext().Authentication.SignOut("Auth0");
        }

        [Authorize]
        public ActionResult Claims()
        {
            return View();
        }
        
        [Authorize]
        public ActionResult UserProfile()
        {
            ClaimsIdentity claimsIdentity = User.Identity as ClaimsIdentity;

            return View(new UserProfileViewModel()
            {
                Name = claimsIdentity?.FindFirst(c => c.Type == claimsIdentity.NameClaimType)?.Value,
                EmailAddress = claimsIdentity?.FindFirst(c => c.Type == ClaimTypes.Email)?.Value,
                ProfileImage = claimsIdentity?.FindFirst(c => c.Type == "picture")?.Value
            });
        }

        [Authorize(Roles = "staff")]
        public ActionResult Admin()
        {
            return View();
        }
    }
}