using System;
using System.Collections.Generic;
using System.Linq;
using GR.Core;
using GR.Core.Data;
using GR.Core.Domain.Menus;
using GR.Core.Domain.Roles;
using GR.Core.Domain.Users;
using GR.Services.Menus.Models;

namespace GR.Services.Menus
{
    public class MenuService
    {
        private readonly IRepository<Menu> _menuRepository;
        //private readonly IRepository<Role> _roleRepository;
        //private readonly IRepository<User> _userRepository;

        public MenuService(IRepository<Menu> menuRepository/*, IRepository<Role> roleRepository, IRepository<User> userRepository*/)
        {
            _menuRepository = menuRepository;
           // _roleRepository = roleRepository;
        }

        ///// <summary>
        ///// 获取所有菜单
        ///// </summary>
        ///// <returns></returns>
        //public ReturnResult<MenuListViewModel> GetAllMenu(List<int> roleIds)
        //{
        //    var result = new ReturnResult<MenuListViewModel>();
        //    result.IsSuccess = true;
        //    result.Message = "获取成功";
        //    //获取根节点
        //    result.Data = new MenuListViewModel();
        //    result.Data.Items = GetMenuItem(roleIds);
        //    //
        //    return result;
        //}

        ///// <summary>
        ///// 递归获取菜单项
        ///// </summary>
        ///// <param name="roleId"></param>
        ///// <param name="parentId"></param>
        ///// <returns></returns>
        //protected ReturnResult<List<MenuViewModel>> GetMenuItem(List<int> roleIds, int? parentId = null)
        //{
        //    var roles = _roleRepository.GetAll().Where(x => roleIds.Contains(x.Id)).ToList();

        //}

        ///// <summary>
        ///// 根据userid获取导航菜单
        ///// </summary>
        ///// <param name="userId"></param>
        ///// <returns></returns>
        //public ReturnResult<MenuListViewModel> GetMenuListBy(int userId)
        //{
        //    /*
        //     * 
        //     declare @userId int 
        //     set @userId=8 

        //     select * from [dbo].[Menu] where id in  (
        //     select distinct m.Id from [dbo].[User] as u 
        //     join [dbo].[UserRoleMapping] as urm on urm.[UserId]=u.Id
        //     join [dbo].[Role] as r on r.Id=urm.RoleId
        //     join [dbo].[MenuRoleMapping] as mrm on mrm.RoleId=r.Id
        //     join [dbo].[Menu] as m on m.Id=mrm.MenuId
        //     where u.Id=@userId)  
        //     */
        //    var result = new ReturnResult<MenuListViewModel>();
        //    result.IsSuccess = true;
        //    result.Message = "成功";
        //    var user = _userRepository.FirstOrDefault(userId);
        //    if (user == null)
        //    {
        //        result.Message = "该用户不存在";
        //        result.IsSuccess = false;
        //        return result;
        //    }
        //    if (user.UserRoles.Count <= 0)
        //    {
        //        result.Message = "该用户无访问权限";
        //        result.IsSuccess = false;
        //        return result;
        //    }
        //    var roleIds = new List<int>();
        //    user.UserRoles.ForEach(x =>
        //    {
        //        roleIds.Add(x.RoleId);
        //    });
        //    //var roles=_roleRepository.
        //    //_menuRepository.GetAll().Where(x=> x.MenuRoles.Where(mr=> roleIds.Contains(mr.RoleId)))
        //}

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
