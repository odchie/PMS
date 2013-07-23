using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Pms.Entity;
using Pms.Entity.Interface;

namespace Pms.Repository
{
    public interface ILookupServiceRepository
    {
        IList<ServiceHeader> ServiceHeaders { set; }
        IList<ICountry> GetAllCountry();
        IList<ICareerLevel> GetAllCareerLevel();
        IList<ICompetency> GetAllCompetency(out IList<CodeMessage> messages);
        IList<ICompetencyManager> GetAllCompetencyManager();
        IList<IDepartment> GetAllDepartment();
        IList<IDepartmentManager> GetAllDepartmentManager();
        IList<IJob> GetAllJob();
        IList<IManager> GetAllManager();
        IList<IStaffManager> GetAllStaffManager();
        IList<IOfficeLocation> GetAllOfficeLocation();
    }
}
