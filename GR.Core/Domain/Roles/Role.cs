using System; 

namespace GR.Core.Domain.Roles
{
    public class Role : BaseEntity
    {
        public string RoleName { get; set; }

        public bool IsActived { get; set; }
         
    }
}
