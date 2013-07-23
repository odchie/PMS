using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Pms.Entity.Interface;

namespace Pms.Entity
{
    public class Employee : Person, IEmployee
    {
        public string CorporateLocationNumber { get; set; }
        public string CompetencyId { get; set; }
        public string CompetencyGroupId { get; set; }
        public string CommisionPct { get; set; }
        public string EmployeeId { get; set; }
        public string Email { get; set; }
        public string EmployeeTypeId { get; set; }
        public string EmployeeStatus { get; set; }
        public DateTime HireDate { get; set; }
        public bool HireDateSpecified { get; set; }
        public string Number { get; set; }
        public string OfficeLocationId { get; set; }
        public string ProjectManagerId { get; set; }
        public string RecordStatus { get; set; }
        public string Rohq { get; set; }
        public string StaffId { get; set; }
        public string Salary { get; set; }
        public string SeatNumber { get; set; }

        public string DepartmentId { get; set; }
        public IDepartment DepartmentObject { get; set; }

        public string ManagerId { get; set; }
        public IManager ManagerObject { get; set; }

        public string PersonId { get; set; }
        public IPerson PersonObject { get; set; }

        public string CareerPathId { get; set; }
        public string CareerLevelId { get; set; }
        public ICareerLevel CareerLevelObject { get; set; }

        public string JobId { get; set; }
        public IJob JobObject { get; set; }

        //Fields needed for service
        public string AccountType { get; set; }
        public string Language { get; set; }
    }
}
