using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Pms.Entity;

namespace Pms.Entity.Interface
{
    public interface IEmployee : IBase, IPerson
    {
        string CorporateLocationNumber { get; set; }
        string CompetencyId { get; set; }
        string CompetencyGroupId { get; set; }
        string CommisionPct { get; set; }
        string EmployeeId { get; set; }
        string EmployeeTypeId { get; set; }
        string EmployeeStatus { get; set; }
        string Email { get; set; }
        DateTime HireDate { get; set; }
        bool HireDateSpecified { get; set; }
        string OfficeLocationId { get; set; }
        string Number { get; set; }
        string ProjectManagerId { get; set; }
        string RecordStatus { get; set; }
        string Rohq { get; set; }
        string StaffId { get; set; }
        string Salary { get; set; }
        string SeatNumber { get; set; }

        string DepartmentId { get; set; }
        IDepartment DepartmentObject { get; set; }

        string ManagerId { get; set; }
        IManager ManagerObject { get; set; }

        string PersonId { get; set; }
        IPerson PersonObject { get; set; }

        string CareerPathId { get; set; }
        string CareerLevelId { get; set; }
        ICareerLevel CareerLevelObject { get; set; }

        string JobId { get; set; }
        IJob JobObject { get; set; }

        //Fields needed for service
        string AccountType { get; set; }
        string Language { get; set; }
    }
}
