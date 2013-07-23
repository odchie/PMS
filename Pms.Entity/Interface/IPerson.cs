using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Pms.Entity.Interface
{
    public interface IPerson : ITimeStamp
    {
        string PersonId { get; set; }
        string ActivationToken { get; set; }
        string Address { get; set; }
        string Age { get; set; }
        bool AgeSpecified { get; set; }
        DateTime? Birthday { get; set; }
        bool BirthdaySpecified { get; set; }
        string CivilStatus { get; set; }
        string CountryId { get; set; }
        bool CountryIdSpecified { get; set; }
        string ExternalEmail { get; set; }
        string FullName { get; set; }
        string FirstName { get; set; }
        string LastName { get; set; }
        string MiddleName { get; set; }
        string HomeNumber { get; set; }
        DateTime? PassportExpiry { get; set; }
        bool PassportExpirySpecified { get; set; }
        string PassportNumber { get; set; }
        string PrimaryNumber { get; set; }
        int RecordStatus { get; set; }
        bool RecordStatusSpecified { get; set; }
        string SecondaryNumber { get; set; }
        string Sex { get; set; }
        string SocialSecurityNumber { get; set; }
        DateTime? StartDateExpiry { get; set; }
        bool StartDateExpirySpecified { get; set; }
        int YearsItExperience { get; set; }
        bool YearsItExperienceSpecified { get; set; }
        string PersonType { get; set; }
        string PersonStatus { get; set; }

        //Fieds that are not part of Person table
        string Reason { get; set; }
        string StatusChangeBy  { get; set; }
        string StatusChangeReason  { get; set; }
        DateTime? StatusChangeTime  { get; set; }
        bool StatusChangeTimeSpecified  { get; set; }
    }
}

