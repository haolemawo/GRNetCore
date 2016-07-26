using System;
using GR.Core.Domain.Roles;
using Microsoft.EntityFrameworkCore;

namespace GR.Data.Mapping.Roles
{
    public class RoleMap
    {
        public static void Map(ModelBuilder modelBuilder)
        {
            //表映射
            modelBuilder.Entity<Role>().ToTable("Role");
            //主键映射
            modelBuilder.Entity<Role>().HasKey(t => t.Id).HasName("PK_ROLE_ID");
            //列映射
            modelBuilder.Entity<Role>().Property(t => t.Id).HasColumnName("Id");
            modelBuilder.Entity<Role>().Property(t => t.RoleName).IsRequired().HasColumnName("RoleName").HasColumnType("nvarchar(100)");
            modelBuilder.Entity<Role>().Property(t => t.IsActived).HasColumnName("IsActived");
        }
    }
}
