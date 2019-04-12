using Lightning.Domain.Entities;
using Lightning.EntityFramework.Repository;
using System;
using System.Collections.Generic;
using System.Text;

namespace Lightning.EntityFramework.Repositories.UserRepositories
{
    public interface IUserRepository:IRepository<User>
    {
    }
}
