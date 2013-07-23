using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel.Channels;
using System.Text;

using AutoMapper;

using Pms.Common.Variable.Enumerations;
using Pms.Entity;
using Pms.Entity.Interface;
using EmsService = Pms.Data.Integration.EmployeeManagementService;
using UmsService = Pms.Data.Integration.UserManagementService;
using CmsService = Pms.Data.Integration.CommonManagementService;

namespace Pms.Data.Integration.Helper
{
    using Pms.Data.Integration.Helper.TranslatorEntity;

    internal class Utility
    {
        public static bool ResolveResponseStatus(string responseStatus)
        {
            if (responseStatus.ToLower() == Enum.GetName(typeof(ServiceResponse), ServiceResponse.Success).ToLower())
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public static bool ResolveCodeMessage(object response, out IList<CodeMessage> messages)
        {
            messages = new List<CodeMessage>();

            Type responseType = response.GetType();
            object codeMessages = responseType.BaseType.GetProperty("codeMessages").GetValue(response, null);
            if (codeMessages == null)
            {
                return true;
            }
            else
            {
                Array codeMessageArray = codeMessages as Array;
                if (codeMessageArray.Length > 0)
                {
                    foreach (var responseItem in codeMessageArray)
                    {
                        string code = responseItem.GetType().GetProperty("code").GetValue(responseItem, null).ToString();

                        if (code == "0")
                        {
                            return true;
                        }
                        else
                        {
                            messages.Add(new CodeMessage() { Id = code, Name = responseItem.GetType().GetProperty("message").GetValue(responseItem, null).ToString() });
                        }
                    }

                    return false;
                }
                else
                {
                    return false;
                }
            }
        }

        public static HttpRequestMessageProperty SetMessageProperty(IList<ServiceHeader> serviceHeader)
        {
            HttpRequestMessageProperty messageProperty = new HttpRequestMessageProperty();

            foreach (ServiceHeader header in serviceHeader)
            {
                messageProperty.Headers.Add(header.Id, header.Name);
            }

            return messageProperty;
        }
    }

    internal static class DataIntegrationMapper
    {
        static DataIntegrationMapper()
        {
            AutoMapper.Mapper.CreateMap<CmsService.countryDTO, CmsService.officeLocationDTO>();
            AutoMapper.Mapper.CreateMap<CmsService.departmentInfoDTO, CmsService.officeLocationDTO>();
            AutoMapper.Mapper.CreateMap<EmsService.GetEmployeeAccountResponse, EmsService.employeeInfoDTO>().ForMember(x => x.accountInfoDTO, y => y.MapFrom(src => src.person));
            AutoMapper.Mapper.CreateMap<EmsService.accountInfoDTO, UmsService.accountInfoDTO>();
            AutoMapper.Mapper.CreateMap<UmsService.GetAccountResponse, UmsService.accountInfoDTO>();
            AutoMapper.Mapper.CreateMap<EmsService.genericResponse, UmsService.genericResponse>();
        }

        public static UmsService.accountInfoDTO ToAccountInfoDto(this EmsService.accountInfoDTO source)
        {
            return Mapper.Map<EmsService.accountInfoDTO, UmsService.accountInfoDTO>(source);
        }

        public static UmsService.accountInfoDTO ToAccountInfoDto(this UmsService.GetAccountResponse source)
        {
            return Mapper.Map<UmsService.GetAccountResponse, UmsService.accountInfoDTO>(source);
        }

        public static EmsService.employeeInfoDTO ToEmployeeDto(this EmsService.GetEmployeeAccountResponse source)
        {
            return Mapper.Map<EmsService.GetEmployeeAccountResponse, EmsService.employeeInfoDTO>(source);
        }

        //public static UmsService.genericResponse ToGenericResponse(this EmsService.genericResponse source)
        //{
        //    return Mapper.Map<EmsService.genericResponse, UmsService.genericResponse>(source);
        //}

        public static IServiceBase ToServiceBase<Entity>(this Entity source) where Entity : class, new ()
        {
            AutoMapper.Mapper.CreateMap<Entity, IServiceBase>();
            return Mapper.Map<Entity, IServiceBase>(source);
        }

        public static IServiceLookup ToServiceLookup<Entity>(this Entity source) where Entity : class, new()
        {
            AutoMapper.Mapper.CreateMap<Entity, IServiceLookup>();
            return Mapper.Map<Entity, IServiceLookup>(source);
        }

        public static UmsService.genericResponse ToGenericResponse<Entity>(this Entity source) where Entity : class, new()
        {
            AutoMapper.Mapper.CreateMap<Entity, UmsService.genericResponse>();
            return Mapper.Map<Entity, UmsService.genericResponse>(source);
        }

    }

    internal class Translator
    {
        public static IPerson PersonToEntity(UmsService.accountInfoDTO person)
        {
            IPerson entityObject = BaseToEntity<Person>(DataIntegrationMapper.ToServiceBase<UmsService.accountInfoDTO>(person));
            entityObject.PersonId = person.id.ToString();
            entityObject.PersonStatus = person.status;
            entityObject.ActivationToken = person.activationToken;
            entityObject.Address = person.address;
            entityObject.Age = person.age.ToString();
            entityObject.AgeSpecified = person.ageSpecified;
            entityObject.Birthday = person.birthDate;
            entityObject.BirthdaySpecified = person.birthDateSpecified;
            entityObject.CivilStatus = person.civilStatus;
            entityObject.CountryId = person.countryId.ToString();
            entityObject.CountryIdSpecified = person.countryIdSpecified;
            entityObject.ExternalEmail = person.externalEmail;
            entityObject.HomeNumber = person.homeNo;
            entityObject.FirstName = person.firstName;
            entityObject.LastName = person.lastName;
            entityObject.MiddleName = person.middleName;
            entityObject.PassportExpiry = person.passportExpiry;
            entityObject.PassportExpirySpecified = person.passportExpirySpecified;
            entityObject.PassportNumber = person.passportNo;
            entityObject.PrimaryNumber = person.primaryNo;
            entityObject.RecordStatus = person.recordStatus;
            entityObject.RecordStatusSpecified = person.recordStatusSpecified;
            entityObject.SecondaryNumber = person.secondaryNo;
            entityObject.Sex = person.sex;
            entityObject.SocialSecurityNumber = person.socialSecNo;
            entityObject.StartDateExpiry = person.startDateExp;
            entityObject.StartDateExpirySpecified = person.startDateExpSpecified;
            entityObject.UpdateReason = person.updateReason;
            entityObject.YearsItExperience = person.yrsItExp;
            entityObject.YearsItExperienceSpecified = person.yrsItExpSpecified;
            return entityObject;
        }

        public static EmsService.accountInfoDTO PersonToService(IPerson person)
        {
            EmsService.accountInfoDTO serviceModelPerson = new EmsService.accountInfoDTO();

            serviceModelPerson.id = int.Parse(person.PersonId);
            serviceModelPerson.activationToken = person.ActivationToken;
            serviceModelPerson.address = person.Address;
            serviceModelPerson.age = int.Parse(person.Age);
            serviceModelPerson.ageSpecified = person.AgeSpecified;

            if (person.Birthday != null && person.Birthday != default(DateTime))
            {
                serviceModelPerson.birthDate = (DateTime)person.Birthday;
                serviceModelPerson.birthDateSpecified = true;
            }

            serviceModelPerson.civilStatus = person.CivilStatus;
            serviceModelPerson.countryId = int.Parse(person.CountryId);
            serviceModelPerson.countryIdSpecified = person.CountryIdSpecified;

            serviceModelPerson.externalEmail = person.ExternalEmail;
            serviceModelPerson.homeNo = person.HomeNumber;
            serviceModelPerson.firstName = person.FirstName;
            serviceModelPerson.lastName = person.LastName;
            serviceModelPerson.middleName = person.MiddleName;

            if (person.PassportExpiry != null && person.PassportExpiry != default(DateTime))
            {
                serviceModelPerson.passportExpiry = (DateTime)person.PassportExpiry;
                serviceModelPerson.passportExpirySpecified = true;
            }

            serviceModelPerson.passportNo = person.PassportNumber;
            serviceModelPerson.primaryNo = person.PrimaryNumber;
            serviceModelPerson.recordStatus = person.RecordStatus;
            serviceModelPerson.recordStatusSpecified = person.RecordStatusSpecified;
            serviceModelPerson.secondaryNo = person.SecondaryNumber;
            serviceModelPerson.sex = person.Sex;
            serviceModelPerson.socialSecNo = person.SocialSecurityNumber;

            if (person.StartDateExpiry != null && person.StartDateExpiry != default(DateTime))
            {
                serviceModelPerson.startDateExp = (DateTime)person.StartDateExpiry;
                serviceModelPerson.startDateExpSpecified = true;
            }

            serviceModelPerson.updateReason = person.UpdateReason;
            serviceModelPerson.yrsItExp = person.YearsItExperience;
            serviceModelPerson.yrsItExpSpecified = person.YearsItExperienceSpecified;

            return serviceModelPerson;
        }

        public static IJob JobToEntity(EmsService.jobInfoDTO job)
        {
            IJob entityObject = BaseToEntity<Job>(DataIntegrationMapper.ToServiceBase<EmsService.jobInfoDTO>(job));
            entityObject.Code = job.code;
            entityObject.CreateBy = job.createBy;
            entityObject.CreateDate = job.createDate;
            entityObject.CreateDateSpecified = job.createDateSpecified;
            entityObject.MaxSalary = job.maxSalary;
            entityObject.MaxSalarySpecified = job.maxSalarySpecified;
            entityObject.MinSalary = job.minSalary;
            entityObject.minSalarySpecified = job.minSalarySpecified;
            entityObject.Status = job.status;
            entityObject.Title = job.title;
            entityObject.UpdateBy = job.updateBy;
            entityObject.UpdateDate = job.updateDate;
            entityObject.UpdateDateSpecified = job.updateDateSpecified;
            entityObject.UpdateReason = job.updateReason;
            return entityObject;
        }

        public static EmsService.jobInfoDTO JobToService(IJob job)
        {
            EmsService.jobInfoDTO serviceModelJob = new EmsService.jobInfoDTO();

            serviceModelJob.code = job.Code;
            serviceModelJob.maxSalary = job.MaxSalary;
            serviceModelJob.maxSalarySpecified = job.MaxSalarySpecified;
            serviceModelJob.minSalary = job.MinSalary;
            serviceModelJob.minSalarySpecified = job.minSalarySpecified;
            serviceModelJob.status = job.Status;
            serviceModelJob.title = job.Title;
            serviceModelJob.createBy = job.CreateBy;

            if (job.CreateDate != null && job.CreateDate != default(DateTime))
            {
                serviceModelJob.createDate = (DateTime)job.CreateDate;
                serviceModelJob.createDateSpecified = true;
            }

            serviceModelJob.updateReason = job.UpdateReason;
            serviceModelJob.updateBy = job.UpdateBy;

            if (job.UpdateDate != null && job.UpdateDate != default(DateTime))
            {
                serviceModelJob.updateDate = (DateTime)job.UpdateDate;
                serviceModelJob.updateDateSpecified = true;
            }

            return serviceModelJob;
        }

        public static ICareerLevel CareerLevelToEntity(EmsService.careerLevelDTO careerLevel)
        {
            ICareerLevel entityObject = BaseToEntity<CareerLevel>(DataIntegrationMapper.ToServiceBase<EmsService.careerLevelDTO>(careerLevel));
            entityObject.CareerLevelId = careerLevel.careerLvl.ToString();
            entityObject.CareerLevelGroupId = careerLevel.careerLvlGrp.ToString();
            entityObject.CareerTitle = careerLevel.careerTitle;
            entityObject.IndustryExperience = careerLevel.indExp;
            entityObject.RoleExperience = careerLevel.roleExp;
            entityObject.Route = careerLevel.route;
            return entityObject;
        }

        public static EmsService.careerLevelDTO CareerLevelToService(ICareerLevel careerLevel)
        {
            return new EmsService.careerLevelDTO()
            {
                careerLvl = int.Parse(careerLevel.CareerLevelId),
                careerLvlGrp = int.Parse(careerLevel.CareerLevelGroupId),
                careerTitle = careerLevel.CareerTitle,
                indExp = careerLevel.IndustryExperience,
                roleExp = careerLevel.RoleExperience,
                route = careerLevel.Route
            };
        }

        public static Entity EmployeeToEntity<Entity>(EmsService.employeeInfoDTO source) where Entity : IEmployee, new ()
        {
            Entity entityObject = BaseToEntity<Entity>(DataIntegrationMapper.ToServiceBase<EmsService.employeeInfoDTO>(source));

            entityObject.FullName = Common.Helper.Utility.GenerateFullName(source.accountInfoDTO.firstName, source.accountInfoDTO.middleName, source.accountInfoDTO.lastName);
            entityObject.FirstName = source.accountInfoDTO.firstName;
            entityObject.MiddleName = source.accountInfoDTO.middleName;
            entityObject.LastName = source.accountInfoDTO.lastName;
            entityObject.ExternalEmail = source.accountInfoDTO.externalEmail;

            entityObject.CareerPathId = source.careerPathId.ToString();
            entityObject.CompetencyGroupId = source.cmptncyGrpId.ToString();
            entityObject.CompetencyId = source.cmptncyId.ToString();
            entityObject.CommisionPct = source.commisionPct.ToString();
            entityObject.CorporateLocationNumber = source.corpLocNo;
            entityObject.Email = source.email;
            entityObject.EmployeeStatus = source.empStatusId.ToString();
            entityObject.EmployeeTypeId = source.empTypeId.ToString();
            entityObject.HireDate = source.hireDate;
            entityObject.EmployeeId = source.id.ToString();
            entityObject.ManagerId = source.managerId.ToString();
            entityObject.Number = source.number;
            entityObject.OfficeLocationId = source.officeLocId.ToString();
            entityObject.ProjectManagerId = source.projMgrId.ToString();
            entityObject.RecordStatus = source.recordStatus.ToString();
            entityObject.Rohq = source.rohq;
            entityObject.Salary = source.salary.ToString();
            entityObject.SeatNumber = source.seatNo;

            if (source.departmentInfoDTO != null)
            {
                entityObject.DepartmentId = source.departmentInfoDTO.deptId.ToString();
                entityObject.DepartmentObject = DepartmentToEntity<Department>(source.departmentInfoDTO);
            }

            return entityObject;
        }

        public static Entity DepartmentToEntity<Entity>(EmsService.departmentInfoDTO source) where Entity : IDepartment, new()
        {
            Entity entityObject = LookupToEntity<Entity>(DataIntegrationMapper.ToServiceLookup<EmsService.departmentInfoDTO>(source));
            entityObject.Id = source.deptId.ToString();
            entityObject.DepartmentId = entityObject.Id;
            entityObject.Name = source.deptName;
            entityObject.LocationId = source.locationId;
            entityObject.ManagerId = source.managerId;
            entityObject.Status = source.status.ToString();

            return entityObject;
        }

        public static Entity LookupToEntity<Entity>(IServiceLookup source) where Entity : ILookup, new()
        {
            Entity lookup = BaseToEntity<Entity>(source);

            try
            {
                lookup.Name = source.name;
            }
            catch (Exception ex)
            {
                //Do nothing, some object might not have name
            }

            try
            {
                lookup.Description = source.description;
            }
            catch (Exception ex)
            {
                //Do nothing, some object might not have description
            }

            return lookup;
        }

        public static Entity BaseToEntity<Entity>(IServiceBase source) where Entity : IBase, new()
        {
            Entity baseObject = new Entity();
            baseObject.CreateBy = source.createBy;
            baseObject.CreateDate = source.createDate;
            baseObject.UpdateBy = source.updateBy;
            baseObject.UpdateDate = source.updateDate;
            baseObject.UpdateReason = source.updateReason;
            return baseObject;
        }
    }
}

namespace Pms.Data.Integration.Helper.TranslatorEntity
{
    public interface IServiceLookup : IServiceBase
    {
        string name { get; set; }
        string description { get; set; }
    }

    public class ServiceBase
    {
        public string createBy { get; set; }
        public DateTime? createDate { get; set; }
        public string updateBy { get; set; }
        public DateTime? updateDate { get; set; }
        public string updateReason { get; set; }
    }

    public interface IServiceBase
    {
        string createBy { get; set; }
        DateTime? createDate { get; set; }
        string updateBy { get; set; }
        DateTime? updateDate { get; set; }
        string updateReason { get; set; }
    }
}
