//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Linq.Expressions;
//using System.Text;
//using Pms.Entity;
//using Pms.Repository;

//namespace Pms.Mock.Service
//{
//    public class CountryService : ICountryRepository
//    {
//        #region Variable
//        #endregion

//        public CountryService()
//        {
//        }

//        #region Implements
//        public IList<ServiceHeader> ServiceHeaders { get; set; }

//        public Pms.Entity.Country Get(string id)
//        {

//            return ((List<Country>)this.GetAll()).Find(delegate(Pms.Entity.Country country) { return country.Id == id; });
//        }

//        public IList<Pms.Entity.Country> Search(Expression<Func<Pms.Entity.Country, bool>> predicate)
//        {
//            throw new NotImplementedException();
//        }

//        public IList<Pms.Entity.Country> GetAll()
//        {
//            List<Pms.Entity.Country> list = new List<Pms.Entity.Country>();
//            list.Add(new Country() { Id = "1", Name = "Philipines" });
//            return (List<Pms.Entity.Country>)list;
//        }

//        public bool Insert(Pms.Entity.Country country)
//        {
//            throw new NotImplementedException();
//        }

//        public bool Update(Pms.Entity.Country country)
//        {
//            throw new NotImplementedException();
//        }

//        public bool Delete(string id)
//        {
//            throw new NotImplementedException();
//        }
//        #endregion
//    }
//}
