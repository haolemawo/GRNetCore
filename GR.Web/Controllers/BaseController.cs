using System;
using System.Linq;
using System.Security.Claims;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling MVC for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace GR.Web.Controllers
{
    public abstract class BaseController : Controller
    {
        protected IActionResult RedirectToLocal(string returnUrl)
        {
            if (Url.IsLocalUrl(returnUrl))
            {
                return Redirect(returnUrl);
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
        }

        protected string UserName
        {
            get
            {
                return User.Identities.First(u => u.IsAuthenticated && u.HasClaim(c => c.Type == ClaimTypes.Name)).FindFirst(ClaimTypes.Name).Value;
            }
        }

        protected string UserId
        {
            get
            {
                return User.Identities.First(u => u.IsAuthenticated && u.HasClaim(c => c.Type == ClaimTypes.UserData)).FindFirst(ClaimTypes.UserData).Value;
            }
        }
    }
}
