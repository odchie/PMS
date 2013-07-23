using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Pms.Entity.Interface;

namespace Pms.Entity
{
    public class CompetencyGroup : Lookup, ICompetencyGroup
    {
        public string CompetencyId { get; set; }
    }
}
