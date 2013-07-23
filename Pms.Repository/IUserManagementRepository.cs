using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Pms.Entity;
using Pms.Entity.Interface;

namespace Pms.Repository
{
    public interface IUserManagementRepository
    {
        IList<ServiceHeader> ServiceHeaders { set; }

        //Person
        IPerson PersonGet(string key, string value);
        IList<IPerson> PersonSearch(string accountId, string email, string firstName, string lastName);
        bool PersonInsert(IPerson person, out IList<CodeMessage> messages);
        bool PersonUpdate(IPerson person, out IList<CodeMessage> messages);
        bool PersonDelete(string id, string statusChangeReason);
        bool PersonActivateAccount(string accountId);
        bool PersonActivateByEmail(string activateByEmailId, string activationToken, string email, string userName, out IList<CodeMessage> messages);
        bool PersonBlockAccount(string blockAccountId, string status, string statusChangeReason, out IList<CodeMessage> messages);
        bool PersonLockAccount(string lockAccountId, string email, string status, string useName, string statusChangeReason);
        bool PersonGetAccountState(string getAccountId);

        //Employee
        IEmployee EmployeeGet(string key, string value, out IList<CodeMessage> messages);
        bool EmployeeInsert(IEmployee employee, out IList<CodeMessage> messages);
        bool EmployeeUpdate(IEmployee employee, out IList<CodeMessage> messages);

        //Scorecard
        IList<IScoreCard> ScoreCardGet(string employeeId, string competencyId, out IList<CodeMessage> messages);
        bool ScoreCardInsert(IList<IScoreCard> scoreCards, out IList<CodeMessage> messages);
    }
}
