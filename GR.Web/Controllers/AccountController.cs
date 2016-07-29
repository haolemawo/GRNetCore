using System; 
using System.Threading.Tasks;
using GR.Core;
using GR.Core.Security;
using GR.Services.Account;
using GR.Services.Account.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.Authentication;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling MVC for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace GR.Web.Controllers
{
    [Authorize]
    public class AccountController : BaseController
    {
        private readonly AccountService _accountService;

        public AccountController(AccountService accountService)
        {
            _accountService = accountService;
        }

        [AllowAnonymous]
        // GET: /<controller>/
        public IActionResult Login(string returnUrl = null)
        {

            ViewBag.IsInvalid = false;
            ViewBag.ErrorMsg = "";
            ViewBag.ReturnUrl = returnUrl;
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginViewModel model, string returnUrl = null)
        {
            if (ModelState.IsValid)
            {
                //123456-20A386F664CB306D8D61437BF15002E2
                model.Password = MD5EncryptProvider.Encrypt(model.Password);
                var userPrincipal = _accountService.SignIn(model);
                if (userPrincipal != null)
                {
                    //
                    await HttpContext.Authentication.SignInAsync(Constants.CONSTANTS_LOGIN_COOKIE, userPrincipal,
                        new AuthenticationProperties
                        {
                            ExpiresUtc = DateTime.UtcNow.AddMinutes(20),
                            IsPersistent = false,
                            AllowRefresh = false
                        });
                    //
                    if (returnUrl != null && returnUrl.Replace('/', ' ').Trim() != string.Empty)
                    {
                        return RedirectToLocal(returnUrl);
                    }
                    return RedirectToAction("Index", "Home");
                }
                ViewBag.IsInvalid = true;
                ViewBag.ErrorMsg = "账号或密码错误";
                return View(model);
            }
            return View(model);
        }

        public async Task SignOut()
        {
            await HttpContext.Authentication.SignOutAsync(Constants.CONSTANTS_LOGIN_COOKIE);
        }

        public IActionResult Forbidden()
        {
            return View();
        }

        private IActionResult RedirectToLocal(string returnUrl)
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
    }
}
