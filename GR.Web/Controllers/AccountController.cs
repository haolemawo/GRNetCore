using System;
using GR.Services.Account;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling MVC for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace GR.Web.Controllers
{
    public class AccountController : Controller
    {
       // private readonly AccountService _accountService;
        // GET: /<controller>/
        public IActionResult Login()
        {
            return View();
        }
    }
}
