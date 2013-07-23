using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Pms.Entity.Interface;

namespace Pms.Entity
{
    public class Department: Lookup, IDepartment
    {
        public string DepartmentId { get; set; }
        public string LocationId { get; set; }
        public string ManagerId { get; set; }
        public string Status { get; set; }
    }
}
