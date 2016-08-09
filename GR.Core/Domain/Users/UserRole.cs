using System;
using GR.Core.Domain.Roles;

namespace GR.Core.Domain.Users
{
    public class UserRole : BaseEntity
    {
        public int UserId { get; set; }

        public int RoleId { get; set; }

        public User User { get; set; }

        public Role Role { get; set; }
    }
}
