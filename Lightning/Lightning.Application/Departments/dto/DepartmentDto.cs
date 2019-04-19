using System;
using System.Collections.Generic;
using System.Text;

namespace Lightning.Application.Departments.dto
{
    public class DepartmentDto
    {
        /// <summary>
        /// 部门名称Id
        /// </summary>
        public Guid Id { get; set; }
        /// <summary>
        /// 部门名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 部门编号
        /// </summary>
        public string Code { get; set; }

        /// <summary>
        /// 父级部门ID
        /// </summary>
        public Guid ParentId { get; set; }
    }
}
