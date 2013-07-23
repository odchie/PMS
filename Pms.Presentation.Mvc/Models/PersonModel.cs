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
using Pms.Repository;
using Pms.Presentation.Mvc.Helper;
using Pms.Presentation.Mvc.Helper.Attribute;
using Pms.Presentation.Mvc;

namespace Pms.Presentation.Mvc.Models
{
    public class PersonModel : IPerson
    {
        #region Public property
        [LocalizedDisplayName("Person_Id")]
        public string PersonId { get; set; }

        [LocalizedDisplayName("Person_Address")]
        public string Address { get; set; }

        [LocalizedDisplayName("Person_Email")]
        [Required]
        public string ExternalEmail { get; set; }

        public string FullName { get; set; }

        [LocalizedDisplayName("Person_FirstName")]
        [Required]
        public string FirstName { get; set; }

        [LocalizedDisplayName("Person_LastName")]
        [Required]
        public string LastName { get; set; }

        [LocalizedDisplayName("Person_MiddleName")]
        public string MiddleName { get; set; }

        [LocalizedDisplayName("Person_HomeNumber")]
        public string HomeNumber { get; set; }

        [LocalizedDisplayName("Person_Password")]
        public string Password { get; set; }

        [LocalizedDisplayName("Person_ActivationTokenField")]
        public string ActivationToken { get; set; }

        [LocalizedDisplayName("Person_Age")]
        public string Age { get; set; }
        public bool AgeSpecified { get; set; }

        [LocalizedDisplayName("Person_Birthday")]
        [DataType(DataType.Date, ErrorMessageResourceName = "Validation_Date", ErrorMessageResourceType = typeof(Resources.Pms)), DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:MM/dd/yyyy}")]
        public DateTime? Birthday { get; set; }

        [LocalizedDisplayName("Person_BirthdaySpecified")]
        public bool BirthdaySpecified { get; set; }

        [LocalizedDisplayName("Person_CreateBy")]
        public string CreateBy { get; set; }

        [LocalizedDisplayName("Person_CreateDate")]
        [DataType(DataType.Date), DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:MM/dd/yyyy}")]
        public DateTime? CreateDate { get; set; }

        [LocalizedDisplayName("Person_CreateDateSpecified")]
        public bool CreateDateSpecified { get; set; }

        [LocalizedDisplayName("Person_CivilStatus")]
        public string CivilStatus { get; set; }

        [LocalizedDisplayName("Person_Reason")]
        public string Reason { get; set; }

        [LocalizedDisplayName("Person_PassportNumber")]
        public string PassportNumber { get; set; }

        [LocalizedDisplayName("Person_PassportExpiry")]
        public DateTime? PassportExpiry { get; set; }
        public bool PassportExpirySpecified { get; set; }

        [LocalizedDisplayName("Person_PersonStatus")]
        public string PersonStatus { get; set; }

        [LocalizedDisplayName("Person_PersonType")]
        public string PersonType { get; set; }

        [LocalizedDisplayName("Person_RecordStatus")]
        public int RecordStatus { get; set; }
        public bool RecordStatusSpecified { get; set; }

        [LocalizedDisplayName("Person_PrimaryNumber")]
        public string PrimaryNumber { get; set; }

        [LocalizedDisplayName("Person_SecondaryNumber")]
        public string SecondaryNumber { get; set; }

        [LocalizedDisplayName("Person_Sex")]
        public string Sex { get; set; }

        [LocalizedDisplayName("Person_SocialSecurityNumber")]
        public string SocialSecurityNumber { get; set; }

        [LocalizedDisplayName("Person_StatusChangeBy")]
        public string StatusChangeBy { get; set; }

        [LocalizedDisplayName("Person_StatusChangeReason")]
        public string StatusChangeReason { get; set; }

        [LocalizedDisplayName("Person_StatusChangeTime")]
        public DateTime? StatusChangeTime { get; set; }
        public bool StatusChangeTimeSpecified { get; set; }

        [LocalizedDisplayName("Person_StartDateExpiry")]
        public DateTime? StartDateExpiry { get; set; }
        public bool StartDateExpirySpecified { get; set; }

        [LocalizedDisplayName("Person_UpdateBy")]
        public string UpdateBy { get; set; }

        [LocalizedDisplayName("Person_UpdateReason")]
        public string UpdateReason { get; set; }

        [LocalizedDisplayName("Person_UpdateDate")]
        [DataType(DataType.Date), DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:MM/dd/yyyy}")]
        public DateTime? UpdateDate { get; set; }
        public bool UpdateDateSpecified { get; set; }

        [LocalizedDisplayName("Person_CountryObject")]
        public ICountry CountryObject { get; set; }

        [LocalizedDisplayName("Person_Country")]
        public string CountryId { get; set; }
        public bool CountryIdSpecified { get; set; }

        [LocalizedDisplayName("Person_YearsItExperience")]
        public int YearsItExperience { get; set; }
        public bool YearsItExperienceSpecified { get; set; }

        public IList<ICountry> ListCountry { get; set; }

        public IList<CivilStatus> ListCivilStatus
        {
            get
            {
                return PmsVariable.CivilStatus.ToList();
            }
        }

        public IList<Sex> ListSex
        {
            get
            {
                return PmsVariable.Sex.ToList();
            }
        }

        public IList<PersonType> ListPersonType
        {
            get
            {
                return PmsVariable.PersonType.ToList();
            }
        }

        
        #endregion

        #region Public methods
        public void LoadCountryObject(ILookupServiceRepository lookupServiceRepository)
        {
            ICountry country = default(Country);
            if (CountryId != string.Empty)
            {
                IList<ICountry> countries = lookupServiceRepository.GetAllCountry();
                if (countries.Count != 0)
                {
                    country = countries.SingleOrDefault(x => x.Id == CountryId);
                }
            }

            this.CountryObject = country;
        }

        public void LoadList(ILookupServiceRepository lookupServiceRepository)
        {
            this.ListCountry = lookupServiceRepository.GetAllCountry();
        }
        #endregion
    }
}
