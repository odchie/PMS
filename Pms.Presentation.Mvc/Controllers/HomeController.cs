using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Pms.Entity.Interface;

namespace Pms.Presentation.Mvc.Controllers
{
    public class HomeController : BaseController
    {
        [Authorize]
        public ActionResult Index()
        {
            ViewBag.Message = "Performance Management System";
            IPerson modelUser = (IPerson)TempData["UserModel"];
            return View(modelUser);
        }
    }
}
