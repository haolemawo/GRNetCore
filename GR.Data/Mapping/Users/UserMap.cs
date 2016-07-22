using System;
using GR.Core.Domain.Users;
using Microsoft.EntityFrameworkCore;

namespace GR.Mapping.Users
{
    public class UserMap
    {
        public static void Map(ModelBuilder modelBuilder)
        {
            //表映射
            modelBuilder.Entity<User>().ToTable("User");
            //主键映射
            modelBuilder.Entity<User>().HasKey(t => t.Id).HasName("PK_USER_ID");
            //列映射
            modelBuilder.Entity<User>().Property(t => t.Id).HasColumnName("Id");
            modelBuilder.Entity<User>().Property(t => t.UserName).IsRequired().HasColumnName("UserName").HasColumnType("nvarchar(100)");
            modelBuilder.Entity<User>().Property(t => t.Password).IsRequired().HasColumnName("Password").HasColumnType("nvarchar(100)");
            modelBuilder.Entity<User>().Property(t => t.IsActived).HasColumnName("IsActived");

        }
    }
}
