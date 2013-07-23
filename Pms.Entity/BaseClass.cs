using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Pms.Entity.Interface;

namespace Pms.Entity
{
    public abstract class BaseClass : IBase
    {
        public virtual string CreateBy { get; set; }
        public virtual DateTime? CreateDate { get; set; }
        public virtual bool CreateDateSpecified { get; set; }
        public virtual string UpdateBy { get; set; }
        public virtual DateTime? UpdateDate { get; set; }
        public virtual bool UpdateDateSpecified { get; set; }
        public virtual string UpdateReason { get; set; }
    }
}
