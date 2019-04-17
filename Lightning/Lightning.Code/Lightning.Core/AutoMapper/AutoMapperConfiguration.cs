using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lightning.Core.AutoMapper
{
    /// <summary>
    /// AutoMapper的全局实体映射配置静态类
    /// </summary>
    public static class AutoMapperConfiguration
    {
        public static void Init()
        {
            MapperConfiguration = new MapperConfiguration(cfg =>
            {

                #region Post
                //将领域实体映射到视图实体
                //cfg.CreateMap<Post, PostViewModel>()
                //    .ForMember(d => d.IsDeleted, d => d.MapFrom(s => s.IsDeleted ? "是" : "否")) //将布尔类型映射成字符串类型的是/否
                //;
                ////将视图实体映射到领域实体
                //cfg.CreateMap<PostViewModel, Post>();
                #endregion
            });

            Mapper = MapperConfiguration.CreateMapper();
        }

        public static IMapper Mapper { get; private set; }

        public static MapperConfiguration MapperConfiguration { get; private set; }
    }
}
