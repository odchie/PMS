using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Pms.Entity.Interface;

namespace Pms.Entity
{
    public class CareerLevel : TimeStamp, ICareerLevel
    {
        public string CareerLevelId { get; set; }
        public string CareerLevelGroupId { get; set; }
        public string CareerTitle { get; set; }
        public string IndustryExperience { get; set; }
        public string RoleExperience { get; set; }
        public string Route { get; set; }
    }
}
