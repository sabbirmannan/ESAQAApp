using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DOF003.Controllers
{
    public class GalleryController : BaseController
    {
        // GET: /Gallery/
        public ActionResult Index()
        {
            return View();
        }
	}
}