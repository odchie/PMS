using System;
using System.ServiceModel;
using System.ServiceModel.Channels;
using System.Configuration;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Pms.Entity;
using Pms.Entity.Interface;
using Pms.Common.Variable;
using Pms.Data.Integration.Helper;
using CmsService = Pms.Data.Integration.CommonManagementService;
using EmsService = Pms.Data.Integration.EmployeeManagementService;
using SmsService = Pms.Data.Integration.SkillManagementService;
using UmsService = Pms.Data.Integration.UserManagementService;

namespace Pms.Data.Integration.ServiceIntegration
{
    public class LookupIntegration
    {
        #region Variable
        protected EmsService.EmployeeManagementServiceEndpointClient _emsEndPointClient;
        protected UmsService.UserManagementServiceEndpointClient _umsEndPointClient;
        protected CmsService.CommonManagementServiceEndpointClient _cmsEndPointClient;
        protected SmsService.SkillManagementServiceEndpointClient _smsEndPointClient;
        #endregion

        #region Constructor
        public LookupIntegration()
        {
            string username = ConfigurationSettings.AppSettings[ConfigurationKey.WebServiceUsername];
            string password = ConfigurationSettings.AppSettings[ConfigurationKey.WebServicePassword];

            _cmsEndPointClient = new CmsService.CommonManagementServiceEndpointClient();
            _cmsEndPointClient.ClientCredentials.UserName.UserName = username;
            _cmsEndPointClient.ClientCredentials.UserName.Password = password;

            _emsEndPointClient = new EmsService.EmployeeManagementServiceEndpointClient();
            _emsEndPointClient.ClientCredentials.UserName.UserName = username;
            _emsEndPointClient.ClientCredentials.UserName.Password = password;

            _smsEndPointClient = new SmsService.SkillManagementServiceEndpointClient();
            _smsEndPointClient.ClientCredentials.UserName.UserName = username;
            _smsEndPointClient.ClientCredentials.UserName.Password = password;

            _umsEndPointClient = new UmsService.UserManagementServiceEndpointClient();
            _umsEndPointClient.ClientCredentials.UserName.UserName = username;
            _umsEndPointClient.ClientCredentials.UserName.Password = password;
        }
        #endregion

        #region Public property
        public IList<ServiceHeader> ServiceHeaders { get; set; }
        #endregion

        #region public method
        public IList<ICountry> GetAllCountry()
        {
            using (new OperationContextScope((IContextChannel)_cmsEndPointClient.InnerChannel))
            {
                if (ServiceHeaders != null)
                {
                    OperationContext.Current.OutgoingMessageProperties[HttpRequestMessageProperty.Name] = Utility.SetMessageProperty(ServiceHeaders);
                }
                CmsService.GetAllCountriesResponse response = _cmsEndPointClient.getAllCountries(new CmsService.getAllCountriesRequest());

                IList<ICountry> listReturn = new List<ICountry>();
                foreach (CmsService.countryDTO item in response.countries)
                {
                    ICountry itemToTransform = Translator.LookupToEntity<Country>(DataIntegrationMapper.ToServiceLookup<CmsService.countryDTO>(item));
                    itemToTransform.Id = item.id.ToString();
                    itemToTransform.RegionId = item.regionId.ToString();
                    listReturn.Add(itemToTransform);
                }

                return listReturn;
            }
        }

        public IList<IDepartment> GetAllDepartment()
        {
            using (new OperationContextScope((IContextChannel)_cmsEndPointClient.InnerChannel))
            {
                if (ServiceHeaders != null)
                {
                    OperationContext.Current.OutgoingMessageProperties[HttpRequestMessageProperty.Name] = Utility.SetMessageProperty(ServiceHeaders);
                }
                CmsService.RetrieveAllDepartmentResponse response = _cmsEndPointClient.getAllDepartments(new CmsService.getAllDepartmentsRequest());

                IList<IDepartment> listReturn = new List<IDepartment>();
                foreach (CmsService.departmentInfoDTO item in response.departmentList)
                {
                    IDepartment itemToTransform = Translator.BaseToEntity<Department>(DataIntegrationMapper.ToServiceBase<CmsService.departmentInfoDTO>(item));
                    //IDepartment department = Translator.LookupToEntity<Department>(DataIntegrationMapper.ToOfficeLocationDto(item));
                    itemToTransform.Status = item.status.ToString();
                    itemToTransform.LocationId = item.locationId.ToString();
                    itemToTransform.ManagerId = item.managerId.ToString();
                    listReturn.Add(itemToTransform);
                }

                return listReturn;
            }
        }

        public IList<IJob> GetAllJob()
        {
            using (new OperationContextScope((IContextChannel)_cmsEndPointClient.InnerChannel))
            {
                if (ServiceHeaders != null)
                {
                    OperationContext.Current.OutgoingMessageProperties[HttpRequestMessageProperty.Name] = Utility.SetMessageProperty(ServiceHeaders);
                }
                CmsService.RetrieveAllJobResponse response = _cmsEndPointClient.getAllJobs(new CmsService.getAllJobsRequest());

                IList<IJob> listReturn = new List<IJob>();
                foreach (CmsService.jobInfoDTO item in response.jobList)
                {
                    listReturn.Add(new Job()
                    {
                        JobId = item.id.ToString(),
                        Code = item.code,
                        MaxSalary = item.maxSalary,
                        MinSalary = item.minSalary,
                        Status = item.status,

                        CreateBy = item.createBy,
                        CreateDate = item.createDate,
                        UpdateBy = item.updateBy,
                        UpdateDate = item.updateDate,
                        UpdateReason = item.updateReason
                    });
                }

                return listReturn;
            }
        }

        public IList<IOfficeLocation> GetAllOfficeLocation()
        {
            using (new OperationContextScope((IContextChannel)_cmsEndPointClient.InnerChannel))
            {
                if (ServiceHeaders != null)
                {
                    OperationContext.Current.OutgoingMessageProperties[HttpRequestMessageProperty.Name] = Utility.SetMessageProperty(ServiceHeaders);
                }
                CmsService.GetAllOfficeLocationsResponse response = _cmsEndPointClient.getAllOfficeLocations(new CmsService.getAllOfficeLocationsRequest());

                IList<IOfficeLocation> listReturn = new List<IOfficeLocation>();
                foreach (CmsService.officeLocationDTO item in response.officeLocations)
                {
                    IOfficeLocation itemToTransform = Translator.LookupToEntity<OfficeLocation>(DataIntegrationMapper.ToServiceLookup<CmsService.officeLocationDTO>(item));
                    itemToTransform.status = item.status.ToString();
                    listReturn.Add(itemToTransform);
                }

                return listReturn;
            }
        }

        public IList<ICareerLevel> GetAllCareerLevel()
        {
            using (new OperationContextScope((IContextChannel)_emsEndPointClient.InnerChannel))
            {
                if (ServiceHeaders != null)
                {
                    OperationContext.Current.OutgoingMessageProperties[HttpRequestMessageProperty.Name] = Utility.SetMessageProperty(ServiceHeaders);
                }
                EmsService.SearchAccountResponse response = _emsEndPointClient.getAllCareerLevels(new EmsService.getAllCareerLevelsRequest());

                IList<ICareerLevel> listReturn = new List<ICareerLevel>();
                foreach (EmsService.careerLevelDTO item in response.careerLevels)
                {
                    ICareerLevel itemToTransform = Translator.BaseToEntity<CareerLevel>(DataIntegrationMapper.ToServiceBase<EmsService.careerLevelDTO>(item));

                    itemToTransform.CareerLevelId = item.id.ToString();
                    itemToTransform.CareerLevelGroupId = item.careerLvlGrp.ToString();
                    itemToTransform.CareerTitle = item.careerTitle;
                    itemToTransform.IndustryExperience = item.indExp;
                    itemToTransform.RoleExperience = item.roleExp;
                    itemToTransform.Route = item.route;
                    listReturn.Add(itemToTransform);
                }

                return listReturn;
            }
        }

        public IList<ICompetency> GetAllCompetency(out IList<CodeMessage> messages)
        {
            using (new OperationContextScope((IContextChannel)_smsEndPointClient.InnerChannel))
            {
                if (ServiceHeaders != null)
                {
                    OperationContext.Current.OutgoingMessageProperties[HttpRequestMessageProperty.Name] = Utility.SetMessageProperty(ServiceHeaders);
                }

                SmsService.GetAllCompetenciesResponse response = _smsEndPointClient.getAllCompetencies(new SmsService.getAllCompetenciesRequest());

                IList<ICompetency> listReturn = new List<ICompetency>();
                if (Utility.ResolveCodeMessage(DataIntegrationMapper.ToGenericResponse(response), out messages))
                {
                    foreach (SmsService.competencyDto competency in response.comptencies)
                    {
                        ICompetency competencyEntity = Helper.Translator.BaseToEntity<Competency>(DataIntegrationMapper.ToServiceLookup<SmsService.competencyDto>(competency));

                        competencyEntity.CompetencyId = competency.comeptencyId.ToString();
                        competencyEntity.Description = competency.comeptencyDescription;
                        competencyEntity.Name = competency.comeptencyName;
                        competencyEntity.ManagerId = competency.competencyManagerId.ToString();
                        competencyEntity.ManagerIdSpecified = competency.competencyManagerIdSpecified;
                        competencyEntity.RecordStatusId = competency.recordStatusId.ToString();
                        competencyEntity.RecordStatusIdSpecified = competency.recordStatusIdSpecified;
                        competencyEntity.Description = competency.comeptencyDescription;

                        listReturn.Add(competencyEntity);
                    }
                }

                return listReturn;
            }
        }

        public IList<ICompetencyManager> GetAllCompetencyManager()
        {
            using (new OperationContextScope((IContextChannel)_emsEndPointClient.InnerChannel))
            {
                if (ServiceHeaders != null)
                {
                    OperationContext.Current.OutgoingMessageProperties[HttpRequestMessageProperty.Name] = Utility.SetMessageProperty(ServiceHeaders);
                }

                EmsService.GetAllCompetencyManagerResponse response = _emsEndPointClient.getAllCompetencyManager(new EmsService.getAllCompetencyManagerRequest());

                IList<ICompetencyManager> listReturn = new List<ICompetencyManager>();
                foreach (EmsService.employeeInfoDTO item in response.list)
                {
                    ICompetencyManager manager = Translator.EmployeeToEntity<CompetencyManager>(item);

                    if (item.accountInfoDTO != null)
                    {
                        manager.PersonObject = Translator.PersonToEntity(DataIntegrationMapper.ToAccountInfoDto(item.accountInfoDTO));
                    }

                    if (item.careerLevel != null)
                    {
                        manager.CareerLevelObject = Translator.CareerLevelToEntity(item.careerLevel);
                    }
                    
                    if (item.job != null)
                    {
                        manager.JobObject = Translator.JobToEntity(item.job);
                    }

                    listReturn.Add(manager);
                }

                return listReturn;
            }
        }

        public IList<IDepartmentManager> GetAllDepartmentManager()
        {
            using (new OperationContextScope((IContextChannel)_emsEndPointClient.InnerChannel))
            {
                if (ServiceHeaders != null)
                {
                    OperationContext.Current.OutgoingMessageProperties[HttpRequestMessageProperty.Name] = Utility.SetMessageProperty(ServiceHeaders);
                }
                EmsService.GetAllDepartmentManagerResponse response = _emsEndPointClient.getAllDepartmentManager(new EmsService.getAllDepartmentManagerRequest());

                IList<IDepartmentManager> listReturn = new List<IDepartmentManager>();
                foreach (EmsService.employeeInfoDTO item in response.list)
                {
                    IDepartmentManager manager = Translator.EmployeeToEntity<DepartmentManager>(item);

                    if (item.accountInfoDTO != null)
                    {
                        manager.PersonObject = Translator.PersonToEntity(DataIntegrationMapper.ToAccountInfoDto(item.accountInfoDTO));
                    }

                    listReturn.Add(manager);
                }

                return listReturn;
            }
        }

        public IList<IManager> GetAllManager()
        {
            using (new OperationContextScope((IContextChannel)_emsEndPointClient.InnerChannel))
            {
                if (ServiceHeaders != null)
                {
                    OperationContext.Current.OutgoingMessageProperties[HttpRequestMessageProperty.Name] = Utility.SetMessageProperty(ServiceHeaders);
                }
                EmsService.GetAllManagersResponse response = _emsEndPointClient.getAllManagers(new EmsService.getAllManagersRequest());

                IList<IManager> listReturn = new List<IManager>();

                if (response.list != null)
                {
                    foreach (EmsService.employeeInfoDTO item in response.list)
                    {
                        IManager manager = Translator.EmployeeToEntity<Manager>(item);
                        if (item.accountInfoDTO != null)
                        {
                            manager.PersonObject = Translator.PersonToEntity(DataIntegrationMapper.ToAccountInfoDto(item.accountInfoDTO));
                        }
                        listReturn.Add(manager);
                    }
                }

                return listReturn;
            }
        }

        public IList<IStaffManager> GetAllStaffManager()
        {
            using (new OperationContextScope((IContextChannel)_emsEndPointClient.InnerChannel))
            {
                if (ServiceHeaders != null)
                {
                    OperationContext.Current.OutgoingMessageProperties[HttpRequestMessageProperty.Name] = Utility.SetMessageProperty(ServiceHeaders);
                }

                EmsService.GetAllStaffManagersResponse response = _emsEndPointClient.getAllStaffManagers(new EmsService.getAllStaffManagersRequest());

                IList<IStaffManager> listReturn = new List<IStaffManager>();
                foreach (EmsService.employeeInfoDTO item in response.list)
                {
                    IStaffManager manager = Translator.EmployeeToEntity<StaffManager>(item);

                    if (item.accountInfoDTO != null)
                    {
                        manager.PersonObject = Translator.PersonToEntity(DataIntegrationMapper.ToAccountInfoDto(item.accountInfoDTO));
                    }

                    listReturn.Add(manager);
                }

                return listReturn;
            }
        }
        #endregion

    }
}
