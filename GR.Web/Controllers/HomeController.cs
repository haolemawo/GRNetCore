using System;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GR.Web.Controllers
{
    [Authorize]
    //[Authorize(Roles = "Administrator")]
    public class HomeController : BaseController
    {
        public IActionResult Index()
        {
            ViewData["Title"] = "首页";
            return View();
        }
    }
}
