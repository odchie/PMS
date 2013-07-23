using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Pms.Entity.Interface;

namespace Pms.Entity
{
    public class Job : TimeStamp, IJob
    {
        public string JobId { get; set; }
        public string Code { get; set; }
        public long MaxSalary { get; set; }
        public bool MaxSalarySpecified { get; set; }
        public long MinSalary { get; set; }
        public bool minSalarySpecified { get; set; }
        public string Status { get; set; }
        public string Title { get; set; }
    }
}
