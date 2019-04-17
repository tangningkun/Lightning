using System;
using System.IO;
using System.Reflection;
using Lightning.EntityFramework;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
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
            
            services.AddDbContext<LightningDbContext>(d => d.UseMySql(Configuration.GetConnectionString("Default")));
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("First", new Info
                {
                    Version = "First",
                    Title = "Lightning接口文档",
                    Description = "Lightning API",
                    TermsOfService = "None",
                    //作者信息
                    Contact = new Contact
                    {
                        Name = "TangNingKun",
                        Email = "1209229446@qq.com",
                        Url = ""
                    },
                    //许可证
                    //License = new License
                    //{
                    //    Name = "许可证名字",
                    //    Url = "http://www.cnblogs.com/Scholars/"
                    //}
                });
                // 下面三个方法为 Swagger JSON and UI设置xml文档注释路径
                // Set the comments path for the Swagger JSON and UI.
                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                //var basePath = Path.GetDirectoryName(typeof(Program).Assembly.Location);//获取应用程序所在目录（绝对，不受工作目录影响，建议采用此方法获取路径）
                //var xmlPath = Path.Combine(basePath, "Lightning.WebApi.xml");
                c.IncludeXmlComments(xmlPath);
            });
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

            // 配置Swagger  必须加在app.UseMvc前面
            app.UseSwagger();
            //Swagger Core需要配置的  必须加在app.UseMvc前面
            app.UseSwaggerUI(options =>
            {
                options.SwaggerEndpoint("/swagger/First/swagger.json", "LightningAPI");
            });

            app.UseHttpsRedirection();
            app.UseMvc();
        }
    }
}
