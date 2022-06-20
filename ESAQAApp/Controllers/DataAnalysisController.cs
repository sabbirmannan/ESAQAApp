using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DOF003.Helpers;
using DOF003.Models;
using DAL;
using System.Data;

namespace DOF003.Controllers
{
    [AccessDeniedAuthorize]
    public class DataAnalysisController : BaseController
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        CommonController cc = new CommonController();
        ExecuteClass exec = new ExecuteClass();
        string msg = string.Empty;

        // GET: /DataAnalysis/Index :: Routine Monitoring
        public ActionResult Index()
        {
            var enumReportType = from ReportType e in Enum.GetValues(typeof(ReportType))
                                 select new
                                 {
                                     ID = e.ToString(),
                                     Name = e.ToString()
                                 };

            ViewBag.ReportType = new SelectList(enumReportType, "ID", "Name");
            ViewBag.Rivers = new SelectList(db.Rivers.Where(w => w.IsActive == true && w.IsDelete == false).OrderBy(o => o.RiverName), "RiverID", "RiverName");
            //ViewBag.StrategicLocations = new SelectList(db.StrategicLocations.Where(w => w.IsActive == true && w.IsDelete == false).OrderBy(o => o.StrategicLocationName), "StrategicLocationID", "StrategicLocationName");
            ViewBag.Parameters = new SelectList(db.WaterQualityParams.Where(w => w.IsActive == true && w.IsDelete == false).GroupBy(g => g.ParameterName).Select(s => s.FirstOrDefault()).OrderBy(o => o.ParameterName), "WaterQualityParamID", "ParameterName");

            return View();
        }

        // GET: /DataAnalysis/Non Routine Monitoring
        public ActionResult NRM()
        {
            var enumReportType = from ReportType e in Enum.GetValues(typeof(ReportType))
                                 select new
                                 {
                                     ID = e.ToString(),
                                     Name = e.ToString()
                                 };

            ViewBag.ReportType = new SelectList(enumReportType, "ID", "Name");
            ViewBag.Rivers = new SelectList(db.Rivers.Where(w => w.IsActive == true && w.IsDelete == false).OrderBy(o => o.RiverName), "RiverID", "RiverName");
            //ViewBag.StrategicLocations = new SelectList(db.StrategicLocations.Where(w => w.IsActive == true && w.IsDelete == false).OrderBy(o => o.StrategicLocationName), "StrategicLocationID", "StrategicLocationName");
            ViewBag.Parameters = new SelectList(db.WaterQualityParams.Where(w => w.IsActive == true && w.IsDelete == false).GroupBy(g => g.ParameterName).Select(s => s.FirstOrDefault()).OrderBy(o => o.ParameterName), "WaterQualityParamID", "ParameterName");

            return View();
        }

        // GET: /DataAnalysis/Drinking Water Monitoring
        public ActionResult DWM()
        {
            var enumReportType = from ReportType e in Enum.GetValues(typeof(ReportType))
                                 select new
                                 {
                                     ID = e.ToString(),
                                     Name = e.ToString()
                                 };

            ViewBag.ReportType = new SelectList(enumReportType, "ID", "Name");
            ViewBag.Rivers = new SelectList(db.Rivers.Where(w => w.IsActive == true && w.IsDelete == false).OrderBy(o => o.RiverName), "RiverID", "RiverName");
            //ViewBag.StrategicLocations = new SelectList(db.StrategicLocations.Where(w => w.IsActive == true && w.IsDelete == false).OrderBy(o => o.StrategicLocationName), "StrategicLocationID", "StrategicLocationName");
            ViewBag.Parameters = new SelectList(db.WaterQualityParams.Where(w => w.IsActive == true && w.IsDelete == false).GroupBy(g => g.ParameterName).Select(s => s.FirstOrDefault()).OrderBy(o => o.ParameterName), "WaterQualityParamID", "ParameterName");

            return View();
        }

        // GET: /DataAnalysis/Ground Water Monitoring
        public ActionResult GWM()
        {
            var enumReportType = from ReportType e in Enum.GetValues(typeof(ReportType))
                                 select new
                                 {
                                     ID = e.ToString(),
                                     Name = e.ToString()
                                 };

            ViewBag.ReportType = new SelectList(enumReportType, "ID", "Name");
            ViewBag.Rivers = new SelectList(db.Rivers.Where(w => w.IsActive == true && w.IsDelete == false).OrderBy(o => o.RiverName), "RiverID", "RiverName");
            //ViewBag.StrategicLocations = new SelectList(db.StrategicLocations.Where(w => w.IsActive == true && w.IsDelete == false).OrderBy(o => o.StrategicLocationName), "StrategicLocationID", "StrategicLocationName");
            ViewBag.Parameters = new SelectList(db.WaterQualityParams.Where(w => w.IsActive == true && w.IsDelete == false).GroupBy(g => g.ParameterName).Select(s => s.FirstOrDefault()).OrderBy(o => o.ParameterName), "WaterQualityParamID", "ParameterName");

            return View();
        }

        // GET: /DataAnalysis/StrategicLocations
        public ActionResult StrategicLocations()
        {
            var enumReportType = from ReportType e in Enum.GetValues(typeof(ReportType))
                                 select new
                                 {
                                     ID = e.ToString(),
                                     Name = e.ToString()
                                 };

            ViewBag.ReportType = new SelectList(enumReportType, "ID", "Name");
            ViewBag.Rivers = new SelectList(db.Rivers.Where(w => w.IsActive == true && w.IsDelete == false).OrderBy(o => o.RiverName), "RiverID", "RiverName");
            //ViewBag.StrategicLocations = new SelectList(db.StrategicLocations.Where(w => w.IsActive == true && w.IsDelete == false).OrderBy(o => o.StrategicLocationName), "StrategicLocationID", "StrategicLocationName");
            ViewBag.Parameters = new SelectList(db.WaterQualityParams.Where(w => w.IsActive == true && w.IsDelete == false).GroupBy(g => g.ParameterName).Select(s => s.FirstOrDefault()).OrderBy(o => o.ParameterName), "WaterQualityParamID", "ParameterName");

            return View();
        }

        // GET: /DataAnalysis/Graph
        public ActionResult Graph()
        {
            var enumReportType = from ReportType e in Enum.GetValues(typeof(ReportType))
                                 select new
                                 {
                                     //ID = (int)e,
                                     ID = e.ToString(),
                                     Name = e.ToString()
                                 };

            var enumMonths = from Month e in Enum.GetValues(typeof(Month))
                             select new
                             {
                                 ID = e.ToString(),
                                 Name = e.ToString()
                             };

            ViewBag.Years = new SelectList(cc.GetYearList(), "ID", "Name");
            ViewBag.Months = new SelectList(enumMonths, "ID", "Name");
            ViewBag.ReportType = new SelectList(enumReportType, "ID", "Name");
            ViewBag.Rivers = new SelectList(db.Rivers.Where(w => w.IsActive == true && w.IsDelete == false).OrderBy(o => o.RiverName), "RiverID", "RiverName");
            //ViewBag.StrategicLocations = new SelectList(db.StrategicLocations.Where(w => w.IsActive == true && w.IsDelete == false).OrderBy(o => o.StrategicLocationName), "StrategicLocationID", "StrategicLocationName");
            ViewBag.Parameters = new SelectList(db.WaterQualityParams.Where(w => w.IsActive == true && w.IsDelete == false).OrderBy(o => o.ParameterName), "ParameterName", "ParameterName");
            ViewBag.Units = new SelectList(db.WaterQualityParams.Where(w => w.IsActive == true && w.IsDelete == false).OrderBy(o => o.ParameterName), "ParameterName", "Unit");
            ViewBag.EQS = new SelectList(db.WaterQualityCategories.Where(w => w.IsActive == true && w.IsDelete == false).OrderBy(o => o.WaterQualityCategoryID), "WaterQualityCategoryID", "CategoryName");

            return View();
        }

        // GET: /DataAnalysis/Graph
        public ActionResult TimeSeries()
        {
            //var enumReportType = from ReportType e in Enum.GetValues(typeof(ReportType)) select new { ID = e.ToString(), Name = e.ToString() };
            var enumMonths = from Month e in Enum.GetValues(typeof(Month)) select new { ID = e.ToString(), Name = e.ToString() };

            //ViewBag.Months = new SelectList(enumMonths, "ID", "Name");
            //ViewBag.ReportType = new SelectList(enumReportType, "ID", "Name");
            //ViewBag.Rivers = new SelectList(db.Rivers.Where(w => w.IsActive == true && w.IsDelete == false).OrderBy(o => o.RiverName), "RiverID", "RiverName");
            ViewBag.FromYears = new SelectList(cc.GetYearList(true), "ID", "Name");
            ViewBag.ToYears = new SelectList(cc.GetYearList(), "ID", "Name");
            ViewBag.StrategicLocations = new SelectList(db.StrategicLocations.Where(w => w.IsActive == true && w.IsDelete == false).OrderBy(o => o.StrategicLocationName), "StrategicLocationID", "StrategicLocationName");
            ViewBag.Parameters = new SelectList(db.WaterQualityParams.Where(w => w.IsActive == true && w.IsDelete == false).OrderBy(o => o.ParameterName), "ParameterName", "ParameterName");
            ViewBag.Units = new SelectList(db.WaterQualityParams.Where(w => w.IsActive == true && w.IsDelete == false).OrderBy(o => o.ParameterName), "ParameterName", "Unit");
            ViewBag.EQS = new SelectList(db.WaterQualityCategories.Where(w => w.IsActive == true && w.IsDelete == false).OrderBy(o => o.WaterQualityCategoryID), "WaterQualityCategoryID", "CategoryName");

            return View();
        }

        // GET: /DataAnalysis/Individual
        public ActionResult Individual()
        {
            ViewBag.Rivers = new SelectList(db.Rivers.Where(w => w.IsActive == true && w.IsDelete == false).OrderBy(o => o.RiverName), "RiverID", "RiverName");
            //ViewBag.StrategicLocations = new SelectList(db.StrategicLocations.Where(w => w.IsActive == true && w.IsDelete == false).OrderBy(o => o.StrategicLocationName), "StrategicLocationID", "StrategicLocationName");
            ViewBag.Parameters = new SelectList(db.WaterQualityParams.Where(w => w.IsActive == true && w.IsDelete == false).OrderBy(o => o.ParameterName), "ParameterName", "ParameterName");

            return View();
        }
    }
}
