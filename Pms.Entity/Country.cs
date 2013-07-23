using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Pms.Entity.Interface;

namespace Pms.Entity
{
    public class Country : Lookup, ICountry 
    {
        public string RegionId { get; set; }
    }
}
