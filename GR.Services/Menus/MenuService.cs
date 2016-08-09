using System;
using System.Collections.Generic;
using System.Linq;
using GR.Core;
using GR.Core.Data;
using GR.Core.Domain.Menus;
using GR.Services.Menus.Models;

namespace GR.Services.Menus
{
    public class MenuService
    {
        private readonly IRepository<Menu> _menuRepository;

        public MenuService(IRepository<Menu> menuRepository)
        {
            _menuRepository = menuRepository;
        }
        
        #region 不涉及权限
        /// <summary>
        /// 获取所有菜单
        /// </summary>
        /// <returns></returns>
        public ReturnResult<MenuListViewModel> GetAllMenu()
        {
            var result = new ReturnResult<MenuListViewModel>();
            result.IsSuccess = true;
            result.Message = "获取成功";
            //获取根节点
            result.Data = new MenuListViewModel();
            result.Data.Items = GetMenuItem();
            //
            return result;
        }

        /// <summary>
        /// 递归获取菜单项
        /// </summary>
        /// <param name="parentId"></param>
        /// <returns></returns>
        protected List<MenuViewModel> GetMenuItem(int? parentId = null)
        {
            List<MenuViewModel> result = null;
            var rootMenus = _menuRepository.GetAll().Where(x => x.ParentId == parentId).ToList();
            if (rootMenus != null && rootMenus.Count > 0)
            {
                result = new List<MenuViewModel>();
                rootMenus.ForEach(r =>
                {
                    var model = new MenuViewModel
                    {
                        Id = r.Id,
                        MenuName = r.MenuName,
                        ParentId = r.ParentId.HasValue ? r.ParentId.Value : 0,
                        AreaName = r.AreaName,
                        ControllerName = r.ControllerName,
                        ActionName = r.ActionName
                    };
                    //获取子节点
                    model.Children = GetMenuItem(r.Id);
                    //
                    result.Add(model);
                });
            }
            return result;
        }
        #endregion
    }
}
