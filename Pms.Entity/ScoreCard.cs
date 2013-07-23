using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Pms.Entity.Interface;

namespace Pms.Entity
{
    public class ScoreCard : BaseClass, IScoreCard
    {
        public string CompetencyGroupId { get; set; }
        public bool CompetencyGroupIdSpecified { get; set; }
        public string CompetencyId { get; set; }
        public bool CompetencyIdSpecified { get; set; }
        public string EmployeeId { get; set; }
        public bool EmployeeIdSpecified { get; set; }
        public IEmployee EmployeeObject { get; set; }
        public string PersonId { get; set; }
        public bool PersonIdSpecified { get; set; }
        public IPerson PersonObject { get; set; }
        public string RecordStatusId { get; set; }
        public bool RecordStatusIdSpecified { get; set; }
        public string ScoreLevel { get; set; }
        public bool ScoreLevelSpecified { get; set; }
        public string TechnologyKnowledgeGroupId { get; set; }
        public bool TechnologyKnowledgeGroupIdSpecified { get; set; }
        public string TechnologyKnowlegeDetailId { get; set; }
        public bool TechnologyKnowlegeDetailIdSpecified { get; set; }
        public string UpdateReason { get; set; }
        public string Year { get; set; }
        public bool YearSpecified { get; set; }
    }
}
