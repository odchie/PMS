//using System;
//using System.Collections.Generic;
//using System.Linq.Expressions;
//using System.Text;

//using Pms.Entity;
//using Pms.Entity.Interface;
//using Pms.Repository;

//namespace Pms.Mock.Service
//{
//    public class UserManagementService : IPersonRepository
//    {
//        #region Variable
//        private const string MockSession = "Oderpc_78fc";
//        #endregion

//        public UserManagementService()
//        {
//            if (this.GetAccountFromSession() == null)
//            {
//                this.CreateTemporaryUser();
//            }
//        }

//        #region Implements
//        public IList<ServiceHeader> ServiceHeaders { get; set; }

//        public Pms.Entity.Person Get(string id)
//        {
//            return this.GetAccountFromSession().Find(delegate(Pms.Entity.Person user) { return user.PersonId == id; });
//        }

//        public IPerson Get(string key, string value)
//        {
//            return this.GetAccountFromSession().Find(delegate(Pms.Entity.Person user) { return user.Email == value; });
//        }

//        public IList<Pms.Entity.Person> Search(Expression<Func<Pms.Entity.Person, bool>> predicate)
//        {
//            throw new NotImplementedException();
//        }

//        public IList<Pms.Entity.Person> Search(string email, string firstName, string LastName, out int noOfTotalHits)
//        {
//            IList<Pms.Entity.Person> users = this.GetAccountFromSession();
//            noOfTotalHits = users.Count;
//            return users;
//        }

//        public IList<Pms.Entity.Person> GetAll()
//        {
//            return this.GetAccountFromSession();
//        }

//        public bool Insert(Pms.Entity.Person Person)
//        {
//            throw new NotImplementedException();
//        }

//        public bool Insert(Pms.Entity.Interface.IPerson Person, out IList<CodeMessage> messages)
//        {
//            List<Pms.Entity.Person> Persons = this.GetAccountFromSession();
//            Persons.Add(Person as Pms.Entity.Person);
//            System.Web.HttpContext.Current.Session[MockSession] = Persons;

//            messages = new List<CodeMessage>();
//            return true;
//        }

//        public bool Update(Pms.Entity.Person Person)
//        {
//            throw new NotImplementedException();
//        }

//        public bool Update(Pms.Entity.Interface.IPerson Person, out IList<CodeMessage> messages)
//        {
//            this.Delete(Person.PersonId, string.Empty);
//            this.Insert(Person, out messages);
//            return true;
//        }

//        public bool Delete(string id)
//        {
//            throw new NotImplementedException();
//        }

//        public bool Delete(string id, string statusChangeReason)
//        {
//            List<Pms.Entity.Person> Persons = this.GetAccountFromSession();
//            Persons.Remove(this.Get(id));
//            System.Web.HttpContext.Current.Session[MockSession] = Persons;
//            return true;
//        }
//        #endregion

//        #region Private method
//        private List<Pms.Entity.Person> GetAccountFromSession()
//        {
//            if (System.Web.HttpContext.Current.Session[MockSession] == null)
//            {
//                return null;
//            }
//            else
//            {
//                return (List<Pms.Entity.Person>)System.Web.HttpContext.Current.Session[MockSession];
//            }
//        }

//        private IList<Pms.Entity.Person> CreateTemporaryUser()
//        {
//            IList<Pms.Entity.Person> users = new List<Pms.Entity.Person>();
//            for (int counter = 1; counter < 3; counter++)
//            {
//                users.Add(new Pms.Entity.Person()
//                {
//                    PersonId = counter.ToString(),
//                    FirstName = "FirstName" + counter.ToString(),
//                    LastName = "LastName" + counter.ToString(),
//                    MiddleName = "MiddleName" + counter.ToString(),
//                    Email = "email@email.com" + counter.ToString(),
//                    CountryId = "1",
//                    Sex = "Single",
//                    SocialSecurityNumber = "username" + counter.ToString(),
//                    Birthday = DateTime.Now.AddYears(-counter),
//                    CreateDate = DateTime.Now,
//                    Age = counter.ToString()
//                });
//            }

//            System.Web.HttpContext.Current.Session[MockSession] = users;

//            return users;
//        }
//        #endregion
//    }
//}
