using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Pms.Entity.Interface;

namespace Pms.Entity
{
    public class Person : TimeStamp, IPerson
    {
        public string PersonId { get; set; }
        public string ActivationToken { get; set; }
        public string Address { get; set; }
        public string Age { get; set; }
        public bool AgeSpecified { get; set; }
        public DateTime? Birthday { get; set; }
        public bool BirthdaySpecified { get; set; }
        public string CivilStatus { get; set; }
        public string CountryId { get; set; }
        public bool CountryIdSpecified { get; set; }
        public string ExternalEmail { get; set; }
        public string FullName { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string MiddleName { get; set; }
        public string HomeNumber { get; set; }
        public DateTime? PassportExpiry { get; set; }
        public bool PassportExpirySpecified { get; set; }
        public string PassportNumber { get; set; }
        public string PrimaryNumber { get; set; }
        public int RecordStatus { get; set; }
        public bool RecordStatusSpecified { get; set; }
        public string SecondaryNumber { get; set; }
        public string Sex { get; set; }
        public string SocialSecurityNumber { get; set; }
        public DateTime? StartDateExpiry { get; set; }
        public bool StartDateExpirySpecified { get; set; }
        public int YearsItExperience { get; set; }
        public bool YearsItExperienceSpecified { get; set; }
        public string PersonType { get; set; }
        public string PersonStatus { get; set; }

        //Fieds that are not part of Person table
        public string Reason { get; set; }
        public string StatusChangeBy { get; set; }
        public string StatusChangeReason { get; set; }
        public DateTime? StatusChangeTime { get; set; }
        public bool StatusChangeTimeSpecified { get; set; }
    }
}
