using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using pmsEntity = Pms.Entity;

namespace Pms.Common.Variable
{
    public struct GeneralString
    {
        /// <summary>
        ///  Default country string
        /// </summary>
        public const string DefaultCountry = "PH";

        /// <summary>
        ///  Comma string
        /// </summary>
        public const string CharacterComma = ",";

        /// <summary>
        ///  Comma string
        /// </summary>
        public const string CharacterColon = ":";

        /// <summary>
        ///  Dash string
        /// </summary>
        public const string CharacterDash = "-";

        /// <summary>
        ///  Complex delimiter string
        /// </summary>
        public const string ComplexDelimiter = "~|~";
    }

    public struct RouteVariables
    {
        /// <summary>
        ///  Controller variable
        /// </summary>
        public const string Controller = "controller";

        /// <summary>
        ///  Action variable
        /// </summary>
        public const string Action = "action";

        /// <summary>
        ///  Language variable
        /// </summary>
        public const string Language = "language";
    }

    public struct ControllerString
    {
        /// <summary>
        ///  Home controller
        /// </summary>
        public const string Home = "Home";

        /// <summary>
        ///  Common controller
        /// </summary>
        public const string Common = "Common";

        /// <summary>
        ///  Account controller
        /// </summary>
        public const string Account = "Account";

        /// <summary>
        ///  User Management controller
        /// </summary>
        public const string UserManagement = "UserManagement";
    }

    public struct ControllerActionString
    {
        /// <summary>
        ///  Index action
        /// </summary>
        public const string Index = "Index";

        /// <summary>
        ///  Create action
        /// </summary>
        public const string Create = "Create";

        /// <summary>
        ///  Delete action
        /// </summary>
        public const string Delete = "Delete";

        /// <summary>
        ///  Edit action
        /// </summary>
        public const string Edit = "Edit";

        /// <summary>
        ///  Detail action
        /// </summary>
        public const string Detail = "Detail";

        /// <summary>
        ///  Search action
        /// </summary>
        public const string Search = "Search";

        /// <summary>
        ///  Error action
        /// </summary>
        public const string Error = "Error";

        /// <summary>
        ///  Notification action
        /// </summary>
        public const string Notification = "Notification";

        /// <summary>
        ///  Login action
        /// </summary>
        public const string Login = "Login";
    }

    public struct CodeNameObjectProperty
    {
        /// <summary>
        ///  Code property
        /// </summary>
        public const string Id = "Id";

        /// <summary>
        ///  Name property
        /// </summary>
        public const string Name = "Name";
    }

    public struct ConfigurationKey
    {
        /// <summary>
        ///  Key for cache time out
        /// </summary>
        public const string CacheTimeOut = "Pms_CacheTimeOut";

        /// <summary>
        ///  Key for languages
        /// </summary>
        public const string Languages = "Pms_Languages";

        /// <summary>
        ///  Key for connection string active directory
        /// </summary>
        public const string ActiveDirectoryConnectionString = "ADConnectionString";

        /// <summary>
        ///  Key for username to use in service
        /// </summary>
        public const string WebServiceUsername = "Pms_Service_Username";

        /// <summary>
        ///  Key for password to use in service
        /// </summary>
        public const string WebServicePassword = "Pms_Service_Password";
    }

    public struct SearchKey
    {
        /// <summary>
        ///  Id field
        /// </summary>
        public const string Id = "ID";

        /// <summary>
        ///  Email field
        /// </summary>
        public const string Email = "EMAIL";
    }

    public struct SessioKey
    {
        /// <summary>
        ///  Key for credential
        /// </summary>
        public const string LoginCredential = "r_09ikL";

        /// <summary>
        ///  Key for Career Level lookup
        /// </summary>
        public const string LookupCareerLevel = "tkdi_0";

        /// <summary>
        ///  Key for Competency lookup
        /// </summary>
        public const string LookupCompetency = "cRdipo";

        /// <summary>
        ///  Key for CompetencyGroup lookup
        /// </summary>
        public const string LookupCompetencyGroup = "gmx_0ta";

        /// <summary>
        ///  Key for CompetencyManager lookup
        /// </summary>
        public const string LookupCompetencyManager = "fd_kki9i";

        /// <summary>
        ///  Key for Department lookup
        /// </summary>
        public const string LookupDepartment = "f_dg90s";

        /// <summary>
        ///  Key for DepartmentManager lookup
        /// </summary>
        public const string LookupDepartmentManager = "ccmdk_09i";

        /// <summary>
        ///  Key for Job lookup
        /// </summary>
        public const string LookupJob = "vfr_98a";

        /// <summary>
        ///  Key for Manager lookup
        /// </summary>
        public const string LookupManager = "TY45iL";

        /// <summary>
        ///  Key for OfficeLocation lookup
        /// </summary>
        public const string LookupOfficeLocation = "xs_4ilJ";

        /// <summary>
        ///  Key for StaffManager lookup
        /// </summary>
        public const string LookupStaffManager = "45oUkix";
    }

    public struct ServiceHeaderKey
    {
        /// <summary>
        ///  Header name for Username
        /// </summary>
        public const string UserName = "UserName";

        /// <summary>
        ///  Header name for email
        /// </summary>
        public const string Email = "Email";

        /// <summary>
        ///  Header name for EmployeeId
        /// </summary>
        public const string EmployeeId = "EmployeeId";
    }

    public struct CacheKeys
    {
        /// <summary>
        ///  Key for Country list
        /// </summary>
        public const string CountryList = "Ds0Ywd!1";
    }

    public struct ServiceCodeMessage
    {
        /// <summary>
        ///  Code for success
        /// </summary>
        public const string Success = "Success";
    }

    public struct PostParameter
    {
        /// <summary>
        ///  Post parameter for Email
        /// </summary>
        public const string Email = "email";

        /// <summary>
        ///  Post parameter for Firstname
        /// </summary>
        public const string Firstname = "firstName";

        /// <summary>
        ///  Post parameter for Lastname
        /// </summary>
        public const string Lastname = "lastName";
    }

    public struct ErrorMessage
    {
        /// <summary>
        ///  Error message for missing configuration
        /// </summary>
        public const string ConfigurationMissing = "A configuration setting is missing.";
    }

    public struct Sex
    {
        /// <summary>
        ///  Sex string for Male
        /// </summary>
        public static string Male = "Male";

        /// <summary>
        ///  Sex string for Female
        /// </summary>
        public static string Female = "Female";

        /// <summary>
        ///  Convert this struct to a list
        /// </summary>
        /// <returns>List of this struct</returns>
        public static IList<pmsEntity.Sex> ToList()
        {
            IList<pmsEntity.Sex> returnList = new List<pmsEntity.Sex>();
            returnList.Add(new pmsEntity.Sex() { Id = "M", Name = Sex.Male });
            returnList.Add(new pmsEntity.Sex() { Id = "F", Name = Sex.Female });
            return returnList;
        }
    }

    public struct EmployeeType
    {
        /// <summary>
        ///  Employee type of Regular
        /// </summary>
        public static string Regular = "Regular";

        /// <summary>
        ///  Employee type of Contractor
        /// </summary>
        public static string Contractor = "Contractor";

        /// <summary>
        ///  Convert this struct to a list
        /// </summary>
        /// <returns>List of this struct</returns>
        public static IList<pmsEntity.EmployeeType> ToList()
        {
            IList<pmsEntity.EmployeeType> returnList = new List<pmsEntity.EmployeeType>();
            returnList.Add(new pmsEntity.EmployeeType() { Id = "1", Name = EmployeeType.Regular });
            returnList.Add(new pmsEntity.EmployeeType() { Id = "2", Name = EmployeeType.Contractor });
            return returnList;
        }
    }

    public struct PersonType
    {
        /// <summary>
        ///  Person type of Employee
        /// </summary>
        public static string Employee = "Employee";

        /// <summary>
        ///  Person type of Applicant
        /// </summary>
        public static string Applicant = "Applicant";

        /// <summary>
        ///  Convert this struct to a list
        /// </summary>
        /// <returns>List of this struct</returns>
        public static IList<pmsEntity.PersonType> ToList()
        {
            IList<pmsEntity.PersonType> returnList = new List<pmsEntity.PersonType>();
            returnList.Add(new pmsEntity.PersonType() { Id = "E", Name = PersonType.Employee });
            returnList.Add(new pmsEntity.PersonType() { Id = "A", Name = PersonType.Applicant });
            return returnList;
        }
    }

    public struct ManagerType
    {
        /// <summary>
        ///  Manaager type of Tier director
        /// </summary>
        public static string TierDirector = "Tier director";

        /// <summary>
        ///  Person type of Applicant
        /// </summary>
        public static string LineManager = "Line manager";

        /// <summary>
        ///  Manaager type of Staff manager
        /// </summary>
        public static string StaffManager = "Staff manager";

        /// <summary>
        ///  Manaager type of Staff manager
        /// </summary>
        public static string PracticeLead = "Practice lead";

        /// <summary>
        ///  Convert this struct to a list
        /// </summary>
        /// <returns>List of this struct</returns>
        public static IList<pmsEntity.ManagerType> ToList()
        {
            IList<pmsEntity.ManagerType> returnList = new List<pmsEntity.ManagerType>();
            returnList.Add(new pmsEntity.ManagerType() { Id = "1", Name = ManagerType.LineManager });
            returnList.Add(new pmsEntity.ManagerType() { Id = "2", Name = ManagerType.PracticeLead });
            returnList.Add(new pmsEntity.ManagerType() { Id = "3", Name = ManagerType.StaffManager });
            returnList.Add(new pmsEntity.ManagerType() { Id = "4", Name = ManagerType.TierDirector });
            return returnList;
        }
    }


    public struct CivilStatus
    {
        /// <summary>
        ///  Married status
        /// </summary>
        public static string Married = "Married";

        /// <summary>
        ///  Married status
        /// </summary>
        public static string Single = "Single";

        /// <summary>
        ///  Widow status
        /// </summary>
        public static string Widow = "Widow";

        /// <summary>
        ///  Convert this struct to a list
        /// </summary>
        /// <returns>List of this struct</returns>
        public static IList<pmsEntity.CivilStatus> ToList()
        {
            IList<pmsEntity.CivilStatus> returnList = new List<pmsEntity.CivilStatus>();
            returnList.Add(new pmsEntity.CivilStatus() { Id = "M", Name = CivilStatus.Married });
            returnList.Add(new pmsEntity.CivilStatus() { Id = "S", Name = CivilStatus.Single });
            returnList.Add(new pmsEntity.CivilStatus() { Id = "W", Name = CivilStatus.Widow });
            return returnList;
        }
    }
}
