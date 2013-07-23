using System;
using System.ServiceModel;
using System.ServiceModel.Channels;
using System.Configuration;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Pms.Common.Variable;
using Pms.Data.Integration.Helper;
using Pms.Entity;
using Pms.Entity.Interface;
using UmsService = Pms.Data.Integration.UserManagementService;
using EmsService = Pms.Data.Integration.EmployeeManagementService;
using SmsService = Pms.Data.Integration.SkillManagementService;

namespace Pms.Data.Integration.ServiceIntegration
{
    public class UserManagementIntegration
    {
        #region Variable
        protected UmsService.UserManagementServiceEndpointClient _umsEndPointClient;
        protected EmsService.EmployeeManagementServiceEndpointClient _emsEndPointClient;
        protected SmsService.SkillManagementServiceEndpointClient _smsEndPointClient;
        #endregion

        #region Constructor
        public UserManagementIntegration()
        {
            string username = ConfigurationSettings.AppSettings[ConfigurationKey.WebServiceUsername];
            string password = ConfigurationSettings.AppSettings[ConfigurationKey.WebServicePassword];

            _umsEndPointClient = new UmsService.UserManagementServiceEndpointClient();
            _umsEndPointClient.ClientCredentials.UserName.UserName = username;
            _umsEndPointClient.ClientCredentials.UserName.Password = password;

            _emsEndPointClient = new EmsService.EmployeeManagementServiceEndpointClient();
            _emsEndPointClient.ClientCredentials.UserName.UserName = username;
            _emsEndPointClient.ClientCredentials.UserName.Password = password;

            _smsEndPointClient = new SmsService.SkillManagementServiceEndpointClient();
            _smsEndPointClient.ClientCredentials.UserName.UserName = username;
            _smsEndPointClient.ClientCredentials.UserName.Password = password;

            System.Net.ServicePointManager.Expect100Continue = false;
        }
        #endregion

        #region Public property
        public IList<ServiceHeader> ServiceHeaders { get; set; }
        #endregion

        #region Public method
        #region User management WSDL
        public IList<IPerson> SearchAccount(string accountId, string email, string firstName, string lastName)
        {
            using (new OperationContextScope((IContextChannel)_umsEndPointClient.InnerChannel))
            {
                if (ServiceHeaders != null)
                {
                    OperationContext.Current.OutgoingMessageProperties[HttpRequestMessageProperty.Name] = Utility.SetMessageProperty(ServiceHeaders);
                }

                UmsService.searchAccountRequest request = new UmsService.searchAccountRequest();

                if (string.IsNullOrEmpty(accountId))
                {
                    request.accountId = long.Parse(accountId);
                }

                request.email = email;
                request.firstName = firstName;
                request.lastName = lastName;
                
                UmsService.SearchAccountResponse response = _umsEndPointClient.searchAccount(request);

                IList<IPerson> accountsFound = new List<IPerson>();

                foreach (UmsService.accountInfoDTO user in response.userAccountList)
                {
                    accountsFound.Add(new Person()
                    {
                        PersonId = user.id.ToString(),
                        FirstName = user.firstName,
                        LastName = user.lastName
                    });
                }

                return accountsFound;
            }
        }

        public IPerson GetAccount(string key, string value)
        {
            using (new OperationContextScope((IContextChannel)_umsEndPointClient.InnerChannel))
            {
                if (ServiceHeaders != null)
                {
                    OperationContext.Current.OutgoingMessageProperties[HttpRequestMessageProperty.Name] = Utility.SetMessageProperty(ServiceHeaders);
                }

                UmsService.GetAccountResponse response = _umsEndPointClient.getAccount(new UmsService.getAccountRequest() { key = key, value = value });
                IPerson person = Translator.PersonToEntity(DataIntegrationMapper.ToAccountInfoDto(response));
                //Helper.Translator.PersonToEntity(
                //Person Person = new Person()
                //{
                //    FullName = Common.Helper.Utility.GenerateFullName(response.firstName, response.middleName, response.lastName),
                //    PersonId = response.id,
                //    ActivationToken = response.activationToken,
                //    Age = response.age,
                //    Birthday = response.birthday,
                //    BirthdaySpecified = response.birthdaySpecified,
                //    CountryId = response.country,
                //    CreateDate = response.createDate,
                //    CreateDateSpecified = response.createDateSpecified,
                //    ExternalEmail = response.email,
                //    FirstName = response.firstName,
                //    LastName = response.lastName,
                //    MiddleName = response.middleName,
                //    Reason = response.reason,
                //    Sex = response.sex,
                //    SocialSecurityNumber = response.socialSecNumber,
                //    StatusChangeBy = response.statusChangeBy,
                //    StatusChangeReason = response.statusChangeReason,
                //    StatusChangeTime = response.statusChangeTime,
                //    StatusChangeTimeSpecified = response.statusChangeTimeSpecified,
                //    UpdateDate = response.updateDate,
                //    UpdateDateSpecified = response.updateDateSpecified,
                //};

                return person;
            }
        }

        public bool SaveAccount(IPerson Person, out IList<CodeMessage> messages)
        {
            using (new OperationContextScope((IContextChannel)_umsEndPointClient.InnerChannel))
            {
                if (ServiceHeaders != null)
                {
                    OperationContext.Current.OutgoingMessageProperties[HttpRequestMessageProperty.Name] = Utility.SetMessageProperty(ServiceHeaders);
                }

                UmsService.saveAccountRequest request = new UmsService.saveAccountRequest();
                request.activationToken = Person.ActivationToken;
                request.age = Person.Age;

                request.birthdaySpecified = Person.Birthday != default(DateTime);
                if (request.birthdaySpecified)
                {
                    request.birthday = (DateTime)Person.Birthday;
                    request.birthdaySpecified = Person.BirthdaySpecified;
                }

                request.countryId = Person.CountryId;
                request.email = Person.ExternalEmail;
                request.firstName = Person.FirstName;
                request.lastName = Person.LastName;
                request.middleName = Person.MiddleName;
                request.reason = Person.Reason;
                request.sex = Person.Sex;
                request.socialSecNumber = Person.SocialSecurityNumber;
                request.passportNo = Person.PassportNumber;
                request.passportExpiry = Person.PassportExpiry.ToString();
                request.primaryNo = Person.PrimaryNumber;
                request.secondaryNo = Person.SecondaryNumber;

                return Utility.ResolveCodeMessage(_umsEndPointClient.saveAccount(request), out messages);
            }
        }

        public bool UpdateAccount(IPerson Person, out IList<CodeMessage> messages)
        {
            using (new OperationContextScope((IContextChannel)_umsEndPointClient.InnerChannel))
            {
                if (ServiceHeaders != null)
                {
                    OperationContext.Current.OutgoingMessageProperties[HttpRequestMessageProperty.Name] = Utility.SetMessageProperty(ServiceHeaders);
                }

                UmsService.updateAccountRequest request = new UmsService.updateAccountRequest();
                request.activationToken = Person.ActivationToken;
                request.age = Person.Age;

                request.birthdaySpecified = Person.Birthday != default(DateTime);
                if (request.birthdaySpecified)
                {
                    request.birthday = (DateTime)Person.Birthday;
                    request.birthdaySpecified = Person.BirthdaySpecified;
                }

                request.civilStatus = Person.CivilStatus;
                request.countryId = Person.CountryId;
                request.email = Person.ExternalEmail;
                request.firstName = Person.FirstName;
                request.lastName = Person.LastName;
                request.middleName = Person.MiddleName;
                request.reason = Person.Reason;
                request.sex = Person.Sex;
                request.socialSecNumber = Person.SocialSecurityNumber;

                return Utility.ResolveCodeMessage(_umsEndPointClient.updateAccount(request), out messages);
            }
        }

        public bool DeleteAccount(string id, string statusChangeReason)
        {
            using (new OperationContextScope((IContextChannel)_umsEndPointClient.InnerChannel))
            {
                if (ServiceHeaders != null)
                {
                    OperationContext.Current.OutgoingMessageProperties[HttpRequestMessageProperty.Name] = Utility.SetMessageProperty(ServiceHeaders);
                }

                UmsService.deleteAccountRequest request = new UmsService.deleteAccountRequest();
                request.deleteAccountId = id;
                request.statusChangeReason = statusChangeReason;

                UmsService.DeleteAccountResponse response = _umsEndPointClient.deleteAccount(request);

                bool isDeleted = response.isDeleted;

                return isDeleted;
            }
        }

        public bool ActivateAccount(string accountId)
        {
            using (new OperationContextScope((IContextChannel)_umsEndPointClient.InnerChannel))
            {
                if (ServiceHeaders != null)
                {
                    OperationContext.Current.OutgoingMessageProperties[HttpRequestMessageProperty.Name] = Utility.SetMessageProperty(ServiceHeaders);
                }
            }
            UmsService.ActivateAccountResponse response = _umsEndPointClient.activateAccount(new UmsService.activateAccountRequest() { activateAccountId = accountId });

            return Utility.ResolveResponseStatus(response.result);
        }

        public bool ActivateByEmail(string activateByEmailId, string activationToken, string email, string userName, out IList<CodeMessage> messages)
        {
            using (new OperationContextScope((IContextChannel)_umsEndPointClient.InnerChannel))
            {
                if (ServiceHeaders != null)
                {
                    OperationContext.Current.OutgoingMessageProperties[HttpRequestMessageProperty.Name] = Utility.SetMessageProperty(ServiceHeaders);
                }
            }

            return Utility.ResolveCodeMessage(_umsEndPointClient.activateByEmail(new UmsService.activateByEmailRequest() { activateByEmailId = activateByEmailId, activationToken = activationToken, email = email, userName = userName }), out messages);
        }

        public bool BlockAccount(string blockAccountId, string status, string statusChangeReason, out IList<CodeMessage> messages)
        {
            using (new OperationContextScope((IContextChannel)_umsEndPointClient.InnerChannel))
            {
                if (ServiceHeaders != null)
                {
                    OperationContext.Current.OutgoingMessageProperties[HttpRequestMessageProperty.Name] = Utility.SetMessageProperty(ServiceHeaders);
                }
            }

            UmsService.BlockAccountResponse response = _umsEndPointClient.blockAccount(new UmsService.blockAccountRequest() { blockAccountId = blockAccountId, statusChangeReason = statusChangeReason });
            Utility.ResolveCodeMessage(response, out messages);
            return response.isBlocked;
        }

        public bool LockAccount(string lockAccountId, string email, string status, string useName, string statusChangeReason)
        {
            using (new OperationContextScope((IContextChannel)_umsEndPointClient.InnerChannel))
            {
                if (ServiceHeaders != null)
                {
                    OperationContext.Current.OutgoingMessageProperties[HttpRequestMessageProperty.Name] = Utility.SetMessageProperty(ServiceHeaders);
                }
            }

            UmsService.LockAccountResponse response = _umsEndPointClient.lockAccount(new UmsService.lockAccountRequest() { email = email, lockAccountId = lockAccountId, status = status, statusChangeReason = statusChangeReason });
            return Utility.ResolveResponseStatus(response.result);
        }

        public bool GetAccountState(string getAccountId)
        {
            using (new OperationContextScope((IContextChannel)_umsEndPointClient.InnerChannel))
            {
                if (ServiceHeaders != null)
                {
                    OperationContext.Current.OutgoingMessageProperties[HttpRequestMessageProperty.Name] = Utility.SetMessageProperty(ServiceHeaders);
                }
            }

            UmsService.GetAccountStateResponse response = _umsEndPointClient.getAccountState(new UmsService.getAccountStateRequest() { getAccountId = getAccountId });
            return Utility.ResolveResponseStatus(response.state);
        }
        #endregion

        #region Employee management WSDL
        public IEmployee GetEmployeeAccount(string key, string value, out IList<CodeMessage> messages)
        {
            using (new OperationContextScope((IContextChannel)_emsEndPointClient.InnerChannel))
            {
                if (ServiceHeaders != null)
                {
                    OperationContext.Current.OutgoingMessageProperties[HttpRequestMessageProperty.Name] = Utility.SetMessageProperty(ServiceHeaders);
                }

                EmsService.GetEmployeeAccountResponse response = _emsEndPointClient.getEmployeeAccount(new EmsService.getEmployeeAccountRequest() { key = key, value = value });

                if (Helper.Utility.ResolveCodeMessage(response, out messages))
                {
                    IEmployee employee = Translator.EmployeeToEntity<Employee>(DataIntegrationMapper.ToEmployeeDto(response));

                    if (response.person != null)
                    {
                        employee.FirstName = response.person.firstName;
                        employee.MiddleName = response.person.middleName;
                        employee.LastName = response.person.lastName;
                        employee.PersonId = response.person.id.ToString();
                        employee.PersonObject = Translator.PersonToEntity(DataIntegrationMapper.ToAccountInfoDto(response.person));
                    }

                    if (response.careerlevel != null)
                    {
                        employee.CareerLevelId = response.careerlevel.id.ToString();
                        employee.CareerLevelObject = Translator.CareerLevelToEntity(response.careerlevel);
                    }

                    if (response.job != null)
                    {
                        employee.JobId = response.job.id.ToString();
                        employee.JobObject = Translator.JobToEntity(response.job);
                    }

                    return employee;
                }
                else
                {
                    return null;
                }
            }
        }

        public bool SaveEmployee(IEmployee emloyee, out IList<CodeMessage> messages)
        {
            using (new OperationContextScope((IContextChannel)_emsEndPointClient.InnerChannel))
            {
                if (ServiceHeaders != null)
                {
                    OperationContext.Current.OutgoingMessageProperties[HttpRequestMessageProperty.Name] = Utility.SetMessageProperty(ServiceHeaders);
                }
            }

            EmsService.saveEmployeeAccountRequest request = new EmsService.saveEmployeeAccountRequest();

            request.careerLevelId = emloyee.CareerLevelId;
            request.cmptncyGrpId = emloyee.CompetencyGroupId;
            request.cmptncyId = emloyee.CompetencyId;
            request.commissionPct = emloyee.CommisionPct;
            request.departmentId = long.Parse(emloyee.DepartmentId);
            request.email = emloyee.Email;
            request.empTypeId = emloyee.EmployeeTypeId;
            request.managerId = emloyee.ManagerId;
            request.phoneNumber = emloyee.CompetencyGroupId;
            request.salary = emloyee.Salary;

            if (emloyee.PersonObject != null)
            {
                IPerson person = emloyee.PersonObject;
                request.acctType = person.PersonType;
                request.countryId = person.CountryId;
                request.firstName = person.FirstName;
                request.lastName = person.LastName;
                request.sex = person.Sex;
                request.smsNumber = person.PrimaryNumber;
                request.socialSecNo = person.SocialSecurityNumber;
            }

            return Utility.ResolveCodeMessage(DataIntegrationMapper.ToGenericResponse(_emsEndPointClient.saveEmployeeAccount(request)), out messages);
        }

        public bool UpdateEmployee(IEmployee emloyee, out IList<CodeMessage> messages)
        {
            using (new OperationContextScope((IContextChannel)_emsEndPointClient.InnerChannel))
            {
                if (ServiceHeaders != null)
                {
                    OperationContext.Current.OutgoingMessageProperties[HttpRequestMessageProperty.Name] = Utility.SetMessageProperty(ServiceHeaders);
                }
            }

            EmsService.updateEmployeeAccountRequest request = new EmsService.updateEmployeeAccountRequest();

            request.careerLevelId = emloyee.CareerLevelId;
            request.cmptncyGrpId = emloyee.CompetencyGroupId;
            request.cmptncyId = emloyee.CompetencyId;
            request.commissionPct = emloyee.CommisionPct;
            request.departmentId = long.Parse(emloyee.DepartmentId);
            request.email = emloyee.Email;
            request.empTypeId = emloyee.EmployeeTypeId;
            request.managerId = emloyee.ManagerId;
            request.phoneNumber = emloyee.CompetencyGroupId;
            request.salary = emloyee.Salary;

            if (emloyee.PersonObject != null)
            {
                IPerson person = emloyee.PersonObject;
                request.acctType = person.PersonType;
                request.countryId = person.CountryId;
                request.firstName = person.FirstName;
                request.lastName = person.LastName;
                request.sex = person.Sex;
                request.smsNumber = person.PrimaryNumber;
                request.socialSecNo = person.SocialSecurityNumber;
            }

            return Utility.ResolveCodeMessage(DataIntegrationMapper.ToGenericResponse(_emsEndPointClient.updateEmployeeAccount(request)), out messages);
        }
        #endregion

        #region Skill management WSDL
        public IList<IScoreCard> GetScoreCard(string employeeId, string competencyId, out IList<CodeMessage> messages)
        {
            using (new OperationContextScope((IContextChannel)_smsEndPointClient.InnerChannel))
            {
                if (ServiceHeaders != null)
                {
                    OperationContext.Current.OutgoingMessageProperties[HttpRequestMessageProperty.Name] = Utility.SetMessageProperty(ServiceHeaders);
                }
            }

            SmsService.GetScoreCardResponse response = _smsEndPointClient.getScoreCard(new SmsService.getScoreCardRequest() { employeeId = long.Parse(employeeId), competencyId = long.Parse(competencyId) });

            IList<IScoreCard> listReturn = new List<IScoreCard>();

            if (Utility.ResolveCodeMessage(DataIntegrationMapper.ToGenericResponse(response), out messages))
            { 
                foreach (SmsService.scoreCardViewDto scoreCard in response.scoreCards)
                {
                    IScoreCard scoreCardEntity = Helper.Translator.BaseToEntity<ScoreCard>(DataIntegrationMapper.ToServiceLookup<SmsService.scoreCardViewDto>(scoreCard));
                    scoreCardEntity.CompetencyGroupId = scoreCard.competencyGroupId.ToString();
                    scoreCardEntity.CompetencyGroupIdSpecified = scoreCard.competencyGroupIdSpecified;
                    scoreCardEntity.CompetencyId = scoreCard.competencyId.ToString();
                    scoreCardEntity.CompetencyIdSpecified = scoreCard.competencyIdSpecified;
                    scoreCardEntity.EmployeeId = scoreCard.employeeId.ToString();
                    scoreCardEntity.EmployeeIdSpecified = scoreCard.employeeIdSpecified;
                    scoreCardEntity.ScoreLevel = scoreCard.scoreLevel.ToString();
                    scoreCardEntity.ScoreLevelSpecified = scoreCard.scoreLevelSpecified;
                    scoreCardEntity.TechnologyKnowledgeGroupId = scoreCard.technologyKnowledgeGroupId.ToString();
                    scoreCardEntity.TechnologyKnowledgeGroupIdSpecified = scoreCard.technologyKnowledgeDetailIdSpecified;
                    scoreCardEntity.TechnologyKnowlegeDetailId = scoreCard.technologyKnowledgeDetailId.ToString();
                    scoreCardEntity.TechnologyKnowlegeDetailIdSpecified = scoreCard.technologyKnowledgeDetailIdSpecified;
                    scoreCardEntity.Year = scoreCard.year.ToString();
                    scoreCardEntity.YearSpecified = scoreCard.yearSpecified;

                    if (scoreCard.personIdSpecified)
                    {
                        scoreCardEntity.PersonId = scoreCard.personId.ToString();
                        scoreCardEntity.PersonObject = new Person() { 
                            PersonId = scoreCard.personId.ToString(),
                            FirstName = scoreCard.firstName,
                            LastName = scoreCard.lastName
                        };
                    }

                    listReturn.Add(scoreCardEntity);
                }
            }

            return listReturn;
        }

        public bool SaveScoreCard(IList<IScoreCard> scoreCards, out IList<CodeMessage> messages)
        {
            using (new OperationContextScope((IContextChannel)_smsEndPointClient.InnerChannel))
            {
                if (ServiceHeaders != null)
                {
                    OperationContext.Current.OutgoingMessageProperties[HttpRequestMessageProperty.Name] = Utility.SetMessageProperty(ServiceHeaders);
                }
            }

            SmsService.saveScoreCardRequest request = new SmsService.saveScoreCardRequest();
            
            for (int counter = 0; counter <= scoreCards.Count; counter++)
            {
                SmsService.scoreCardDto requestItem = new SmsService.scoreCardDto();

                requestItem.updateReason = scoreCards[counter].CompetencyGroupId;

                long competencyGroupId = 0;
                if (long.TryParse(scoreCards[counter].CompetencyGroupId, out competencyGroupId))
                {
                    requestItem.competencyGroupId = competencyGroupId;
                    requestItem.competencyGroupIdSpecified = true;
                }

                long competencyId = 0;
                if (long.TryParse(scoreCards[counter].CompetencyId, out competencyId))
                {
                    requestItem.competencyId = competencyId;
                    requestItem.competencyIdSpecified = true;
                }

                long employeeId = 0;
                if (long.TryParse(scoreCards[counter].EmployeeId, out employeeId))
                {
                    requestItem.employeeId = employeeId;
                    requestItem.employeeIdSpecified = true;
                }

                long personId = 0;
                if (long.TryParse(scoreCards[counter].PersonId, out personId))
                {
                    requestItem.personId = personId;
                    requestItem.personIdSpecified = true;
                }

                long recordStatusId = 0;
                if (long.TryParse(scoreCards[counter].RecordStatusId, out recordStatusId))
                {
                    requestItem.recordStatusId = recordStatusId;
                    requestItem.recordStatusIdSpecified = true;
                }

                int scoreLevel = 0;
                if (int.TryParse(scoreCards[counter].ScoreLevel, out scoreLevel))
                {
                    requestItem.scoreLevel = scoreLevel;
                    requestItem.scoreLevelSpecified = true;
                }

                long technologyKnowledgeGroupId = 0;
                if (long.TryParse(scoreCards[counter].TechnologyKnowledgeGroupId, out technologyKnowledgeGroupId))
                {
                    requestItem.technologyKnowledgeGroupId = technologyKnowledgeGroupId;
                    requestItem.technologyKnowledgeGroupIdSpecified = true;
                }

                int year = 0;
                if (int.TryParse(scoreCards[counter].Year, out year))
                {
                    requestItem.year = year;
                    requestItem.yearSpecified = true;
                }

                request.scoreCards[counter] = requestItem;
            }

            return Utility.ResolveCodeMessage(DataIntegrationMapper.ToGenericResponse(_smsEndPointClient.saveScoreCard(request)), out messages);
        }
        #endregion
        #endregion
    }
}
