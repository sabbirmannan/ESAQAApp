using System.Web.Mvc;
using ESAQAApp.Models;

namespace ESAQAApp.Controllers
{
    public class EsaMasterController : BaseController
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        public ActionResult Create()
        {
            return View();
        }
    }
}
