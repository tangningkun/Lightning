using Lightning.Core.Dependency;
using Lightning.Domain;
using Lightning.EntityFramework.Repository;
using System;
using System.Collections.Generic;
using System.Text;

namespace Lightning.Application.AppServices
{
    public abstract class AppService<TEntity> : IAppService<TEntity> where TEntity : Entity
    {
        private readonly IRepository<TEntity> _repositiory;
        public AppService(IRepository<TEntity> repositiory)
        {
            _repositiory = repositiory;
        }
    }
}
