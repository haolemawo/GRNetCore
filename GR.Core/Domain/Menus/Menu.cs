using System;
using System.Collections.Generic;

namespace GR.Core.Domain.Menus
{
    public class Menu : BaseEntity
    {
        public int? ParentId { get; set; }

        public string MenuName { get; set; }

        public string Url { get; set; }

        public bool IsActived { get; set; }

        public Menu Parent { get; set; }

        public List<Menu> Children { get; set; }

        public List<MenuRole> MenuRoles { get; set; }
    }
}
