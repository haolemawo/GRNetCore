using System;
using System.Collections.Generic; 

namespace GR.Services.Menus.Models
{
    public class MenuViewModel
    { 
        public int Id { get; set; }

        public int ParentId { get; set; }

        public string MenuName { get; set; }

        public string AreaName { get; set; }

        public string ControllerName { get; set; }

        public string ActionName { get; set; }

        public List<MenuViewModel> Children { get; set; }

        /// <summary>
        /// 是否是叶子节点
        /// </summary>
        public Boolean IsLeaf
        {
            get
            {
                if (Children == null ||(Children.Count <= 0))
                {
                    return false;
                }
                return true;
            }
        }
    }
}
