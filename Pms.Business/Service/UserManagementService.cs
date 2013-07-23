using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Pms.Data.Integration.ServiceIntegration;
using Pms.Entity;
using Pms.Entity.Interface;
using Pms.Repository;

namespace Pms.Business.Service
{
    public class UserManagementService : IUserManagementRepository
    {
        #region Variable
        protected UserManagementIntegration _umsIntegration;
        #endregion

        #region Constructor
        public UserManagementService() : this(new UserManagementIntegration()) { }

        public UserManagementService(UserManagementIntegration userManagementIntegration)
        {
            _umsIntegration = userManagementIntegration;
        }
        #endregion

        #region Property
        public IList<ServiceHeader> ServiceHeaders
        {
            set { _umsIntegration.ServiceHeaders = value; }
        }
        #endregion

        #region Implements
        public IPerson PersonGet(string key, string value)
        {
            return _umsIntegration.GetAccount(key, value);
        }

        public IList<IPerson> PersonSearch(string accountId, string email, string firstName, string lastName)
        {
            return _umsIntegration.SearchAccount(accountId, email, firstName, lastName);
        }

        public bool PersonInsert(IPerson person, out IList<CodeMessage> messages)
        {
            return _umsIntegration.SaveAccount(person, out messages);
        }

        public bool PersonUpdate(IPerson person, out IList<CodeMessage> messages)
        {
            return _umsIntegration.UpdateAccount(person, out messages);
        }

        public bool PersonDelete(string id, string statusChangeReason)
        {
            return _umsIntegration.DeleteAccount(id, statusChangeReason);
        }

        public bool PersonActivateAccount(string accountId)
        {
            return _umsIntegration.ActivateAccount(accountId);
        }

        public bool PersonActivateByEmail(string activateByEmailId, string activationToken, string email, string userName, out IList<CodeMessage> messages)
        {
            return _umsIntegration.ActivateByEmail(activateByEmailId, activationToken, email, userName, out messages);
        }

        public bool PersonBlockAccount(string blockAccountId, string status, string statusChangeReason, out IList<CodeMessage> messages)
        {
            return _umsIntegration.BlockAccount(blockAccountId, status, statusChangeReason, out messages);
        }

        public bool PersonLockAccount(string lockAccountId, string email, string status, string useName, string statusChangeReason)
        {
            return _umsIntegration.LockAccount(lockAccountId, email, status, useName, statusChangeReason);
        }

        public bool PersonGetAccountState(string getAccountId)
        {
            return _umsIntegration.GetAccountState(getAccountId);
        }

        public IEmployee EmployeeGet(string key, string value, out IList<CodeMessage> messages)
        {
            return _umsIntegration.GetEmployeeAccount(key, value, out messages);
        }

        public bool EmployeeInsert(IEmployee employee, out IList<CodeMessage> messages)
        {
            return _umsIntegration.SaveEmployee(employee, out messages);
        }

        public bool EmployeeUpdate(IEmployee employee, out IList<CodeMessage> messages)
        {
            return _umsIntegration.UpdateEmployee(employee, out messages);
        }

        public IList<IScoreCard> ScoreCardGet(string employeeId, string competencyId, out IList<CodeMessage> messages)
        {
            return _umsIntegration.GetScoreCard(employeeId, competencyId, out messages);
        }

        public bool ScoreCardInsert(IList<IScoreCard> scoreCards, out IList<CodeMessage> messages)
        {
            return _umsIntegration.SaveScoreCard(scoreCards, out messages);
        }
        #endregion
    }
}
