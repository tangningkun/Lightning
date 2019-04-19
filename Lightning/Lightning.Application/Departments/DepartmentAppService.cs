using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Lightning.Application.Departments.dto;
using Lightning.Core.ListResult;
using Lightning.Domain.Entities;
using Lightning.EntityFramework.Repositories.DepartmentRepositiories;
using Lightning.EntityFramework.Repository;
using Lightning.Core.AutoMapper;
using System.Linq;

namespace Lightning.Application.Departments
{
    public class DepartmentAppService : IDepartmentAppService
    {
        private readonly IDepartmentRepositiory _departmentRepositiory;
        public DepartmentAppService(IDepartmentRepositiory departmentRepositiory)
        {
            _departmentRepositiory = departmentRepositiory;
        }
        public async Task<List<DepartmentDto>> GetAllDepartment()
        {
            var items = await _departmentRepositiory.GetAllListAsync(d => d.IsDeleted == true);
            var count = items.Count;
            var result = new List<DepartmentDto>(
                items.Select(item=> 
                {
                    var dto = new DepartmentDto();
                    dto = item.MapTo<Department, DepartmentDto>();
                    return dto;
                }).ToList()
            );
            return result;
        }
    }
}
