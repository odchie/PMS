using System;
using System.Configuration;
using System.Collections.Generic;
using System.DirectoryServices;
using System.Linq;
using System.Transactions;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using DotNetOpenAuth.AspNet;
using Microsoft.Web.WebPages.OAuth;
using WebMatrix.WebData;

using Pms.Common.Variable;
using Pms.Entity;
using Pms.Entity.Interface;
using Pms.Presentation.Mvc.Models;
using Pms.Presentation.Mvc.Helper;
using Pms.Repository;

using pmsVariable = Pms.Common.Variable;

namespace Pms.Presentation.Mvc.Controllers
{
    [Authorize]
    public class AccountController : BaseController
    {
        #region Variables
        private IUserManagementRepository _userManagementRepository;
        #endregion

        #region Constructors
        public AccountController() { }

        public AccountController(IUserManagementRepository userManagementRepository)
        {
            this._userManagementRepository = userManagementRepository;
        }
        #endregion

        [AllowAnonymous]
        public ActionResult Login(string returnUrl)
        {
            ViewBag.ReturnUrl = returnUrl;
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult Login(LoginModel model, string returnUrl)
        {
            if (ModelState.IsValid)
            {
                if (Membership.ValidateUser(model.UserName, model.Password))
                {
                    using (DirectoryEntry de = new DirectoryEntry(ConfigurationManager.ConnectionStrings[ConfigurationKey.ActiveDirectoryConnectionString].ToString(), model.UserName, model.Password))
                    {
                        using (DirectorySearcher adSearch = new DirectorySearcher(de))
                        {
                            const string _filterFormat = "(sAMAccountName={0})";
                            adSearch.Filter = string.Format(_filterFormat, model.UserName);
                            SearchResult adSearchResult = adSearch.FindOne();

                            if (adSearchResult.Properties.Count != 0)
                            {
                                const string _employeeIdkey = "employeeid";
                                const string _emailkey = "userprincipalname";

                                model.EmployeeId = adSearchResult.Properties[_employeeIdkey][0].ToString();
                                model.Email = adSearchResult.Properties[_emailkey][0].ToString();
                                Session[SessioKey.LoginCredential] = model;

                                if (model.Email == null || string.IsNullOrEmpty(model.Email))
                                {
                                    ModelState.AddModelError(string.Empty, Resources.Pms.Login_InvalidLogin);
                                }
                                else
                                {
                                    IList<CodeMessage> messages = new List<CodeMessage>();
                                    this._userManagementRepository.ServiceHeaders = PresentationUtility.GetBasicHeaders(new string[] { ServiceHeaderKey.EmployeeId, ServiceHeaderKey.UserName, ServiceHeaderKey.Email });
                                    IEmployee employee = _userManagementRepository.EmployeeGet(SearchKey.Email, model.Email, out messages);
                                    if (employee == null)
                                    {
                                        Session[SessioKey.LoginCredential] = null;

                                        foreach (CodeMessage message in messages)
                                        {
                                            ModelState.AddModelError(string.Empty, message.Name);
                                        }

                                        return View(model);
                                    }
                                    else
                                    {
                                        model.PersonId = employee.PersonId;
                                        model.EmployeeId = employee.EmployeeId;
                                        model.AccountState = string.IsNullOrEmpty(employee.PersonObject.PersonStatus) ? 1 : Int16.Parse(employee.PersonObject.PersonStatus);

                                        Session[SessioKey.LoginCredential] = model;
                                        FormsAuthentication.SetAuthCookie(model.UserName, model.RememberMe);
                                    }
                                }
                            }
                        }
                    }

                    if (Url.IsLocalUrl(returnUrl) && returnUrl.Length > 1 && returnUrl.StartsWith("/") && !returnUrl.StartsWith("//") && !returnUrl.StartsWith(@"\//"))
                    {
                        return Redirect(returnUrl);
                    }
                    else
                    {
                        return RedirectToAction(ControllerActionString.Index, ControllerString.Home);
                    }
                }
                else
                {
                    ModelState.AddModelError(string.Empty, Resources.Pms.Login_InvalidLogin);
                }
            }

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult LogOff()
        {
            FormsAuthentication.SignOut();

            return RedirectToAction(ControllerActionString.Index, ControllerString.Home);
        }

        #region Helpers
        private ActionResult RedirectToLocal(string returnUrl)
        {
            if (Url.IsLocalUrl(returnUrl))
            {
                return Redirect(returnUrl);
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
        }

        public enum ManageMessageId
        {
            ChangePasswordSuccess,
            SetPasswordSuccess,
            RemoveLoginSuccess,
        }

        internal class ExternalLoginResult : ActionResult
        {
            public ExternalLoginResult(string provider, string returnUrl)
            {
                Provider = provider;
                ReturnUrl = returnUrl;
            }

            public string Provider { get; private set; }
            public string ReturnUrl { get; private set; }

            public override void ExecuteResult(ControllerContext context)
            {
                OAuthWebSecurity.RequestAuthentication(Provider, ReturnUrl);
            }
        }

        private static string ErrorCodeToString(MembershipCreateStatus createStatus)
        {
            // See http://go.microsoft.com/fwlink/?LinkID=177550 for
            // a full list of status codes.
            switch (createStatus)
            {
                case MembershipCreateStatus.DuplicateUserName:
                    return "User name already exists. Please enter a different user name.";

                case MembershipCreateStatus.DuplicateEmail:
                    return "A user name for that e-mail address already exists. Please enter a different e-mail address.";

                case MembershipCreateStatus.InvalidPassword:
                    return "The password provided is invalid. Please enter a valid password value.";

                case MembershipCreateStatus.InvalidEmail:
                    return "The e-mail address provided is invalid. Please check the value and try again.";

                case MembershipCreateStatus.InvalidAnswer:
                    return "The password retrieval answer provided is invalid. Please check the value and try again.";

                case MembershipCreateStatus.InvalidQuestion:
                    return "The password retrieval question provided is invalid. Please check the value and try again.";

                case MembershipCreateStatus.InvalidUserName:
                    return "The user name provided is invalid. Please check the value and try again.";

                case MembershipCreateStatus.ProviderError:
                    return "The authentication provider returned an error. Please verify your entry and try again. If the problem persists, please contact your system administrator.";

                case MembershipCreateStatus.UserRejected:
                    return "The user creation request has been canceled. Please verify your entry and try again. If the problem persists, please contact your system administrator.";

                default:
                    return "An unknown error occurred. Please verify your entry and try again. If the problem persists, please contact your system administrator.";
            }
        }
        #endregion
    }
}
