using Lightning.Application.Departments.dto;
using Lightning.Core.ListResult;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Lightning.Application.Departments
{
    public interface IDepartmentAppService
    {
        Task<List<DepartmentDto>> GetAllDepartment();
    }
}
