using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BAC007.Helpers;
using BAC007.Models;
using DAL;
using System.Data;
using Microsoft.AspNet.Identity;

namespace BAC007.Controllers
{
    public class HomeController : BaseController
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        ExecuteClass exec = new ExecuteClass();
        string msg = string.Empty;

        public ActionResult Index()
        {
            string EntryUser = User.Identity.GetUserName();
            string EntryUserID = User.Identity.GetUserId();

            if (string.IsNullOrEmpty(EntryUser))
            {
                return RedirectToAction("Login", "Account");
            }

            if (EntryUser == "rony" || EntryUser == "admin")
            {
                ViewBag.TotalMasterData = db.Master.Where(w => w.IsDelete == false).ToList().Count;
                //ViewBag.TotalArtisanalDataPA = MasterArtisanal.Where(w => w.IsProjectArea == true).Count();
                //ViewBag.TotalArtisanalDataCA = MasterArtisanal.Where(w => w.IsControlArea == true).Count();

                //var MasterFishMarket = db.MasterFishMarket.Where(w => w.IsDelete == false).ToList();
                //ViewBag.TotalFishMarketDataRetailer = MasterFishMarket.Where(w => w.IsRetailer == true).Count();
                //ViewBag.TotalFishMarketDataWholesaler = MasterFishMarket.Where(w => w.IsWholesaler == true).Count();                
            }
            else
            {
                ViewBag.TotalMasterData = db.Master.Where(w => w.CreatedBy == EntryUser && w.IsDelete == false).ToList().Count;
                //ViewBag.TotalArtisanalDataPA = MasterArtisanal.Where(w => w.IsProjectArea == true).Count();
                //ViewBag.TotalArtisanalDataCA = MasterArtisanal.Where(w => w.IsControlArea == true).Count();

                //var MasterFishMarket = db.MasterFishMarket.Where(w => w.CreatedBy == EntryUser && w.IsDelete == false).ToList();
                //ViewBag.TotalFishMarketDataRetailer = MasterFishMarket.Where(w => w.IsRetailer == true).Count();
                //ViewBag.TotalFishMarketDataWholesaler = MasterFishMarket.Where(w => w.IsWholesaler == true).Count();                
            }

            if (EntryUser == "rony" || EntryUser == "admin")
            {
                //var MasterCluster = db.MasterCluster.Where(w => w.IsDelete == false).ToList();
                //ViewBag.TotalClusterDataPA = MasterCluster.Where(w => w.IsProjectArea == true).Count();
                //ViewBag.TotalClusterDataCA = MasterCluster.Where(w => w.IsControlArea == true).Count();
            }
            else
            {
                //var MasterCluster = db.MasterCluster.Where(w => w.CreatedBy == EntryUser && w.IsDelete == false).ToList();
                //ViewBag.TotalClusterDataPA = MasterCluster.Where(w => w.IsProjectArea == true).Count();
                //ViewBag.TotalClusterDataCA = MasterCluster.Where(w => w.IsControlArea == true).Count();
            }

            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";
            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";
            return View();
        }

        [AllowAnonymous]
        public ActionResult SetCulture(FormCollection formCollection)
        {
            string culture = formCollection["culture"];
            string controller = formCollection["controller"];
            string action = formCollection["action"];

            culture = CultureHelper.GetImplementedCulture(culture);
            HttpCookie cookie = Request.Cookies["_culture"];

            if (cookie != null)
            {
                cookie.Value = culture;
            }
            else
            {
                cookie = new HttpCookie("_culture");
                cookie.Value = culture;
                cookie.Expires = DateTime.Now.AddYears(1);
            }

            Response.Cookies.Add(cookie);

            return RedirectToAction(action, controller);
        }
    }
}
