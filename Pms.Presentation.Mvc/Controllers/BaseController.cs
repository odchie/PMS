using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.Mvc;

using Pms.Common.Variable;

namespace Pms.Presentation.Mvc.Controllers
{
    public class BaseController : Controller
    {
        protected override void OnActionExecuting(System.Web.Mvc.ActionExecutingContext filterContext)
        {
            base.OnActionExecuting(filterContext);

            if (filterContext.ActionDescriptor.GetCustomAttributes(typeof(AuthorizeAttribute), false).Any())
            {
                if (Session[SessioKey.LoginCredential] == null)
                {
                    FormsAuthentication.SignOut();
                }
                else
                { 
                    //to do...
                }
            }

            if (RouteData.Values["language"] != null)
            {
                string culture = RouteData.Values["language"].ToString();

                System.Threading.Thread.CurrentThread.CurrentUICulture = new System.Globalization.CultureInfo(culture);
                System.Threading.Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo(culture);
            }
        }
    }
}