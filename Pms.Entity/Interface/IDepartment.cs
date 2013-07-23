using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Pms.Entity.Interface
{
    public interface IDepartment : ILookup
    {
        string DepartmentId { get; set; }
        string LocationId { get; set; }
        string ManagerId { get; set; }
        string Status { get; set; }
    }
}
