using System;
using System.Collections.Generic;

namespace GR.Core.Domain.Users
{
    public class User : BaseEntity
    {
        public string UserName { get; set; }

        public string Password { get; set; }

        public bool IsActived { get; set; }
        
        public List<UserRole> UserRoles { get; set; }
    }
}
