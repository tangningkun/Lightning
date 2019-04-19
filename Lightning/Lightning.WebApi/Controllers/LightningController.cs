using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Lightning.Application.Departments;
using Lightning.Application.Departments.dto;
using Lightning.Core.ListResult;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Lightning.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LightningController : ControllerBase
    {
        private readonly IDepartmentAppService _departmentAppService;
        public LightningController(IDepartmentAppService departmentAppService)
        {
            _departmentAppService = departmentAppService;
        }
        /// <summary>
        /// 获取部门信息
        /// </summary>
        /// <returns></returns>
        [HttpPost("GetAllDepartment")]
        public async Task<List<DepartmentDto>> GetAllDepartment()
        {
            var result = await _departmentAppService.GetAllDepartment();
            return result;
        }
    }
}