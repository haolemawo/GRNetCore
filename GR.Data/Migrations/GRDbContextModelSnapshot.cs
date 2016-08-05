using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using GR.Data;

namespace GR.Data.Migrations
{
    [DbContext(typeof(GRDbContext))]
    partial class GRDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
            modelBuilder
                .HasAnnotation("ProductVersion", "1.0.0-rtm-21431")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("GR.Core.Domain.Menus.Menu", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("Id");

                    b.Property<string>("ActionName")
                        .HasColumnName("ActionName")
                        .HasColumnType("nvarchar(500)");

                    b.Property<string>("AreaName")
                        .HasColumnName("AreaName")
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("ControllerName")
                        .HasColumnName("ControllerName")
                        .HasColumnType("nvarchar(100)");

                    b.Property<bool>("IsActived")
                        .HasColumnName("IsActived");

                    b.Property<string>("MenuName")
                        .IsRequired()
                        .HasColumnName("MenuName")
                        .HasColumnType("nvarchar(100)");

                    b.Property<int?>("ParentId")
                        .HasColumnName("ParentId");

                    b.HasKey("Id")
                        .HasName("PK_MENU_ID");

                    b.HasIndex("ParentId");

                    b.ToTable("Menu");
                });

            modelBuilder.Entity("GR.Core.Domain.Menus.MenuRole", b =>
                {
                    b.Property<int>("RoleId")
                        .HasColumnName("RoleId");

                    b.Property<int>("MenuId")
                        .HasColumnName("MenuId");

                    b.HasKey("RoleId", "MenuId")
                        .HasName("PK_MENU_ROLE_MENUROLEID");

                    b.HasIndex("MenuId");

                    b.HasIndex("RoleId");

                    b.ToTable("MenuRoleMapping");
                });

            modelBuilder.Entity("GR.Core.Domain.Roles.Role", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("Id");

                    b.Property<bool>("IsActived")
                        .HasColumnName("IsActived");

                    b.Property<string>("RoleName")
                        .IsRequired()
                        .HasColumnName("RoleName")
                        .HasColumnType("nvarchar(100)");

                    b.HasKey("Id")
                        .HasName("PK_ROLE_ID");

                    b.ToTable("Role");
                });

            modelBuilder.Entity("GR.Core.Domain.Users.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("Id");

                    b.Property<bool>("IsActived")
                        .HasColumnName("IsActived");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnName("Password")
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("UserName")
                        .IsRequired()
                        .HasColumnName("UserName")
                        .HasColumnType("nvarchar(100)");

                    b.HasKey("Id")
                        .HasName("PK_USER_ID");

                    b.ToTable("User");
                });

            modelBuilder.Entity("GR.Core.Domain.Users.UserRole", b =>
                {
                    b.Property<int>("RoleId")
                        .HasColumnName("RoleId");

                    b.Property<int>("UserId")
                        .HasColumnName("UserId");

                    b.HasKey("RoleId", "UserId")
                        .HasName("PK_USER_ROLE_USERROLEID");

                    b.HasIndex("RoleId");

                    b.HasIndex("UserId");

                    b.ToTable("UserRoleMapping");
                });

            modelBuilder.Entity("GR.Core.Domain.Menus.Menu", b =>
                {
                    b.HasOne("GR.Core.Domain.Menus.Menu", "Parent")
                        .WithMany("Children")
                        .HasForeignKey("ParentId")
                        .HasConstraintName("FK_PARENT_CHILDREN_PARENTID");
                });

            modelBuilder.Entity("GR.Core.Domain.Menus.MenuRole", b =>
                {
                    b.HasOne("GR.Core.Domain.Menus.Menu", "Menu")
                        .WithMany("MenuRoles")
                        .HasForeignKey("MenuId")
                        .HasConstraintName("FK_MENU_ROLE_MENUID")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("GR.Core.Domain.Roles.Role", "Role")
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("GR.Core.Domain.Users.UserRole", b =>
                {
                    b.HasOne("GR.Core.Domain.Roles.Role", "Role")
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("GR.Core.Domain.Users.User", "User")
                        .WithMany("UserRoles")
                        .HasForeignKey("UserId")
                        .HasConstraintName("FK_USER_ROLE_USERID")
                        .OnDelete(DeleteBehavior.Cascade);
                });
        }
    }
}
