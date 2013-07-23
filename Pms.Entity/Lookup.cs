using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Pms.Entity.Interface;

namespace Pms.Entity
{
    public class Lookup : TimeStamp, ILookup
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
    }
}
