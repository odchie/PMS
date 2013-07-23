using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

using PmsVariable = Pms.Common.Variable;
using Pms.Common.Helper;
using Pms.Entity;
using Pms.Entity.Interface;
using Pms.Presentation.Mvc.Helper.Attribute;
using Pms.Repository;

namespace Pms.Presentation.Mvc.Models
{
    public class EmployeeModel : PersonModel, IEmployee
    {
        #region Public property
        public string EmployeeId { get; set; }

        [LocalizedDisplayName("Employee_Type")]
        public string EmployeeTypeId { get; set; }

        [LocalizedDisplayName("Employee_Email")]
        public string Email { get; set; }

        [LocalizedDisplayName("Employee_CareerPathId")]
        public string CareerPathId { get; set; }

        [LocalizedDisplayName("Employee_CorporateLocationNumber")]
        public string CorporateLocationNumber { get; set; }

        [LocalizedDisplayName("Employee_Competency")]
        public string CompetencyId { get; set; }

        [LocalizedDisplayName("Employee_CompetencyGroup")]
        public string CompetencyGroupId { get; set; }

        [LocalizedDisplayName("Employee_CommisionPct")]
        public string CommisionPct { get; set; }

        [LocalizedDisplayName("Employee_EmployeeStatus")]
        public string EmployeeStatus { get; set; }

        [LocalizedDisplayName("Employee_OfficeLocationId")]
        public string OfficeLocationId { get; set; }

        [LocalizedDisplayName("Employee_ProjectManager")]
        public string ProjectManagerId { get; set; }

        [LocalizedDisplayName("Employee_Manager")]
        public string ManagerId { get; set; }
        public IManager ManagerObject { get; set; }

        [LocalizedDisplayName("Employee_Number")]
        public string Number { get; set; }

        [LocalizedDisplayName("Employee_HireDate")]
        [DataType(DataType.Date, ErrorMessageResourceName = "Validation_Date", ErrorMessageResourceType = typeof(Resources.Pms)), DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:MM/dd/yyyy}")]
        public DateTime HireDate { get; set; }

        public bool HireDateSpecified { get; set; }

        [LocalizedDisplayName("Employee_RecordStatus")]
        public string RecordStatus { get; set; }

        [LocalizedDisplayName("Employee_Rohq")]
        public string Rohq { get; set; }

        [LocalizedDisplayName("Employee_StaffId")]
        public string StaffId { get; set; }

        [LocalizedDisplayName("Employee_Salary")]
        public string Salary { get; set; }

        [LocalizedDisplayName("Employee_SeatNumber")]
        public string SeatNumber { get; set; }

        [LocalizedDisplayName("Employee_Department")]
        public string DepartmentId { get; set; }
        public IDepartment DepartmentObject { get; set; }

        public IPerson PersonObject { get; set; }
        public PersonModel PersonModel { get; set; }

        [LocalizedDisplayName("Employee_CareerLevel")]
        public string CareerLevelId { get; set; }
        public ICareerLevel CareerLevelObject { get; set; }

        [LocalizedDisplayName("Employee_Job")]
        public string JobId { get; set; }

        public IJob JobObject { get; set; }

        public string AccountType { get; set; }

        public string Language { get; set; }

        public string CreateBy { get; set; }

        public DateTime? CreateDate { get; set; }

        public bool CreateDateSpecified { get; set; }

        public string UpdateBy { get; set; }

        public DateTime? UpdateDate { get; set; }

        public bool UpdateDateSpecified { get; set; }

        public string UpdateReason { get; set; }

        public IList<EmployeeType> ListEmployeeType
        {
            get
            {
                return PmsVariable.EmployeeType.ToList();
            }
        }
        public IList<ManagerType> ListManagerType
        {
            get
            {
                return PmsVariable.ManagerType.ToList();
            }
        }

        public IList<ICareerLevel> ListCareerLevel { get; set; }
        public IList<ICompetency> ListCompetency { get; set; }
        public IList<ICompetencyGroup> ListCompetencyGroup { get; set; }
        public IList<IManager> ListManager { get; set; }
        public IList<IStaffManager> ListStaffManager { get; set; }
        public IList<IOfficeLocation> ListOfficeLocation { get; set; }
        #endregion

        #region Public method
        public void LoadList(ILookupServiceRepository lookupServiceRepository)
        {

            this.ListCareerLevel = lookupServiceRepository.GetAllCareerLevel();
            this.ListManager = lookupServiceRepository.GetAllManager();
            this.ListStaffManager = lookupServiceRepository.GetAllStaffManager();
            this.ListOfficeLocation = lookupServiceRepository.GetAllOfficeLocation();
        }
        #endregion
    }
}