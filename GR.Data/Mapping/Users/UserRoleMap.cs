using System;
using GR.Core.Domain.Users;
using Microsoft.EntityFrameworkCore;

namespace GR.Data.Mapping.Users
{
    public class UserRoleMap
    {
        public static void Map(ModelBuilder modelBuilder)
        {
            //表映射
            modelBuilder.Entity<UserRole>().ToTable("UserRoleMapping");
            //主键映射
            //modelBuilder.Entity<UserRole>().HasKey(t => new {t.RoleId, t.UserId}).HasName("PK_USER_ROLE_USERROLEID");
            modelBuilder.Entity<UserRole>().HasKey(t => new { t.Id }).HasName("PK_USER_ROLE_USERROLEID");
            //列映射
            modelBuilder.Entity<UserRole>().Property(t => t.Id).HasColumnName("Id");
            modelBuilder.Entity<UserRole>().Property(t => t.RoleId).HasColumnName("RoleId");
            modelBuilder.Entity<UserRole>().Property(t => t.UserId).HasColumnName("UserId");
            //关系映射
            modelBuilder.Entity<UserRole>().HasOne(t => t.User).WithMany(t => t.UserRoles).HasForeignKey(t => t.UserId).HasConstraintName("FK_USER_ROLE_USERID"); ;

        }
    }
}
