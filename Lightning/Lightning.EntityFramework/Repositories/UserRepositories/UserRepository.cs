using Lightning.Domain.Entities;
using Lightning.EntityFramework.Repository;
using System;
using System.Collections.Generic;
using System.Text;

namespace Lightning.EntityFramework.Repositories.UserRepositories
{
    public class UserRepository : LightningRepository<User>, IUserRepository
    {
        public UserRepository(LightningDbContext dbContext) : base(dbContext)
        {

        }
    }
}
