using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Lightning.Application.Departments;
using Lightning.Application.Users;
using Lightning.Core.AutoMapper;
using Lightning.EntityFramework;
using Lightning.EntityFramework.Repositories.DepartmentRepositiories;
using Lightning.EntityFramework.Repositories.UserRepositories;
using Lightning.WebApi.Application;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Swashbuckle.AspNetCore.Swagger;

namespace Lightning.WebApi
{
    public class Startup
    {
        public IConfiguration Configuration { get; }
        public Startup(IHostingEnvironment env, IConfiguration configuration)
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
            /** 添加jwt验证：*/
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options => {
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuer = true,//是否验证Issuer
                        ValidateAudience = true,//是否验证Audience
                        ValidateLifetime = true,//是否验证失效时间
                        ValidateIssuerSigningKey = true,//是否验证SecurityKey
                        ValidAudience = Configuration["JwtSetting:Audience"],//Audience
                        ValidIssuer = Configuration["JwtSetting:Issuer"],//Issuer，这两项和前面签发jwt的设置一致
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["JwtSetting:SecurityKey"]))//拿到SecurityKey
                    };
                });


            services.AddDbContext<LightningDbContext>(options => options.UseMySql(Configuration.GetConnectionString("Default")));

            /** config配置文件注入 */
            services.AddTransient<ApiConfigurtaionServices>();

            /**  服务，仓储的注入 */
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IUserAppService, UserAppService>();
            services.AddScoped<IDepartmentRepositiory, DepartmentRepositiory>();
            services.AddScoped<IDepartmentAppService, DepartmentAppService>();

            /** AutoMapper的配置初始化 */
            AutoMapperRegister();


            /** 注册Swagger生成器，定义一个或多个Swagger文档 */
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("Lightning", new Info { Title = "LightningAPI", Version = "Lightning" });
            });

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


            /** jwt验证 */
            app.UseAuthentication();//注意添加这一句，启用jwt验证

            app.UseHttpsRedirection();

            /** 启用中间件以将生成的Swagger作为JSON端点提供服务。*/
            app.UseSwagger();

            /** 启用中间件以提供swagger-ui（HTML，JS，CSS等），*/
            /** 指定Swagger JSON端点。 */
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/Lightning/swagger.json", "LightningAPI");
            });

            /** 跨域配置 */
            app.UseCors("LightningAny");

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
