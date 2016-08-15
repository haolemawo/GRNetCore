using System;
using System.Collections.Generic;
using System.Text;
using GR.Services.Menus;
using GR.Services.Menus.Models;
using GR.Web.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling MVC for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace GR.Web.Controllers
{
    [Authorize]
    public class LayoutController : BaseController
    {
        private readonly MenuService _menuService;

        public LayoutController(MenuService menuService)
        {
            _menuService = menuService;
        }

        /// <summary>
        /// 菜单导航
        /// </summary>
        /// <returns></returns>
        public PartialViewResult PartialMenuSidebar()
        {
            var model = _menuService.GetMenuListBy(Convert.ToInt32(UserId));
            if (!model.IsSuccess)
            {
                throw new Exception(model.Message);
            }
            var resultData = new MenuHtml
            {
                Html = GetMenuHtml(model.Data.Items).ToString()
            };
            return PartialView("_PartialMenuSidebar", resultData);
        }

        /// <summary>
        /// 获取菜单的HTML
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        protected StringBuilder GetMenuHtml(MenuViewModel model)
        {
            var menu = new StringBuilder();
            menu.Append(GetMenuItemHtml(model));
            if (!model.IsLeaf)
            {
                menu.Append("<ul class=\"sub - menu\">");
                model.Children.ForEach(x =>
                {
                    GetMenuHtml(x);
                });
                menu.Append("</ul>");
            }
            return menu;
        }

        /// <summary>
        /// 获取菜单的HTML
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        protected StringBuilder GetMenuHtml(List<MenuViewModel> models)
        {
            var menu = new StringBuilder();
            models.ForEach(x =>
            {
                if (x.IsLeaf)
                {
                    menu.Append(GetMenuItemHtml(x));
                }
                else
                {
                    menu.Append("<ul class=\"sub - menu\">");
                    menu.Append("<a href=\"javascript:; \" class=\"nav - link nav - toggle\">");
                    menu.AppendFormat("<i class=\"{0}\"></i>", x.Icon);
                    menu.AppendFormat("<span class=\"title\">{0}</span>", x.MenuName);
                    menu.Append("<span class=\"arrow\"></span>");
                    menu.Append("</a>");
                    //
                    menu.Append(GetMenuHtml(x.Children));
                    //
                    menu.Append("</ul>");
                }
            });
            return menu;
        }

        /// <summary>
        /// 获取菜单项的HTML
        /// </summary>
        /// <param name="model"></param>
        /// <param name="isActive"></param>
        /// <returns></returns>
        protected StringBuilder GetMenuItemHtml(MenuViewModel model, bool isActive = false)
        {
            var item = new StringBuilder();

            if (isActive)
            {
                item.Append(" <li class=\"nav - item  \">");
            }
            else
            {
                item.Append(" <li class=\"nav - item  active open\">");
            }
            item.AppendFormat("<a asp-area=\"{0}\" asp-controller=\"{1}\" asp-action=\"{2}\" class=\"nav - link \">", string.IsNullOrEmpty(model.AreaName) ? "" : model.AreaName, model.ControllerName, model.ActionName);
            if (!string.IsNullOrEmpty(model.Icon))
            {
                item.AppendFormat("<i class=\"{0}\"></i>", model.Icon);
            }
            item.AppendFormat("  <span class=\"title\">{0}</span>", model.MenuName);
            item.Append(" </a>");
            item.Append(" </li>");
            return item;
        }

        // GET: /<controller>/
        public IActionResult Footer()
        {
            return View();
        }
    }
}
