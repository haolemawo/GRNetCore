using System; 
using GR.Core.Domain.Menus;
using Microsoft.EntityFrameworkCore;

namespace GR.Mapping.Menus
{
    public class MenuRoleMap
    {
        public static void Map(ModelBuilder modelBuilder)
        {
            //表映射
            modelBuilder.Entity<MenuRole>().ToTable("MenuRoleMapping");
            //主键映射
            modelBuilder.Entity<MenuRole>().HasKey(t => new { t.RoleId, t.MenuId }).HasName("PK_MENU_ROLE_MENUROLEID");
            //列映射
            modelBuilder.Entity<MenuRole>().Property(t => t.RoleId).HasColumnName("RoleId");
            modelBuilder.Entity<MenuRole>().Property(t => t.MenuId).HasColumnName("MenuId");
            //关系映射
            modelBuilder.Entity<MenuRole>().HasOne(t => t.Menu).WithMany(t => t.MenuRoles).HasForeignKey(t => t.MenuId).HasConstraintName("FK_MENU_ROLE_MENUID");

        }
    }
}
