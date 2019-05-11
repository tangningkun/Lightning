using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Lightning.Application.Departments;
using Lightning.Application.Users;
using Lightning.Core.AutoMapper;
using Lightning.EntityFramework;
using Lightning.EntityFramework.Migrations.SeedData;
using Lightning.EntityFramework.Repositories.DepartmentRepositiories;
using Lightning.EntityFramework.Repositories.UserRepositories;
using Lightning.WebPage.Application;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.FileProviders;

namespace Lightning.WebPage
{
    public class Startup
    {
        public IConfigurationRoot Configuration { get; }
        public Startup(IHostingEnvironment env)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true);
            builder.AddEnvironmentVariables();
            Configuration = builder.Build();

            //初始化映射关系
            //FonourMapper.Initialize();
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<LightningDbContext>(options => options.UseMySql(Configuration.GetConnectionString("Default")));
            /*services.Configure<CookiePolicyOptions>(options =>
            {
                // This lambda determines whether user consent for non-essential cookies is needed for a given request.
                options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });*/
            /** config配置文件注入 */
            services.AddTransient<AppConfigurtaionServices>();
            /** 服务，仓储的注入 */
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IUserAppService, UserAppService>();
            services.AddScoped<IDepartmentRepositiory, DepartmentRepositiory>();
            services.AddScoped<IDepartmentAppService, DepartmentAppService>();

            /** 跨域配置 */
            services.AddCors(options =>
            {
                options.AddPolicy("LightningAny", builder =>
                {
                    builder.AllowAnyOrigin() //允许任何来源的主机访问
                    .AllowAnyMethod()
                    .AllowAnyHeader()
                    .AllowCredentials();//指定处理cookie

                });
            });

            services.AddMvc();
            /** Session服务 */
            services.AddSession();

            /** AutoMapper的配置初始化 */
            AutoMapperRegister();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            /** Session服务 */
            app.UseSession();

            /** 使用静态文件 */
            app.UseStaticFiles();

            app.UseStaticFiles(new StaticFileOptions()
            {
                FileProvider = new PhysicalFileProvider(Directory.GetCurrentDirectory())
            });

            /** 跨域配置 */
            app.UseCors("LightningAny");

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=account}/{action=Login}");
            });
            //DefaultSettingsCreator.Initialize(app.ApplicationServices); //初始化数据
        }

        /// <summary>
        /// AutoMapper的配置初始化
        /// </summary>
        private void AutoMapperRegister()
        {
            new AutoMapperStartupTask().Execute();
        }
    }
}
