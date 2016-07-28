using System;
using GR.Core.Security;
using GR.Services.Account;
using GR.Services.Account.Models;
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
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Login(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                //123456-20A386F664CB306D8D61437BF15002E2
                model.Password = MD5EncryptProvider.Encrypt(model.Password);
                var user = _accountService.Login(model);
                if (user != null)
                { 
                    return RedirectToAction("Index", "Home");
                }
                return View(model);
            }
            return View(model);
        }
    }
}
