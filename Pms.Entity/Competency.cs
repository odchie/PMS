using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Pms.Entity.Interface;

namespace Pms.Entity
{
    public class Competency : Lookup, ICompetency
    {
        public string CompetencyId { get; set; }
        public string CompetencyGroupId { get; set; }
        public string ManagerId { get; set; }
        public bool ManagerIdSpecified { get; set; }
        public string RecordStatusId { get; set; }
        public bool RecordStatusIdSpecified { get; set; }
    }
}
