using Lightning.Core.Encryption;
using Lightning.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Lightning.EntityFramework.Migrations.SeedData
{
    public static class DefaultSettingsCreator
    {
        public static void Initialize(IServiceProvider serviceProvider)
        {
            using (var context = new LightningDbContext(serviceProvider.GetRequiredService<DbContextOptions<LightningDbContext>>()))
            {
                var count = context.Users.Any();
                if (context.Users.Any())
                {
                    return;   // 已经初始化过数据，直接返回
                }
                Guid departmentId = Guid.NewGuid();
                //增加一个部门
                context.Departments.Add(
                   new Department
                   {
                       Id = departmentId,
                       Name = "Lightning总办",
                       ParentId = Guid.Empty,
                       CreateTime=DateTime.Now,
                       IsDeleted=true
                   }
                );
                //增加一个超级管理员用户
                context.Users.Add(
                     new User
                     {
                         Id = Guid.NewGuid(),
                         UserName = "admin",
                         Password = Encryptor.Md5Hash("123456"), //暂不进行加密
                         Name = "超级管理员",
                         DepartmentId = departmentId,
                         CreateTime = DateTime.Now,
                         IsDeleted=true
                     }
                );
                //增加四个基本功能菜单
                context.Menus.AddRange(
                   new Menu
                   {
                       Id = Guid.NewGuid(),
                       Name = "组织机构管理",
                       Code = "Department",
                       SerialNumber = 0,
                       ParentId = Guid.Empty,
                       Icon = "fa fa-link",
                       CreateTime=DateTime.Now
                   },
                   new Menu
                   {
                       Id = Guid.NewGuid(),
                       Name = "角色管理",
                       Code = "Role",
                       SerialNumber = 1,
                       ParentId = Guid.Empty,
                       Icon = "fa fa-link",
                       CreateTime = DateTime.Now
                   },
                   new Menu
                   {
                       Id= Guid.NewGuid(),
                       Name = "用户管理",
                       Code = "User",
                       SerialNumber = 2,
                       ParentId = Guid.Empty,
                       Icon = "fa fa-link",
                       CreateTime = DateTime.Now
                   },
                   new Menu
                   {
                       Id = Guid.NewGuid(),
                       Name = "功能管理",
                       Code = "Department",
                       SerialNumber = 3,
                       ParentId = Guid.Empty,
                       Icon = "fa fa-link",
                       CreateTime = DateTime.Now
                   }
                );
                context.SaveChanges();
            }
        }
    }
}
