using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Pms.Entity.Interface
{
    public interface IJob : IBase
    {
        string JobId { get; set; }
        string Code { get; set; }
        long MaxSalary { get; set; }
        bool MaxSalarySpecified { get; set; }
        long MinSalary { get; set; }
        bool minSalarySpecified { get; set; }
        string Status { get; set; }
        string Title { get; set; }
    }
}
