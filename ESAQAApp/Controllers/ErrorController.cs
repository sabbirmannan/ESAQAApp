using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BAC007.Controllers
{
    public class ErrorController : Controller
    {
        // GET: /Error/
        public ActionResult Index()
        {
            return View();
        }

        // GET: /Error/BadRequest
        public ActionResult BadRequest()
        {
            return View();
        }

        // GET: /Error/NotFound
        public ActionResult NotFound()
        {
            return View();
        }

        // GET: /Error/NotAllowed
        public ActionResult NotAllowed()
        {
            return View();
        }

        // GET: /Error/RequestTimeout
        public ActionResult RequestTimeout()
        {
            return View();
        }

        // GET: /Error/InternalServerError
        public ActionResult InternalServerError()
        {
            return View();
        }

        // GET: /Error/NotImplemented
        public ActionResult NotImplemented()
        {
            return View();
        }

        // GET: /Error/ServiceUnavailable
        public ActionResult ServiceUnavailable()
        {
            return View();
        }
	}
}