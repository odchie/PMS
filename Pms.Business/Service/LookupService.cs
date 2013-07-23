using System;
using System.Web;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Pms.Common.Helper;
using Pms.Common.Variable;
using Pms.Data.Integration.ServiceIntegration;
using Pms.Entity;
using Pms.Entity.Interface;
using Pms.Repository;

namespace Pms.Business.Service
{
    public class LookupService : ILookupServiceRepository
    {
        #region Variable
        protected LookupIntegration _lookupIntegration;
        #endregion

        #region Constructor
        public LookupService() : this(new LookupIntegration()) { }

        public LookupService(LookupIntegration lookupIntegration)
        {
            _lookupIntegration = lookupIntegration;
        }
        #endregion

        #region Property
        public IList<ServiceHeader> ServiceHeaders
        {
            set { _lookupIntegration.ServiceHeaders = value; }
        }
        #endregion

        #region Implements
        public IList<ICountry> GetAllCountry()
        {
            string cacheKey = Utility.LanguagePrefix() + CacheKeys.CountryList;
            object countries = CacheManager.Get(cacheKey);

            if (countries == null)
            {
                IList<ICountry> returnList = _lookupIntegration.GetAllCountry();
                CacheManager.Add(cacheKey, returnList);
                return returnList;
            }
            else
            {
                return (IList<ICountry>)countries;
            }
        }

        public IList<IDepartment> GetAllDepartment()
        {
            var sessionObject = HttpContext.Current.Session[SessioKey.LookupDepartment];
            if (sessionObject == null)
            {
                IList<IDepartment> lookupObject = _lookupIntegration.GetAllDepartment();
                HttpContext.Current.Session[SessioKey.LookupDepartment] = lookupObject;
                return lookupObject;
            }
            else
            {
                return (IList<IDepartment>)sessionObject;
            }
        }

        public IList<ICareerLevel> GetAllCareerLevel()
        {
            var sessionObject = HttpContext.Current.Session[SessioKey.LookupCareerLevel];
            if (sessionObject == null)
            {
                IList<ICareerLevel> lookupObject= _lookupIntegration.GetAllCareerLevel();
                HttpContext.Current.Session[SessioKey.LookupCareerLevel] = lookupObject;
                return lookupObject;
            }
            else
            {
                return (IList<ICareerLevel>)sessionObject;
            }
        }

        public IList<ICompetency> GetAllCompetency(out IList<CodeMessage> messages)
        {
            var sessionObject = HttpContext.Current.Session[SessioKey.LookupCompetency];
            if (sessionObject == null)
            {
                IList<ICompetency> lookupObject = _lookupIntegration.GetAllCompetency(out messages);
                HttpContext.Current.Session[SessioKey.LookupCompetency] = lookupObject;
                return lookupObject;
            }
            else
            {
                messages = new List<CodeMessage>();
                return (IList<ICompetency>)sessionObject;
            }
        }

        public IList<ICompetencyManager> GetAllCompetencyManager()
        {
            var sessionObject = HttpContext.Current.Session[SessioKey.LookupCompetencyManager];
            if (sessionObject == null)
            {
                IList<ICompetencyManager> lookupObject = _lookupIntegration.GetAllCompetencyManager();
                HttpContext.Current.Session[SessioKey.LookupCompetencyManager] = lookupObject;
                return lookupObject;
            }
            else
            {
                return (IList<ICompetencyManager>)sessionObject;
            }
        }

        public IList<IStaffManager> GetAllStaffManager()
        {
            var sessionObject = HttpContext.Current.Session[SessioKey.LookupStaffManager];
            if (sessionObject == null)
            {
                IList<IStaffManager> lookupObject = _lookupIntegration.GetAllStaffManager();
                HttpContext.Current.Session[SessioKey.LookupStaffManager] = lookupObject;
                return lookupObject;
            }
            else
            {
                return (IList<IStaffManager>)sessionObject;
            }
        }

        public IList<IDepartmentManager> GetAllDepartmentManager()
        {
            var sessionObject = HttpContext.Current.Session[SessioKey.LookupDepartmentManager];
            if (sessionObject == null)
            {
                IList<IDepartmentManager> lookupObject = _lookupIntegration.GetAllDepartmentManager();
                HttpContext.Current.Session[SessioKey.LookupDepartmentManager] = lookupObject;
                return lookupObject;
            }
            else
            {
                return (IList<IDepartmentManager>)sessionObject;
            }
        }

        public IList<IJob> GetAllJob()
        {
            var sessionObject = HttpContext.Current.Session[SessioKey.LookupJob];
            if (sessionObject == null)
            {
                IList<IJob> lookupObject = _lookupIntegration.GetAllJob();
                HttpContext.Current.Session[SessioKey.LookupJob] = lookupObject;
                return lookupObject;
            }
            else
            {
                return (IList<IJob>)sessionObject;
            }
        }

        public IList<IManager> GetAllManager()
        {
            var sessionObject = HttpContext.Current.Session[SessioKey.LookupManager];
            if (sessionObject == null)
            {
                IList<IManager> lookupObject = _lookupIntegration.GetAllManager();
                HttpContext.Current.Session[SessioKey.LookupManager] = lookupObject;
                return lookupObject;
            }
            else
            {
                return (IList<IManager>)sessionObject;
            }
        }

        public IList<IOfficeLocation> GetAllOfficeLocation()
        {
            var sessionObject = HttpContext.Current.Session[SessioKey.LookupOfficeLocation];
            if (sessionObject == null)
            {
                IList<IOfficeLocation> lookupObject = _lookupIntegration.GetAllOfficeLocation();
                HttpContext.Current.Session[SessioKey.LookupOfficeLocation] = lookupObject;
                return lookupObject;
            }
            else
            {
                return (IList<IOfficeLocation>)sessionObject;
            }
        }
        #endregion
    }
}
