using System;
using GR.Core.Domain.Roles;

namespace GR.Core.Domain.Menus
{
    public class MenuRole
    {
        public int MenuId { get; set; }

        public int RoleId { get; set; }

        public Menu Menu { get; set; }

        public Role Role { get; set; }
    }
}
