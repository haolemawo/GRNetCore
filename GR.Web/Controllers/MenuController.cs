using System;
using GR.Services.Menus;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling MVC for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace GR.Web.Controllers
{
    [Authorize]
    public class MenuController : BaseController
    {
        private readonly MenuService _menuService;

        public MenuController(MenuService menuService)
        {
            _menuService = menuService;
        }

        // GET: /<controller>/
        public IActionResult Index()
        {
            return RedirectToAction("List");
        }
        
        public IActionResult List()
        {
            var list = _menuService.GetAllMenu();
            return View();
        }

        public IActionResult Edit()
        {
            return View();
        }


        public IActionResult Create()
        {
            return View();
        }
        
        public IActionResult Delete()
        {
            return View();
        }
    }
}
