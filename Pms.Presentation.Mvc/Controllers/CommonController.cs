using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Pms.Presentation.Mvc.Helper;

namespace Pms.Presentation.Mvc.Controllers
{
    public class CommonController : BaseController
    {
        public ActionResult Notification(string id)
        {
            PmsNotification notification = PresentationUtility.GetNotification(id);
            ViewBag.Heading = notification.Title;
            ViewBag.Detail = notification.Detail;
            return View();
        }

        public ActionResult Error()
        {
            return View();
        }
    }
}
