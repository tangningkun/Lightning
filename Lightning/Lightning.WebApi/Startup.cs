using System;
using System.Collections.Generic;
using System.IO;
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
        private const string ApiName = "Lightning.WebApi";
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



            /** 跨域配置 */
            services.AddCors(p =>
            {
                p.AddPolicy("LightningAny", builder =>
                {
                    builder.AllowAnyOrigin()//允许任何源
                    .AllowAnyMethod()//允许任何方式
                    .AllowAnyHeader()//允许任何头
                    .AllowCredentials();//允许cookie


                });
                ////一般采用这种方法
                //p.AddPolicy("LimitRequests", policy =>
                //{
                //    // 支持多个域名端口，注意端口号后不要带/斜杆：比如localhost:8000/，是错的http://localhost:43398
                //    // 注意，http://127.0.0.1:1818 和 http://localhost:1818 是不一样的，尽量写两个
                //    policy
                //    .WithOrigins("http://127.0.0.1:43398", "http://localhost:43398", "http://localhost:19763", "http://127.0.0.1:19763")
                //    .AllowAnyHeader()//Ensures that the policy allows any header.
                //    .AllowAnyMethod();
                //});
            });

            #region Swagger UI Service

            var basePath = Microsoft.DotNet.PlatformAbstractions.ApplicationEnvironment.ApplicationBasePath;
            services.AddSwaggerGen(c =>
            {
                //遍历出全部的版本，做文档信息展示
                typeof(ApiVersions).GetEnumNames().ToList().ForEach(version =>
                {
                    c.SwaggerDoc(version, new Info
                    {
                        // {ApiName} 定义成全局变量，方便修改
                        Version = version,
                        Title = $"{ApiName} 接口文档",
                        Description = $"{ApiName} HTTP API " + version,
                        TermsOfService = "None",
                        // Contact = new Contact { Name = "Tang.Core", Email = "Autho.JWT@xxx.com", Url = "https://www.jianshu.com/u/94102b59cc2a" }
                    });
                    // 按相对路径排序，作者：Alby
                    c.OrderActionsBy(o => o.RelativePath);
                });


                //就是这里
                var xmlPath = Path.Combine(basePath, "Lightning.WebApi.xml");//这个就是刚刚配置的xml文件名
                c.IncludeXmlComments(xmlPath, true);//默认的第二个参数是false，这个是controller的注释，记得修改
                //var xmlModelPath = Path.Combine(basePath, "Tang.Core.Model.xml");//这个就是Model层的xml文件名
                //c.IncludeXmlComments(xmlModelPath);


                #region Token绑定到ConfigureServices
                //添加header验证信息
                //c.OperationFilter<SwaggerHeader>();

                // 发行人
                var IssuerName = (Configuration.GetSection("Audience"))["Issuer"];
                var security = new Dictionary<string, IEnumerable<string>> { { IssuerName, new string[] { } }, };
                c.AddSecurityRequirement(security);

                //方案名称“Autho.JWT”可自定义，上下一致即可
                c.AddSecurityDefinition(IssuerName, new ApiKeyScheme
                {
                    Description = "JWT授权(数据将在请求头中进行传输) 直接在下框中输入Bearer {token}（注意两者之间是一个空格）\"",
                    Name = "Authorization",//jwt默认的参数名称
                    In = "header",//jwt默认存放Authorization信息的位置(请求头中)
                    Type = "apiKey"
                });
                #endregion
            });
            #endregion

            #region 【简单授权】

                #region 1、基于角色的API授权 

                // 1【授权】、这个很简单，其他什么都不用做，
                // 无需配置服务，只需要在API层的controller上边，增加特性即可，注意，只能是角色的:
                // [Authorize(Roles = "Admin")]

                // 2【认证】、然后在下边的configure里，配置中间件即可:app.UseMiddleware<JwtTokenAuth>();但是这个方法，无法验证过期时间，所以如果需要验证过期时间，还是需要下边的第三种方法，官方认证

                #endregion

                #region 2、基于策略的授权（简单版）

                // 1【授权】、这个和上边的异曲同工，好处就是不用在controller中，写多个 roles 。
                // 然后这么写 [Authorize(Policy = "Admin")]
                services.AddAuthorization(options =>
                {
                    options.AddPolicy("Client", policy => policy.RequireRole("Client").Build());
                    options.AddPolicy("Admin", policy => policy.RequireRole("Admin").Build());
                    options.AddPolicy("SystemOrAdmin", policy => policy.RequireRole("Admin", "System"));
                });


                // 2【认证】、然后在下边的configure里，配置中间件即可:app.UseMiddleware<JwtTokenAuth>();但是这个方法，无法验证过期时间，所以如果需要验证过期时间，还是需要下边的第三种方法，官方认证
                #endregion
            #endregion


            #region 【认证】
            //读取配置文件
            var audienceConfig = Configuration.GetSection("Audience");
            var symmetricKeyAsBase64 = audienceConfig["Secret"];
            var keyByteArray = Encoding.ASCII.GetBytes(symmetricKeyAsBase64);
            var signingKey = new SymmetricSecurityKey(keyByteArray);


            //2.1【认证】
            services.AddAuthentication(x =>
            {
                x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
             .AddJwtBearer(o =>
             {
                 o.TokenValidationParameters = new TokenValidationParameters
                 {
                     ValidateIssuerSigningKey = true,
                     IssuerSigningKey = signingKey,
                     ValidateIssuer = true,
                     ValidIssuer = audienceConfig["Issuer"],//发行人
                     ValidateAudience = true,
                     ValidAudience = audienceConfig["Audience"],//订阅人
                     ValidateLifetime = true,
                     ClockSkew = TimeSpan.Zero,
                     RequireExpirationTime = true,
                 };

             });
            #endregion



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



            /** 跨域配置 */
            app.UseCors("LightningAny");

            #region Swagger

            /** 启用中间件以将生成的Swagger作为JSON端点提供服务。*/

            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                //根据版本名称倒序 遍历展示
                typeof(ApiVersions).GetEnumNames().OrderByDescending(e => e).ToList().ForEach(version =>
                {
                    c.SwaggerEndpoint($"/swagger/{version}/swagger.json", $"{ApiName} {version}");
                });
                //c.IndexStream = () => GetType().GetTypeInfo().Assembly.GetManifestResourceStream("Blog.Core.index.html");
                c.RoutePrefix = ""; //路径配置，设置为空，表示直接在根域名（localhost:8001）访问该文件,注意localhost:8001/swagger是访问不到的，去launchSettings.json把launchUrl去掉
            });
            #endregion


            //此授权认证方法已经放弃，请使用下边的官方验证方法。但是如果你还想传User的全局变量，还是可以继续使用中间件
            //app.UseJwtTokenAuth(); //app.UseMiddleware<JwtTokenAuth>();

            //如果你想使用官方认证，必须在上边ConfigureService 中，配置JWT的认证服务 (.AddAuthentication 和 .AddJwtBearer 二者缺一不可)
            app.UseAuthentication();

            app.UseMvc();
        }
        /// <summary>
        /// AutoMapper的配置初始化
        /// </summary>
        private void AutoMapperRegister()
        {
            new AutoMapperStartupTask().Execute();
        }
        public enum ApiVersions
        {
            /// <summary>
            /// v1 版本
            /// </summary>
            v1 = 1,
            /// <summary>
            /// v2 版本
            /// </summary>
            v2 = 2,
        }
    }
}
