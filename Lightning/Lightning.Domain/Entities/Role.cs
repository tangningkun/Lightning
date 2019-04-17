using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Lightning.Domain.Entities
{
    public class Role : Entity
    {
        //[MaxLength(36)]
        //public Guid RoleId { get; set; }
        public string Code { get; set; }

        public string Name { get; set; }
        public Guid CreateUserId { get; set; }

        public string Remarks { get; set; }

        public virtual ICollection<RoleMenu> RoleMenus { get; set; }
    }
}
