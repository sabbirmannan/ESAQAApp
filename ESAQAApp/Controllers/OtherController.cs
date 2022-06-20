using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DOF003.Controllers
{
    public class OtherController : Controller
    {
        // GET: /Other/ContactUs
        public ActionResult ContactUs()
        {
            return View();
        }

        //
        // GET: /Other/UserManual
        public ActionResult UserManual()
        {
            return View();
        }
	}
}