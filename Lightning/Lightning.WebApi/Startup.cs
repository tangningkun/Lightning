using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Lightning.Application.Departments;
using Lightning.Application.Users;
using Lightning.Core.AutoMapper;
using Lightning.EntityFramework;
using Lightning.EntityFramework.Repositories.DepartmentRepositiories;
using Lightning.EntityFramework.Repositories.UserRepositories;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Swashbuckle.AspNetCore.Swagger;

namespace Lightning.WebApi
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<LightningDbContext>(options => options.UseMySql(Configuration.GetConnectionString("Default")));

            ///** 服务，仓储的注入 */
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IUserAppService, UserAppService>();
            services.AddScoped<IDepartmentRepositiory, DepartmentRepositiory>();
            services.AddScoped<IDepartmentAppService, DepartmentAppService>();

            //AutoMapper的配置初始化
            AutoMapperRegister();


            // 注册Swagger生成器，定义一个或多个Swagger文档
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("Lightning", new Info { Title = "LightningAPI", Version = "Lightning" });
            });

            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);
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
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();

            //启用中间件以将生成的Swagger作为JSON端点提供服务。
            app.UseSwagger();

            //启用中间件以提供swagger-ui（HTML，JS，CSS等），
            //指定Swagger JSON端点。
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/Lightning/swagger.json", "LightningAPI");
            });

            app.UseMvc();
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
