using System;
using System.ComponentModel;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Web;
using System.Web.Security;
using System.Web.Mvc;
using System.Web.Routing;

using AutoMapper;

using Pms.Common.Helper;
using Pms.Common.Variable;
using Pms.Common.Variable.Enumerations;
using Pms.Entity;
using Pms.Entity.Interface;
using Pms.Presentation.Mvc.Models;

namespace Pms.Presentation.Mvc.Helper
{
    public class PresentationUtility
    {
        public static SelectList CreateSelectList<T>(IEnumerable<T> list, string selectedItem, string keyName, string valueName)
        {
            if (string.IsNullOrEmpty(selectedItem))
            {
                return new SelectList(list, keyName, valueName);
            }
            else
            {
                return new SelectList(list, keyName, valueName, selectedItem);
            }
        }

        public static SelectList CreateSelectList<T>(IEnumerable<T> list, string selectedItem)
        {
            if (string.IsNullOrEmpty(selectedItem))
            {
                return new SelectList(list);
            }
            else
            {
                return new SelectList(list, selectedItem);
            }
        }

        public static SelectList SelectListForLanguage()
        {
            return CreateSelectList<KeyValuePair<string, string>>(Utility.GetLanguages(), Thread.CurrentThread.CurrentCulture.Name, "Key", "Value");
        }

        public static PmsNotification GetNotification(string id)
        {
            PmsNotification notification = new PmsNotification() { Title = string.Empty, Detail = string.Empty };

            try
            {
                const string resourceIdFormatTitle = "Notification_{}_Title";
                notification.Title = Resources.Pms.ResourceManager.GetString(resourceIdFormatTitle);
            }
            catch { }

            try
            {
                const string resourceIdFormatDetail = "Notification_{}_Detail";
                notification.Detail = Resources.Pms.ResourceManager.GetString(resourceIdFormatDetail);
            }
            catch { }

            return notification;
        }

        public static IList<ServiceHeader> GetBasicHeaders(string[] headerId)
        {
            IList<ServiceHeader> serviceHeaders = new List<ServiceHeader>();

            LoginModel login = (LoginModel)HttpContext.Current.Session[SessioKey.LoginCredential];

            foreach (string id in headerId)
            {
                serviceHeaders.Add(new ServiceHeader() { Id = id, Name = typeof(LoginModel).GetProperty(id).GetValue(login, null).ToString() });
            }
            
            return serviceHeaders;
        }

        public static bool IsFirstTime()
        {
            LoginModel loginModel = (LoginModel)HttpContext.Current.Session[SessioKey.LoginCredential];

            if (loginModel == null)
            {
                FormsAuthentication.SignOut();
                return false;
            }
            else
            {
                return loginModel.AccountState == (int)AccountState.Unverified;
            }
        }
    }

    public static class PresentationMapper
    {
        static PresentationMapper()
        {
            AutoMapper.Mapper.CreateMap<IEmployee, EmployeeModel>();
            AutoMapper.Mapper.CreateMap<EmployeeModel, IEmployee>();
            AutoMapper.Mapper.CreateMap<IPerson, PersonModel>();
            AutoMapper.Mapper.CreateMap<PersonModel, IPerson>();
        }

        public static IEmployee ToEntity(this EmployeeModel source)
        {
            return Mapper.Map<EmployeeModel, IEmployee>(source);
        }

        public static EmployeeModel ToModel(this IEmployee source)
        {
            return Mapper.Map<IEmployee, EmployeeModel>(source);
        }

        public static IPerson ToEntity(this PersonModel source)
        {
            return Mapper.Map<PersonModel, IPerson>(source);
        }

        public static PersonModel ToModel(this IPerson source)
        {
            return Mapper.Map<IPerson, PersonModel>(source);
        }
    }

    public class PmsNotification
    {
        public string Title { get; set; }
        public string Detail { get; set; }
    
    }
}