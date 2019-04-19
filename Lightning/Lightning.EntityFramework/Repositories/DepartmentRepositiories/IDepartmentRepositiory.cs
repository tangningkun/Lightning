using System;
using System.Collections.Generic;
using System.Text;
using Lightning.Domain.Entities;
using Lightning.EntityFramework.Repository;

namespace Lightning.EntityFramework.Repositories.DepartmentRepositiories
{
    public interface IDepartmentRepositiory : IRepository<Department>
    {
    }
}
