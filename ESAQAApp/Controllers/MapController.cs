using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DAL;
using DOF003.Helpers;
using DOF003.Models;

namespace DOF003.Controllers
{
    public class MapController : BaseController
    {
        #region Initialization
        private readonly ApplicationDbContext _db = new ApplicationDbContext();
        private CommonHelper _ch = new CommonHelper();
        private readonly CommonController _cc = new CommonController();
        private readonly ExecuteClass _exec = new ExecuteClass();
        private string _msg = string.Empty;
        #endregion

        // GET: /Map/
        public ActionResult Index()
        {
            ViewBag.Divisions = (from d in _db.Division.Where(w => w.IsActive == true && w.IsDelete == false)
                                 select new ListItems
                                 {
                                     Value = d.DivisionID,
                                     Text = d.DivisionName
                                 }).ToList();
            return View();
        }

        // GET: /Map/Strategic Location (old)
        public ActionResult SlOld()
        {
            return View();
        }

        // GET: /Map/Ground Water Monitoring Station
        public ActionResult GroundWater()
        {
            return View();
        }

        // GET: /Map/Pollution Hotspot
        public ActionResult PollutionHotspot()
        {
            return View();
        }

        // GET: /Map/Pollution Hotspot
        public ActionResult GenerateGeoJson()
        {
            return View();
        }
    }
}