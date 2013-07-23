using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Pms.Entity.Interface
{
    public interface ICareerLevel :  ITimeStamp
    {
        string CareerLevelId { get; set; }
        string CareerLevelGroupId { get; set; }
        string CareerTitle { get; set; }
        string IndustryExperience { get; set; }
        string RoleExperience { get; set; }
        string Route { get; set; }
    }
}
