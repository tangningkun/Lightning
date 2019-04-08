using Lightning.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Lightning.EntityFramework
{
    public class LightningDbContext : DbContext
    {
        public LightningDbContext(DbContextOptions<LightningDbContext> options) : base(options)
        {
        }
        public DbSet<User> Users { get; set; }
        public DbSet<Menu> Menus { get; set; }
        public DbSet<Department> Departments { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<UserRole> UserRoles { get; set; }
        public DbSet<RoleMenu> RoleMenus { get; set; }

        //自定义DbContext实体属性名与数据库表对应名称（默认 表名与属性名对应是 User与Users）
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>().ToTable("Users");
            modelBuilder.Entity<Menu>().ToTable("Menus");
            modelBuilder.Entity<Department>().ToTable("Departments");
            modelBuilder.Entity<Role>().ToTable("Roles");
            //UserRole关联配置
            modelBuilder.Entity<UserRole>().ToTable("UserRoles")
              .HasKey(ur => new { ur.UserId, ur.RoleId }); 

            //RoleMenu关联配置
            modelBuilder.Entity<RoleMenu>().ToTable("RoleMenus")
              .HasKey(rm => new { rm.RoleId, rm.MenuId });

            base.OnModelCreating(modelBuilder);
        }
    }
}
