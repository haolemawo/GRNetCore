using System;
using GR.Core.Domain.Menus;
using GR.Core.Domain.Roles;
using GR.Core.Domain.Users;
using GR.Mapping.Menus;
using GR.Mapping.Roles;
using GR.Mapping.Users;
using Microsoft.EntityFrameworkCore;

namespace GR
{
    /// <summary>
    /// 数据库链接上下文
    /// </summary>
    public class GRDbContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<Menu> Menus { get; set; }
        public DbSet<UserRole> UserRoles { get; set; }
        public DbSet<MenuRole> MenuRoles { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Data Source=.;Initial Catalog=NetCoreDb;Integrated Security=False;Persist Security Info=False;User ID=sa;Password=Passw0rd");
            base.OnConfiguring(optionsBuilder);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            TableMap(modelBuilder);
            //
            base.OnModelCreating(modelBuilder);
        }

        /// <summary>
        /// 表结构映射
        /// </summary>
        /// <param name="modelBuilder"></param>
        protected void TableMap(ModelBuilder modelBuilder)
        {
            UserMap.Map(modelBuilder);
            RoleMap.Map(modelBuilder);
            MenuMap.Map(modelBuilder);
            UserRoleMap.Map(modelBuilder);
            MenuRoleMap.Map(modelBuilder);
        }
    }
}
