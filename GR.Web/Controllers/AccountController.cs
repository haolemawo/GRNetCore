using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using GR.Core;
using GR.Core.Security;
using GR.Services.Account;
using GR.Services.Account.Models;
using Microsoft.AspNetCore.Http.Authentication;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling MVC for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace GR.Web.Controllers
{
    public class AccountController : BaseController
    {
        private readonly AccountService _accountService;

        public AccountController(AccountService accountService)
        {
            _accountService = accountService;
        }

        // GET: /<controller>/
        public IActionResult Login(string returnUrl = null)
        {
            ViewBag.ReturnUrl = returnUrl;
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginViewModel model, string returnUrl = null)
        {
            if (ModelState.IsValid)
            {
                //123456-20A386F664CB306D8D61437BF15002E2
                model.Password = MD5EncryptProvider.Encrypt(model.Password);
                var user = _accountService.Login(model);
                if (user != null)
                {
                    await SignInAsync(user);
                    //
                    if (returnUrl != null && returnUrl.Replace('/', ' ').Trim() != string.Empty)
                    {
                        return RedirectToLocal(returnUrl);
                    }
                    return RedirectToAction("Index", "Home");
                }
                return View(model);
            }
            return View(model);
        }
        
        public async Task SignOut()
        {
            await  HttpContext.Authentication.SignOutAsync(ConstConfig.CONFIG_LOGIN_COOKIE);
        }

        public IActionResult Forbidden()
        {
            return View();
        }


        private async Task SignInAsync(UserModel model)
        {
            string issuer = "http://localhost:7493/";
            var claims = new List<Claim>();
            claims.Add(new Claim(ClaimTypes.Name, model.UserName, ClaimValueTypes.String, issuer));
            claims.Add(new Claim(ClaimTypes.Role, "Administrator", ClaimValueTypes.String, issuer));
            var userIdentity = new ClaimsIdentity("SuperSecureLogin_" + model.UserId);
            userIdentity.AddClaims(claims);
            var userPrincipal = new ClaimsPrincipal(userIdentity);
            //
            await HttpContext.Authentication.SignInAsync(ConstConfig.CONFIG_LOGIN_COOKIE, userPrincipal,
                new AuthenticationProperties
                {
                    ExpiresUtc = DateTime.UtcNow.AddMinutes(20),
                    IsPersistent = false,
                    AllowRefresh = false
                });
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
