using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Pms.Entity.Interface
{
    public interface ILookup: ITimeStamp
    {
        string Id { get; set; }
        string Name { get; set; }
        string Description { get; set; }
    }
}
