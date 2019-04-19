using Lightning.Domain.Entities;
using Lightning.EntityFramework.Repository;
using System;
using System.Collections.Generic;
using System.Text;

namespace Lightning.EntityFramework.Repositories.DepartmentRepositiories
{
    public class DepartmentRepositiory : LightningRepository<Department>, IDepartmentRepositiory
    {
        public DepartmentRepositiory(LightningDbContext dbContext) : base(dbContext)
        {

        }
    }
}
