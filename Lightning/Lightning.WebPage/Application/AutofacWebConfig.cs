using Autofac;
using Autofac.Extensions.DependencyInjection;
using Lightning.Core.Dependency;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace Lightning.WebPage.Application
{
    public class AutofacWebConfig
    {
        /*
        /// <summary>
        /// 执行起始
        /// </summary>
        public static void Run(IServiceCollection services)
        {
            AutofacRegister(services);
        }
        /// <summary>
        /// 依赖注入
        /// </summary>
        public static IServiceProvider AutofacRegister(IServiceCollection services)
        {
            var builder = new ContainerBuilder();//实例化 AutoFac  容器   

            var assemblys = Assembly.Load("Lightning.Application");//Service是继承接口的实现方法类库名称
            var baseType = typeof(IDependency);//IDependency 是一个接口（所有要实现依赖注入的接口都要继承该接口）
            builder.RegisterAssemblyTypes(assemblys)
            .Where(m => baseType.IsAssignableFrom(m) && m != baseType)
            .AsImplementedInterfaces().InstancePerLifetimeScope();
            builder.Populate(services);
            var container = builder.Build();

            //设置依赖注入解析器
            DependencyResolver.SetResolver(new AutofacDependencyResolver(container));
        }
        public IServiceProvider ConfigureServices(IServiceCollection services)
        {
            //AutofacWebConfig.Run(services);
            services.Configure<CookiePolicyOptions>(options =>
            {
                // This lambda determines whether user consent for non-essential cookies is needed for a given request.
                options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });
            services.AddDbContext<LightningDbContext>(d => d.UseMySQL(Configuration.GetConnectionString("Default")));
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);

            var builder = new ContainerBuilder();//实例化 AutoFac  容器   

            var assemblys = Assembly.Load("Lightning.Application");//Service是继承接口的实现方法类库名称
            var assemblyes = Assembly.Load("Lightning.EntityFramework");//Service是继承接口的实现方法类库名称
            var baseType = typeof(IDependency);//IDependency 是一个接口（所有要实现依赖注入的借口都要继承该接口）
            builder.RegisterAssemblyTypes(Assembly.GetEntryAssembly())
              .Where(m => baseType.IsAssignableFrom(m) && m != baseType)
              .AsImplementedInterfaces().InstancePerLifetimeScope();

            builder.Populate(services);
            var ApplicationContainer = builder.Build();
            return new AutofacServiceProvider(ApplicationContainer);//第三方IOC接管 core内置DI容器
        }*/
    }
}
