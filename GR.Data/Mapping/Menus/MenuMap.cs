using System;
using GR.Core.Domain.Menus;
using Microsoft.EntityFrameworkCore;

namespace GR.Data.Mapping.Menus
{
    public class MenuMap
    {
        public static void Map(ModelBuilder modelBuilder)
        {
            //表映射
            modelBuilder.Entity<Menu>().ToTable("Menu");
            //主键映射
            modelBuilder.Entity<Menu>().HasKey(t => t.Id).HasName("PK_MENU_ID");
            //列映射
            modelBuilder.Entity<Menu>().Property(t => t.Id).HasColumnName("Id");
            modelBuilder.Entity<Menu>().Property(t => t.ParentId).HasColumnName("ParentId");
            modelBuilder.Entity<Menu>().Property(t => t.MenuName).IsRequired().HasColumnName("RoleName").HasColumnType("nvarchar(100)");
            modelBuilder.Entity<Menu>().Property(t => t.Url).HasColumnName("Url").HasColumnType("nvarchar(1000)");
            modelBuilder.Entity<Menu>().Property(t => t.IsActived).HasColumnName("IsActived");
            //关系映射
            modelBuilder.Entity<Menu>()
                .HasOne(t => t.Parent)
                .WithMany(t => t.Children)
                .HasForeignKey(t => t.ParentId)
                .HasConstraintName("FK_PARENT_CHILDREN_PARENTID");

        }
    }
}
