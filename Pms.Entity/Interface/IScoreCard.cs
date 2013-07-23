using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Pms.Entity.Interface
{
    public interface IScoreCard : IBase
    {
        string CompetencyGroupId { get; set; }
        bool CompetencyGroupIdSpecified { get; set; }
        string CompetencyId { get; set; }
        bool CompetencyIdSpecified { get; set; }
        string EmployeeId { get; set; }
        bool EmployeeIdSpecified { get; set; }
        IEmployee EmployeeObject { get; set; }
        string PersonId { get; set; }
        bool PersonIdSpecified { get; set; }
        IPerson PersonObject { get; set; }
        string RecordStatusId { get; set; }
        bool RecordStatusIdSpecified { get; set; }
        string ScoreLevel { get; set; }
        bool ScoreLevelSpecified { get; set; }
        string TechnologyKnowledgeGroupId { get; set; }
        bool TechnologyKnowledgeGroupIdSpecified { get; set; }
        string TechnologyKnowlegeDetailId { get; set; }
        bool TechnologyKnowlegeDetailIdSpecified { get; set; }
        string UpdateReason { get; set; }
        string Year { get; set; }
        bool YearSpecified { get; set; }
    }
}
