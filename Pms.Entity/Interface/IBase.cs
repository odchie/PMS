using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Pms.Entity.Interface
{
    public interface IBase
    {
        string CreateBy { get; set; }
        DateTime? CreateDate { get; set; }
        bool CreateDateSpecified { get; set; }
        string UpdateBy { get; set; }
        DateTime? UpdateDate { get; set; }
        bool UpdateDateSpecified { get; set; }
        string UpdateReason { get; set; }
    }
}
