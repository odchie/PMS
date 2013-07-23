using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

using pmsVariable = Pms.Common.Variable;
using Pms.Common.Helper;
using Pms.Entity;
using Pms.Entity.Interface;
using Pms.Presentation.Mvc.Models;
using Pms.Presentation.Mvc.Helper.Attribute;
using Pms.Presentation.Mvc.Helper;
using Pms.Repository;

using PagedList;

namespace Pms.Presentation.Mvc.Controllers
{
    public class UserManagementController : BaseController
    {
        #region Variables
        private IUserManagementRepository _userManagementRepository;
        private ILookupServiceRepository _lookupServiceRepository;
        #endregion

        #region Constructors
        public UserManagementController() { }

        public UserManagementController(IUserManagementRepository userManagementRepository, ILookupServiceRepository lookupServiceRepository)
        {
            this._lookupServiceRepository = lookupServiceRepository;
            this._lookupServiceRepository.ServiceHeaders = PresentationUtility.GetBasicHeaders(new string[] { pmsVariable.ServiceHeaderKey.EmployeeId, pmsVariable.ServiceHeaderKey.UserName, pmsVariable.ServiceHeaderKey.Email });
            this._userManagementRepository = userManagementRepository;
            this._userManagementRepository.ServiceHeaders = PresentationUtility.GetBasicHeaders(new string[] { pmsVariable.ServiceHeaderKey.EmployeeId, pmsVariable.ServiceHeaderKey.UserName, pmsVariable.ServiceHeaderKey.Email });
        }
        #endregion

        #region Actions
        [Authorize]
        public ActionResult Index()
        {
            return View();
        }

        #region Search
        [Authorize]
        public ActionResult Search(int? page)
        {
            int pageNumber = (page ?? 1);
            int pageSize = 10;

            if (TempData["Model"] != null)
            {
                IList<IPerson> modelUsers = (IList<IPerson>)TempData["Model"];
                TempData["Model"] = modelUsers;

                return View(modelUsers.ToPagedList(pageNumber, pageSize));
            }
            else
            {
                return View();
            }

        }

        [Authorize]
        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Search(string id, string email, string firstName, string lastName)
        {                                  
            int pageSize = 10;
            int pageNumber = 1;            

            this._userManagementRepository.ServiceHeaders = PresentationUtility.GetBasicHeaders(new string[] { pmsVariable.ServiceHeaderKey.EmployeeId, pmsVariable.ServiceHeaderKey.UserName, pmsVariable.ServiceHeaderKey.Email });

            IList<IPerson> modelUsers = null;
            
            modelUsers = this._userManagementRepository.PersonSearch(id, email, firstName, lastName);

            TempData["Model"] = modelUsers;

            return View(modelUsers.ToPagedList(pageNumber, pageSize));
           
        }
        #endregion

        #region Detail
        [Authorize]
        public ActionResult Detail(string id)
        {
            IPerson person = this._userManagementRepository.PersonGet(pmsVariable.SearchKey.Id, id);
            ViewBag.Email = person.ExternalEmail;
            PersonModel PersonModel = PresentationMapper.ToModel(person);
            PersonModel.LoadCountryObject(this._lookupServiceRepository);
            return View(PersonModel);
        }
        #endregion

        #region Create
        [Authorize]
        public ActionResult Create()
        {
            PersonModel person = new PersonModel();
            person.LoadList(_lookupServiceRepository);
            return View(person);
        }

        [Authorize]
        [HttpPost]
        public ActionResult Create(PersonModel person, FormCollection collection)
        {
            if (ModelState.IsValid)
            {
                IList<CodeMessage> messages = new List<CodeMessage>();
                //this._userManagementRepository.ServiceHeaders = PresentationUtility.GetBasicHeaders(new string[] { pmsVariable.ServiceHeaderKey.EmployeeId, pmsVariable.ServiceHeaderKey.UserName, pmsVariable.ServiceHeaderKey.Email });
                this._userManagementRepository.PersonInsert(PresentationMapper.ToEntity(person), out messages);

                return RedirectToAction(pmsVariable.ControllerActionString.Notification, pmsVariable.ControllerString.Common, new { id = "1" });
            }
            else
            {
                person.LoadList(_lookupServiceRepository);
                return View(person);
            }
        }
        #endregion

        #region Edit
        [Authorize]
        public ActionResult Edit()
        {
            IList<CodeMessage> messages = new List<CodeMessage>();
            IEmployee employee = this._userManagementRepository.EmployeeGet(pmsVariable.SearchKey.Email, ((LoginModel)Session[pmsVariable.SessioKey.LoginCredential]).Email, out messages);
            EmployeeModel employeeModel = PresentationMapper.ToModel(employee);
            employeeModel.StaffId = int.Parse(((LoginModel)Session[pmsVariable.SessioKey.LoginCredential]).EmployeeId).ToString();
            employeeModel.PersonModel = PresentationMapper.ToModel(employeeModel.PersonObject);
            employeeModel.LoadList(_lookupServiceRepository);
            employeeModel.PersonModel.LoadList(_lookupServiceRepository);
            return View(employeeModel);
        }

        [Authorize]
        [HttpPost]
        public ActionResult Edit(EmployeeModel employeeModel, FormCollection collection)
        {
            if (ModelState.IsValid)
            {
                IList<CodeMessage> messages = new List<CodeMessage>();
                this._userManagementRepository.EmployeeUpdate(PresentationMapper.ToEntity(employeeModel), out messages);
                return RedirectToAction(pmsVariable.ControllerActionString.Notification, pmsVariable.ControllerString.Common, new { id = "3" });
            }
            else
            {
                employeeModel.StaffId = int.Parse(((LoginModel)Session[pmsVariable.SessioKey.LoginCredential]).EmployeeId).ToString();
                employeeModel.LoadList(_lookupServiceRepository);
                employeeModel.PersonModel.LoadList(_lookupServiceRepository);
                return View(employeeModel);
            }
        }
        #endregion

        #region Delete
        [Authorize]
        public ActionResult Delete(string id, string value)
        {
            return View(this._userManagementRepository.PersonGet(pmsVariable.SearchKey.Id, id));
        }

        [Authorize]
        [HttpPost]
        public ActionResult Delete(string id, string reason, FormCollection collection)
        {
            //how to delete if no ID yet
            //this._userManagementRepository.ServiceHeaders = PresentationUtility.GetBasicHeaders(new string[] { pmsVariable.ServiceHeaderKey.EmployeeId, pmsVariable.ServiceHeaderKey.UserName, pmsVariable.ServiceHeaderKey.Email });
            this._userManagementRepository.PersonDelete(id, reason);
            return RedirectToAction(pmsVariable.ControllerActionString.Notification, pmsVariable.ControllerString.Common, new { id = "2" });
        }
        #endregion
        #endregion

        #region Private methods
        #endregion
    }
}
