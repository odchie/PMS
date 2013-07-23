using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Pms.Entity.Interface
{
    public interface ICompetency : ILookup 
    {
        string CompetencyId { get; set; }
        string CompetencyGroupId { get; set; }
        string ManagerId { get; set; }
        bool ManagerIdSpecified { get; set; }
        string RecordStatusId { get; set; }
        bool RecordStatusIdSpecified { get; set; }
    }
}
