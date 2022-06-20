using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.Web.Http.Results;
using System.IO;
using System.Linq.Expressions;
using Microsoft.AspNet.Identity;
using PagedList;
using PagedList.Mvc;
using DAL;
using DOF003.Models;
using DOF003.Helpers;
using DOF003.ViewModels;
using System.Web.Script.Serialization;
using Newtonsoft.Json;
using System.Data.Entity.Validation;

namespace DOF003.Controllers
{
    [AccessDeniedAuthorize]
    public class DataCollectionController : BaseController
    {
        #region Initialization
        private readonly ApplicationDbContext _db = new ApplicationDbContext();
        private CommonHelper _ch = new CommonHelper();
        private readonly CommonController _cc = new CommonController();
        private readonly ExecuteClass _exec = new ExecuteClass();
        private string _msg = string.Empty;
        #endregion

        /*
        [HttpPost]
        public ActionResult GetParameterResult(int MasterID)
        {
            List<GetParameterResult> getParameterResults = new List<GetParameterResult>();
            var serializer = new JavaScriptSerializer { MaxJsonLength = Int32.MaxValue };
            DataTable dtResult = _exec.SelectQuery("Exec sprGetParameterResult " + MasterID, ref _msg);

            if (dtResult.Rows.Count > 0)
            {
                foreach (DataRow row in dtResult.Rows)
                {
                    GetParameterResult pr = new GetParameterResult();
                    pr.SampleResultID = Convert.ToInt32(row["SampleResultID"].ToString());
                    pr.ParameterName = Convert.ToString(row["ParameterName"].ToString());
                    pr.Result = Convert.ToString(row["Result"].ToString());
                    pr.Unit = Convert.ToString(row["Unit"].ToString());
                    pr.CollectionMonth = string.IsNullOrEmpty(row["CollectionMonth"].ToString()) ? (DateTime?)null : DateTime.Parse(row["CollectionMonth"].ToString());
                    pr.Remarks = Convert.ToString(row["Remarks"].ToString());
                    pr.CustomID = Convert.ToString(row["CustomID"].ToString());
                    pr.StrategicLocation = Convert.ToString(row["StrategicLocationName"].ToString());

                    getParameterResults.Add(pr);
                }
            }

            var result = new ContentResult
            {
                Content = serializer.Serialize(JsonConvert.SerializeObject(getParameterResults)),
                ContentType = "application/json"
            };

            return result;
        }
        */

        #region Data Explorer
        // GET: /DataCollection/
        public ActionResult Index(int page = 1, int pageSize = 10)
        {
            List<DataExploreView> dev = new List<DataExploreView>();
            DataTable data = _exec.SelectQuery("Exec sprGetDataExplorer", ref _msg);

            if (data.Rows.Count > 0)
            {
                foreach (DataRow row in data.Rows)
                {
                    DataExploreView de = new DataExploreView();
                    de.DataCollectionMasterID = Convert.ToInt32(row["DataCollectionMasterID"].ToString());
                    de.CustomID = Convert.ToString(row["CustomID"].ToString());
                    de.UpazillaName = Convert.ToString(row["UpazillaName"].ToString());
                    de.RiverName = Convert.ToString(row["RiverName"].ToString());
                    de.SamplingLocation = Convert.ToString(row["StrategicLocationName"].ToString());
                    de.SampleResultCount = Convert.ToInt32(row["SampleResultCount"].ToString());
                    de.SurveyDate = string.IsNullOrEmpty(row["SurveyDate"].ToString()) ? "" : row["SurveyDate"].ToString();
                    de.SLID = Convert.ToString(row["SLID"].ToString());
                    de.Latitude = Convert.ToString(row["Latitude"].ToString());
                    de.Longitude = Convert.ToString(row["Longitude"].ToString());

                    dev.Add(de);
                }
            }

            ViewBag.Rivers = new SelectList(_db.Rivers.Where(w => w.IsActive == true && w.IsDelete == false).OrderBy(o => o.RiverName), "RiverID", "RiverName");
            ViewBag.Parameters = new SelectList(_db.WaterQualityParams.Where(w => w.IsActive == true && w.IsDelete == false).GroupBy(g => g.ParameterName).Select(s => s.FirstOrDefault()).OrderBy(o => o.ParameterName), "WaterQualityParamID", "ParameterName");
            ViewBag.Division = new SelectList(_db.Division.Where(w => w.IsActive == true && w.IsDelete == false).OrderBy(o => o.DivisionName), "DivisionID", "DivisionName");
            //ViewBag.District = new SelectList(_db.District.Where(w => w.IsActive == true && w.IsDelete == false).OrderBy(o => o.DistrictName), "DistrictID", "DistrictName");
            //ViewBag.Upazilla = new SelectList(_db.Upazilla.Where(w => w.IsActive == true && w.IsDelete == false).OrderBy(o => o.UpazillaName), "UpazillaID", "UpazillaName");

            PagedList<DataExploreView> pl = new PagedList<DataExploreView>(dev, page, pageSize);
            return View(pl);
        }

        public ActionResult Search(string ross, int page = 1, int pageSize = 10)
        {
            string DivisionID = string.Empty, DistrictID = string.Empty, UpazillaID = string.Empty, RiverID = string.Empty,
                   StrategicLocationID = string.Empty, StartingDate = string.Empty, EndingDate = string.Empty, Parameters = string.Empty;

            //string slid = Request.QueryString["slid"];
            //int? riverid = 0;
            //if (!string.IsNullOrEmpty(slid))
            //{
            //    riverid = _db.StrategicLocations.Where(w => w.StrategicLocationID == int.Parse(slid)).Select(s => s.RiverID).FirstOrDefault();
            //}

            if (!ross.Contains("****"))
            {
                DivisionID = "NULL";
                DistrictID = "NULL";
                UpazillaID = "NULL";

                int SLID = string.IsNullOrEmpty(ross) ? 0 : Int32.Parse(ross);
                StrategicLocationID = _db.StrategicLocations.Where(w => w.SLID == SLID).Select(s => s.StrategicLocationID.ToString()).FirstOrDefault();
                RiverID = _db.StrategicLocations.Where(w => w.SLID == SLID).Select(s => s.RiverID.ToString()).FirstOrDefault();

                StartingDate = "1990-01-01";
                EndingDate = DateTime.Now.ToString("yyyy-MM-dd");
                Parameters = "NULL";
            }
            else if (ross.Contains("****"))
            {
                string[] firstSeparators = { "****", " " };
                string[] urlParams = ross.Split(firstSeparators, StringSplitOptions.RemoveEmptyEntries);
                string[] secondSeparators = { "**", " " };

                DivisionID = urlParams[0].ToString().Split(secondSeparators, StringSplitOptions.RemoveEmptyEntries)[1].ToString();
                DistrictID = urlParams[1].ToString().Split(secondSeparators, StringSplitOptions.RemoveEmptyEntries)[1].ToString();
                UpazillaID = urlParams[2].ToString().Split(secondSeparators, StringSplitOptions.RemoveEmptyEntries)[1].ToString();
                RiverID = urlParams[3].ToString().Split(secondSeparators, StringSplitOptions.RemoveEmptyEntries)[1].ToString();
                StrategicLocationID = urlParams[4].ToString().Split(secondSeparators, StringSplitOptions.RemoveEmptyEntries)[1].ToString();
                StartingDate = urlParams[5].ToString().Split(secondSeparators, StringSplitOptions.RemoveEmptyEntries)[1].ToString();
                EndingDate = urlParams[6].ToString().Split(secondSeparators, StringSplitOptions.RemoveEmptyEntries)[1].ToString();
                Parameters = urlParams[7].ToString().Split(secondSeparators, StringSplitOptions.RemoveEmptyEntries)[1].ToString();
            }

            StartingDate = (StartingDate == "NULL") ? "1990-01-01" : StartingDate.ToDatabaseDateFormat();
            EndingDate = (EndingDate == "NULL") ? DateTime.Now.ToString("yyyy-MM-dd") : EndingDate.ToDatabaseDateFormat();

            List<DataExploreView> dev = new List<DataExploreView>();
            //@DivisionID int = NULL, @DistrictID int = NULL, @UpazillaID int = NULL, @RiverID int = NULL, @StrategicLocationID int = NULL, @StartingDate VARCHAR(10) = NULL, @EndingDate VARCHAR(10) = NULL, @Parameters VARCHAR(200) = NULL
            string spr = "Exec sprGetDataExplorer " + DivisionID + ", " + DistrictID + ", " + UpazillaID + ", " + RiverID + ", " + StrategicLocationID + ", N'" + StartingDate + "', N'" + EndingDate + "', " + Parameters + ";";
            DataTable data = _exec.SelectQuery(spr, ref _msg);

            if (data.Rows.Count > 0)
            {
                foreach (DataRow row in data.Rows)
                {
                    DataExploreView de = new DataExploreView();
                    de.DataCollectionMasterID = Convert.ToInt32(row["DataCollectionMasterID"].ToString());
                    de.CustomID = Convert.ToString(row["CustomID"].ToString());
                    de.UpazillaName = Convert.ToString(row["UpazillaName"].ToString());
                    de.RiverName = Convert.ToString(row["RiverName"].ToString());
                    de.SamplingLocation = Convert.ToString(row["StrategicLocationName"].ToString());
                    de.SampleResultCount = Convert.ToInt32(row["SampleResultCount"].ToString());
                    de.SurveyDate = string.IsNullOrEmpty(row["SurveyDate"].ToString()) ? "" : row["SurveyDate"].ToString();
                    de.SLID = Convert.ToString(row["SLID"].ToString());
                    de.Latitude = Convert.ToString(row["Latitude"].ToString());
                    de.Longitude = Convert.ToString(row["Longitude"].ToString());

                    dev.Add(de);
                }
            }

            if (ross.Contains("****"))
            {
                ViewBag.Rivers = new SelectList(_db.Rivers.Where(w => w.IsActive == true && w.IsDelete == false).OrderBy(o => o.RiverName), "RiverID", "RiverName");
            }
            else if (!ross.Contains("****") && !string.IsNullOrEmpty(ross))
            {
                int SLID = string.IsNullOrEmpty(ross) ? 0 : Int32.Parse(ross);
                StrategicLocationID = _db.StrategicLocations.Where(w => w.SLID == SLID).Select(s => s.StrategicLocationID.ToString()).FirstOrDefault();
                RiverID = _db.StrategicLocations.Where(w => w.SLID == SLID).Select(s => s.RiverID.ToString()).FirstOrDefault();

                ViewBag.Rivers = new SelectList(_db.Rivers.Where(w => w.IsActive == true && w.IsDelete == false).OrderBy(o => o.RiverName), "RiverID", "RiverName", RiverID);
                ViewBag.ViewRiverID = RiverID;
                ViewBag.ViewStrategicLocationID = StrategicLocationID;
            }
            else
            {
                ViewBag.Rivers = new SelectList(_db.Rivers.Where(w => w.IsActive == true && w.IsDelete == false).OrderBy(o => o.RiverName), "RiverID", "RiverName");
            }

            ViewBag.Parameters = new SelectList(_db.WaterQualityParams.Where(w => w.IsActive == true && w.IsDelete == false).GroupBy(g => g.ParameterName).Select(s => s.FirstOrDefault()).OrderBy(o => o.ParameterName), "WaterQualityParamID", "ParameterName");
            ViewBag.Division = new SelectList(_db.Division.Where(w => w.IsActive == true && w.IsDelete == false).OrderBy(o => o.DivisionName), "DivisionID", "DivisionName");

            PagedList<DataExploreView> pl = new PagedList<DataExploreView>(dev, page, pageSize);
            return View("Index", pl);
        }

        // GET: /DataCollection/Spatial/
        public async Task<ActionResult> Spatial()
        {
            var dev = await (from m in _db.DataCollectionMaster.Where(w => w.IsActive == true && w.IsDelete == false)
                                 //join upazilla in db.Upazilla on m.UpazillaID equals upazilla.UpazillaID
                                 //join river in db.Rivers on m.RiverID equals river.RiverID
                             select new DataExploreView
                             {
                                 DataCollectionMasterID = m.DataCollectionMasterID,
                                 CustomID = m.CustomID,
                                 //UpazillaName = upazilla.UpazillaName,
                                 //RiverName = river.RiverName,
                                 //SamplingLocation = m.SamplingLocation,
                                 SurveyDate = m.SurveyDate
                             }).ToListAsync();

            return View(dev);
        }

        // GET: /DataCollection/Timer/
        public async Task<ActionResult> Timer()
        {
            var dev = await (from m in _db.DataCollectionMaster.Where(w => w.IsActive == true && w.IsDelete == false)
                                 //join upazilla in db.Upazilla on m.UpazillaID equals upazilla.UpazillaID
                                 //join river in db.Rivers on m.RiverID equals river.RiverID
                             select new DataExploreView
                             {
                                 DataCollectionMasterID = m.DataCollectionMasterID,
                                 CustomID = m.CustomID,
                                 //UpazillaName = upazilla.UpazillaName,
                                 //RiverName = river.RiverName,
                                 //SamplingLocation = m.SamplingLocation,
                                 SurveyDate = m.SurveyDate
                             }).ToListAsync();

            return View(dev);
        }
        #endregion

        #region Create and Import
        // GET: /DataCollection/Create
        public ActionResult Create()
        {
            //ViewBag.DataCollectionMasterID = 0;

            ViewBag.CustomID = "WQMS-###-" + DateTime.Now.ToString("ddMMyy") + "-" + _db.DataCollectionMaster.Count().ToString().PadLeft(5, '0');
            ViewBag.StrategicLocationID = new SelectList(_db.StrategicLocations.Where(w => w.IsActive == true && w.IsDelete == false).OrderBy(o => o.StrategicLocationName), "StrategicLocationID", "StrategicLocationName");
            ViewBag.WaterQualityParamID = new SelectList(_db.WaterQualityParams.Where(w => w.IsActive == true && w.IsDelete == false).GroupBy(g => g.ParameterName).Select(s => s.FirstOrDefault()).OrderBy(o => o.ParameterName), "WaterQualityParamID", "ParameterName");
            ViewBag.PurposeOfSampleID = new SelectList(_db.PurposeOfSamples.Where(w => w.IsActive == true && w.IsDelete == false).OrderBy(o => o.PurposeOfSampleName), "PurposeOfSampleID", "PurposeOfSampleName");
            ViewBag.TypeOfSampleID = new SelectList(_db.WaterQualityCategories.Where(w => w.IsActive == true && w.IsDelete == false).OrderBy(o => o.CategoryName), "WaterQualityCategoryID", "CategoryName");
            var WeatherConditionList = from WeatherCondition s in Enum.GetValues(typeof(WeatherCondition)) select new { ID = s, Name = s.ToDescription() };
            ViewBag.WeatherCondition = new SelectList(WeatherConditionList, "ID", "Name");

            //inline form 29 mar 2017
            ViewBag.Rivers = new SelectList(_db.Rivers.Where(w => w.IsActive == true && w.IsDelete == false).OrderBy(g => g.RiverName), "RiverID", "RiverName");
            ViewBag.Parameters = new SelectList(_db.WaterQualityParams.Where(w => w.IsActive == true && w.IsDelete == false).GroupBy(g => g.ParameterName).Select(s => s.FirstOrDefault()).OrderBy(o => o.ParameterName), "WaterQualityParamID", "ParameterName");

            ViewBag.SampleCollectionDateTime = "";
            ViewBag.ReceivingDateTimeAtLab = "";
            ViewBag.CompletionDate = "";

            var datacollection = new DataCollection();

            return View(datacollection);
        }

        // POST: /DataCollection/Create
        [HttpPost]
        [ValidateInput(false)]
        //[ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(DataCollection datacollection)
        {
            var dataCollectionMasterId = datacollection.DataCollectionMaster.DataCollectionMasterID;

            try
            {
                #region Data Collection Master: Save
                #region Save
                if (dataCollectionMasterId == 0)
                {
                    datacollection.DataCollectionMaster.SurveyDate = datacollection.DataCollectionMaster.SampleCollectionDateTime.Substring(0, 10).Trim();
                    datacollection.DataCollectionMaster.CreatedBy = User.Identity.GetUserId();
                    datacollection.DataCollectionMaster.CreatedDate = DateTime.Now;
                    datacollection.DataCollectionMaster.IsActive = true;
                    datacollection.DataCollectionMaster.IsDelete = false;

                    try
                    {
                        _db.DataCollectionMaster.Add(datacollection.DataCollectionMaster);
                        var x = await _db.SaveChangesAsync();

                        if (x > 0)
                        {
                            ViewBag.DataCollectionMasterID = datacollection.DataCollectionMaster.DataCollectionMasterID;
                            TempData["Message"] = ShowMessage(Sign.Success, OperationMessage.Success.ToDescription());
                        }
                        else
                        {
                            ViewBag.DataCollectionMasterID = 0;
                            TempData["Message"] = ShowMessage(Sign.Error, OperationMessage.NotSuccess.ToDescription());
                        }
                    }
                    catch (DbEntityValidationException e)
                    {
                        foreach (var eve in e.EntityValidationErrors)
                        {
                            Console.WriteLine("Entity of type \"{0}\" in state \"{1}\" has the following validation errors:",
                                eve.Entry.Entity.GetType().Name, eve.Entry.State);
                            foreach (var ve in eve.ValidationErrors)
                            {
                                Console.WriteLine("- Property: \"{0}\", Error: \"{1}\"",
                                    ve.PropertyName, ve.ErrorMessage);
                            }
                        }
                        throw;
                    }
                    catch (Exception ex)
                    {
                        var message = ExtractInnerException(ex);
                        TempData["Message"] = ShowMessage(Sign.Danger, message);
                    }
                }
                #endregion

                #region Update
                if (dataCollectionMasterId != 0)
                {
                    datacollection.DataCollectionMaster.SurveyDate = datacollection.DataCollectionMaster.SampleCollectionDateTime;
                    datacollection.DataCollectionMaster.UpdateBy = User.Identity.GetUserId();
                    datacollection.DataCollectionMaster.UpdatedDate = DateTime.Now;
                    datacollection.DataCollectionMaster.IsActive = true;
                    datacollection.DataCollectionMaster.IsDelete = false;

                    try
                    {
                        _db.Entry(datacollection.DataCollectionMaster).State = EntityState.Modified;
                        var x = await _db.SaveChangesAsync();

                        if (x > 0)
                        {
                            TempData["Message"] = ShowMessage(Sign.Success, OperationMessage.Update.ToDescription());
                        }
                        else
                        {
                            TempData["Message"] = ShowMessage(Sign.Error, OperationMessage.NotUpdate.ToDescription());
                        }
                    }
                    catch (Exception ex)
                    {
                        var message = ExtractInnerException(ex);
                        TempData["Message"] = ShowMessage(Sign.Danger, message);
                    }

                    ViewBag.DataCollectionMasterID = datacollection.DataCollectionMaster.DataCollectionMasterID;
                }
                #endregion
                #endregion
            }
            catch (Exception ex)
            {
                var message = ExtractInnerException(ex);
                TempData["Message"] = ShowMessage(Sign.Danger, message);
            }

            ViewBag.CustomID = datacollection.DataCollectionMaster.CustomID;
            ViewBag.StrategicLocationID = new SelectList(_db.StrategicLocations.Where(w => w.IsActive == true && w.IsDelete == false).OrderBy(o => o.StrategicLocationName), "StrategicLocationID", "StrategicLocationName", datacollection.DataCollectionMaster.StrategicLocationID);
            ViewBag.WaterQualityParamID = new SelectList(_db.WaterQualityParams.Where(w => w.IsActive == true && w.IsDelete == false).GroupBy(g => g.ParameterName).Select(s => s.FirstOrDefault()).OrderBy(o => o.ParameterName), "WaterQualityParamID", "ParameterName");
            datacollection.HydrologicalInformation = _db.HydrologicalInformations.FirstOrDefault(w => w.DataCollectionMasterID == datacollection.DataCollectionMaster.DataCollectionMasterID && w.IsActive == true && w.IsDelete == false);

            ViewBag.PurposeOfSampleID = new SelectList(_db.PurposeOfSamples.Where(w => w.IsActive == true && w.IsDelete == false).OrderBy(o => o.PurposeOfSampleName), "PurposeOfSampleID", "PurposeOfSampleName", datacollection.DataCollectionMaster.PurposeOfSampleID);
            ViewBag.TypeOfSampleID = new SelectList(_db.WaterQualityCategories.Where(w => w.IsActive == true && w.IsDelete == false).OrderBy(o => o.CategoryName), "WaterQualityCategoryID", "CategoryName", datacollection.DataCollectionMaster.TypeOfSampleID);
            var WeatherConditionList = from WeatherCondition s in Enum.GetValues(typeof(WeatherCondition)) select new { ID = s, Name = s.ToDescription() };
            ViewBag.WeatherCondition = new SelectList(WeatherConditionList, "ID", "Name", datacollection.DataCollectionMaster.WeatherCondition);

            //inline form 29 mar 2017
            ViewBag.Rivers = new SelectList(_db.Rivers.Where(w => w.IsActive == true && w.IsDelete == false).OrderBy(g => g.RiverName), "RiverID", "RiverName");
            ViewBag.Parameters = new SelectList(_db.WaterQualityParams.Where(w => w.IsActive == true && w.IsDelete == false).GroupBy(g => g.ParameterName).Select(s => s.FirstOrDefault()).OrderBy(o => o.ParameterName), "WaterQualityParamID", "ParameterName");

            ViewBag.SampleCollectionDateTime = datacollection.DataCollectionMaster.SampleCollectionDateTime;
            ViewBag.ReceivingDateTimeAtLab = datacollection.DataCollectionMaster.ReceivingDateTimeAtLab;
            ViewBag.CompletionDate = datacollection.DataCollectionMaster.CompletionDate;

            return View(datacollection);
        }

        // GET: /DataCollection/Import/
        public async Task<ActionResult> Import()
        {
            var dev = await (from m in _db.DataCollectionMaster.Where(w => w.IsActive == true && w.IsDelete == false)
                                 //join upazilla in db.Upazilla on m.UpazillaID equals upazilla.UpazillaID
                                 //join river in db.Rivers on m.RiverID equals river.RiverID
                             select new DataExploreView
                             {
                                 DataCollectionMasterID = m.DataCollectionMasterID,
                                 CustomID = m.CustomID,
                                 //UpazillaName = upazilla.UpazillaName,
                                 //RiverName = river.RiverName,
                                 //SamplingLocation = m.SamplingLocation,
                                 SurveyDate = m.SurveyDate
                             }).ToListAsync();

            return View(dev);
        }

        [HttpPost]
        public ActionResult ProcessUploadFileData()
        {
            string userID = System.Web.HttpContext.Current.User.Identity.GetUserId();
            List<ImportedParamResult> sll = new List<ImportedParamResult>();
            DataTable dt = new DataTable();

            if (HttpContext.Request.Files.AllKeys.Any())
            {
                var file = HttpContext.Request.Files["UploadedImage"];

                if (file.ContentLength > 0)
                {
                    string extension = System.IO.Path.GetExtension(file.FileName).ToLower();
                    string connString = "";

                    string[] validFileTypes = { ".xls", ".xlsx", ".csv" };

                    string path = string.Format("{0}/{1}", Server.MapPath("~/Content/Uploads"), file.FileName);

                    if (!Directory.Exists(path))
                    {
                        Directory.CreateDirectory(Server.MapPath("~/Content/Uploads"));
                    }

                    if (validFileTypes.Contains(extension))
                    {
                        try
                        {
                            if (System.IO.File.Exists(path))
                            {
                                System.IO.File.Delete(path);
                            }

                            file.SaveAs(path);

                            if (extension.ToLower() == ".csv")
                            {
                                dt = Utility.ConvertCSVtoDataTable(path);
                                ViewBag.Data = dt;
                            }
                            else if (extension.ToLower().Trim() == ".xls")
                            {
                                connString = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + path + ";Extended Properties=\"Excel 8.0;HDR=Yes;IMEX=2\"";
                                dt = Utility.ConvertXSLXtoDataTable(path, connString);
                                ViewBag.Data = dt;
                            }
                            else if (extension.ToLower().Trim() == ".xlsx")
                            {
                                connString = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + path + ";Extended Properties=\"Excel 12.0;HDR=Yes;IMEX=2\"";
                                dt = Utility.ConvertXSLXtoDataTable(path, connString);
                                ViewBag.Data = dt;
                            }

                            //int a = InsertDoEBulkData(dt);
                            sll = ProcessDataTable(dt);
                        }
                        catch (Exception ex)
                        {
                            return Json(ExtractInnerException(ex), JsonRequestBehavior.AllowGet);
                            //ViewBag.Error = ExtractInnerException(ex);
                            //TempData["Message"] = ShowMessage(Sign.Error, ExtractInnerException(ex));
                        }
                    }
                    else
                    {
                        return Json("Error: Please Upload Files in .xls, .xlsx or .csv format", JsonRequestBehavior.AllowGet);
                        //ViewBag.Error = "Please Upload Files in .xls, .xlsx or .csv format";
                        //TempData["Message"] = ShowMessage(Sign.Error, "Please Upload Files in .xls, .xlsx or .csv format");
                    }
                }
            }

            return Json(sll, JsonRequestBehavior.AllowGet);
        }

        //rony 09 04 17
        private int InsertDoEBulkData(DataTable dt)
        {
            if (dt.Rows.Count > 0)
            {
                DataCollectionMaster dcm = new DataCollectionMaster();
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    string strategicLocationID = _exec.SelectSingleQuery("SELECT [StrategicLocationID] FROM [dbo].[StrategicLocations] WHERE [StrategicLocationName] = '" + dt.Rows[i]["Sample Location"].ToString() + "';", ref _msg);
                    if (int.Parse(strategicLocationID) > 0)
                    {
                        #region Data Collection : Master Part
                        dcm = new DataCollectionMaster();
                        var CustomID = "WQMS-" + strategicLocationID.ToString().PadLeft(5, '0') + "-" + DateTime.Now.ToString("ddMMyy") + "-" + _db.DataCollectionMaster.Count().ToString().PadLeft(5, '0');
                        dcm.CustomID = CustomID;

                        int slId;
                        int? val = Int32.TryParse(strategicLocationID, out slId) ? slId : (int?)null;
                        dcm.StrategicLocationID = val;

                        dcm.SurveyDate = dt.Rows[i]["Date"].ToString();
                        dcm.LabCode = dt.Rows[i]["Lab Code No"].ToString();

                        string _datetime = dt.Rows[i]["Date"].ToString() + " " + dt.Rows[i]["Time"].ToString();
                        if (!string.IsNullOrEmpty(_datetime) && _datetime != " ")
                        {
                            dcm.SampleCollectionDateTime = !string.IsNullOrEmpty(_datetime) ? _datetime : "";
                        }
                        else
                        {
                            dcm.SampleCollectionDateTime = "";
                        }

                        dcm.SampleReceivedBy = dt.Rows[i]["Sample received by"].ToString();

                        _datetime = dt.Rows[i]["Receiving date at lab"].ToString() + " " + dt.Rows[i]["Receiving time at lab"].ToString();
                        if (!string.IsNullOrEmpty(_datetime) && _datetime != " ")
                        {
                            dcm.ReceivingDateTimeAtLab = !string.IsNullOrEmpty(_datetime) ? _datetime : "";
                        }
                        else
                        {
                            dcm.ReceivingDateTimeAtLab = "";
                        }

                        _datetime = dt.Rows[i]["Analysis completion date"].ToString();
                        if (!string.IsNullOrEmpty(_datetime))
                        {
                            dcm.CompletionDate = !string.IsNullOrEmpty(_datetime) ? _datetime : "";
                        }
                        else
                        {
                            dcm.CompletionDate = "";
                        }

                        dcm.WeatherCondition = _cc.GetWeatherCondition(dt.Rows[i]["Weather Condition"].ToString().Trim());
                        dcm.PurposeOfSampleID = _cc.GetPurposeOfSampleName(dt.Rows[i]["Purpose of Sample"].ToString().Trim());
                        dcm.TypeOfSampleID = _cc.GetTypeOfSample(dt.Rows[i]["Type of Sample"].ToString().Trim());

                        dcm.CreatedBy = User.Identity.GetUserId();
                        dcm.CreatedDate = DateTime.Now;
                        dcm.IsActive = true;
                        dcm.IsDelete = false;

                        _db.DataCollectionMaster.Add(dcm);
                        var x = _db.SaveChanges();
                        #endregion

                        #region Sample Results
                        int p = 18;
                        List<SampleResult> sampleResults = new List<SampleResult>();
                        for (p = 18; p < 55; p++)
                        {
                            SampleResult _sampleResult = new SampleResult();
                            _sampleResult.DataCollectionMasterID = dcm.DataCollectionMasterID;
                            _sampleResult.ParameterName = dt.Columns[p].ColumnName.ToString();
                            _sampleResult.Result = dt.Rows[i][p].ToString();
                            _sampleResult.CollectionMonth = dcm.SampleCollectionDateTime;
                            _sampleResult.Remarks = dt.Rows[i]["Notes"].ToString();

                            sampleResults.Add(_sampleResult);
                        }

                        _db.SampleResults.AddRange(sampleResults);
                        _db.SaveChanges();
                        #endregion
                    }
                    else
                    {
                        string fileName = Server.MapPath("~/log.txt");
                        using (FileStream fs = new FileStream(fileName, FileMode.Append, FileAccess.Write))
                        using (StreamWriter sw = new StreamWriter(fs))
                        {
                            sw.WriteLine(dt.Rows[i]["Sample Location"].ToString());
                        }
                    }
                }
            }

            return 0;
        }

        private static List<ImportedParamResult> ProcessDataTable(DataTable dataTable)
        {
            List<ImportedParamResult> iprs = new List<ImportedParamResult>();

            if (dataTable.Rows.Count > 0)
            {
                for (int i = 0; i < dataTable.Rows.Count; i++)
                {
                    ImportedParamResult ipr = new ImportedParamResult();
                    ipr.Serial = i + 1;
                    ipr.StrategicLocations = dataTable.Rows[i][0].ToString();
                    ipr.River = dataTable.Rows[i][1].ToString();
                    ipr.Parameters = dataTable.Rows[i][2].ToString();
                    ipr.PurposeOfSample = dataTable.Rows[i][3].ToString();
                    ipr.TypeOfSample = dataTable.Rows[i][4].ToString();

                    ipr.Jan = string.IsNullOrEmpty(dataTable.Rows[i][5].ToString()) ? "0" : dataTable.Rows[i][5].ToString();
                    ipr.Feb = string.IsNullOrEmpty(dataTable.Rows[i][6].ToString()) ? "0" : dataTable.Rows[i][6].ToString();
                    ipr.Mar = string.IsNullOrEmpty(dataTable.Rows[i][7].ToString()) ? "0" : dataTable.Rows[i][7].ToString();
                    ipr.Apr = string.IsNullOrEmpty(dataTable.Rows[i][8].ToString()) ? "0" : dataTable.Rows[i][8].ToString();
                    ipr.May = string.IsNullOrEmpty(dataTable.Rows[i][9].ToString()) ? "0" : dataTable.Rows[i][9].ToString();
                    ipr.Jun = string.IsNullOrEmpty(dataTable.Rows[i][10].ToString()) ? "0" : dataTable.Rows[i][10].ToString();
                    ipr.Jul = string.IsNullOrEmpty(dataTable.Rows[i][11].ToString()) ? "0" : dataTable.Rows[i][11].ToString();
                    ipr.Aug = string.IsNullOrEmpty(dataTable.Rows[i][12].ToString()) ? "0" : dataTable.Rows[i][12].ToString();
                    ipr.Sep = string.IsNullOrEmpty(dataTable.Rows[i][13].ToString()) ? "0" : dataTable.Rows[i][13].ToString();
                    ipr.Oct = string.IsNullOrEmpty(dataTable.Rows[i][14].ToString()) ? "0" : dataTable.Rows[i][14].ToString();
                    ipr.Nov = string.IsNullOrEmpty(dataTable.Rows[i][15].ToString()) ? "0" : dataTable.Rows[i][15].ToString();
                    ipr.Dec = string.IsNullOrEmpty(dataTable.Rows[i][16].ToString()) ? "0" : dataTable.Rows[i][16].ToString();
                    iprs.Add(ipr);
                }
            }
            else
            {
                iprs = new List<ImportedParamResult>();
            }

            return iprs;
        }

        [HttpPost]
        public async Task<ActionResult> SaveImportedParams(List<ImportedParamResult> iprs)
        {
            bool result = false;
            char[] whitespace = new char[] { ' ', '\t' };
            string CollectionMonth = string.Empty, query = string.Empty;
            Int32 DataCollectionMasterID;
            SampleResult sr = new SampleResult();

            List<SampleResult> sampleResults = new List<SampleResult>();

            //iprs.ForEach(e =>
            foreach (ImportedParamResult e in iprs)
            {
                #region Data Extracting in Month
                if (float.Parse(e.Jan) > 0)
                {
                    sr = new SampleResult();
                    //CollectionMonth = DateTime.Parse("01" + GetPropertyName(() => e.Jan).ToString() + DateTime.Parse(e.ResultDate).Year);
                    CollectionMonth = e.ResultDate;

                    query = "SET DATEFORMAT DMY; Exec sprGetDataCollectionMasterID @StrategicLocation = '" + e.StrategicLocations.Trim() + "', @PurposeOfSample = '" + e.PurposeOfSample.Trim() + "', @TypeOfSample = '" + e.TypeOfSample.Trim() + "', @LabCode = '0', @CollectionDate = '" + CollectionMonth + "', @UploadUser = '" + User.Identity.GetUserId() + "';";
                    DataCollectionMasterID = Int32.Parse(_exec.SelectSingleQuery(query, ref _msg));

                    sr.DataCollectionMasterID = DataCollectionMasterID;
                    sr.ParameterName = e.Parameters;
                    sr.Result = e.Jan;
                    sr.CollectionMonth = CollectionMonth;

                    sr.Remarks = "Bulk data imported from excel";
                    sr.IsActive = true;
                    sr.IsDelete = false;
                    sr.CreatedBy = User.Identity.GetUserId();
                    sr.CreatedDate = DateTime.Now;

                    sampleResults.Add(sr);
                }

                if (float.Parse(e.Feb) > 0)
                {
                    sr = new SampleResult();
                    //CollectionMonth = DateTime.Parse("01" + GetPropertyName(() => e.Feb).ToString() + DateTime.Parse(e.ResultDate).Year);
                    CollectionMonth = e.ResultDate;

                    query = "SET DATEFORMAT DMY; Exec sprGetDataCollectionMasterID @StrategicLocation = '" + e.StrategicLocations.Trim() + "', @PurposeOfSample = '" + e.PurposeOfSample.Trim() + "', @TypeOfSample = '" + e.TypeOfSample.Trim() + "', @LabCode = '0', @CollectionDate = '" + CollectionMonth + "', @UploadUser = '" + User.Identity.GetUserId() + "';";
                    DataCollectionMasterID = Int32.Parse(_exec.SelectSingleQuery(query, ref _msg));

                    sr.DataCollectionMasterID = DataCollectionMasterID;
                    sr.ParameterName = e.Parameters;
                    sr.Result = e.Feb;
                    sr.CollectionMonth = CollectionMonth;

                    sr.Remarks = "Bulk data imported from excel";
                    sr.IsActive = true;
                    sr.IsDelete = false;
                    sr.CreatedBy = User.Identity.GetUserId();
                    sr.CreatedDate = DateTime.Now;

                    sampleResults.Add(sr);
                }

                if (float.Parse(e.Mar) > 0)
                {
                    sr = new SampleResult();
                    //CollectionMonth = DateTime.Parse("01" + GetPropertyName(() => e.Mar).ToString() + DateTime.Parse(e.ResultDate).Year);
                    CollectionMonth = e.ResultDate;

                    query = "SET DATEFORMAT DMY; Exec sprGetDataCollectionMasterID @StrategicLocation = '" + e.StrategicLocations.Trim() + "', @PurposeOfSample = '" + e.PurposeOfSample.Trim() + "', @TypeOfSample = '" + e.TypeOfSample.Trim() + "', @LabCode = '0', @CollectionDate = '" + CollectionMonth + "', @UploadUser = '" + User.Identity.GetUserId() + "';";
                    DataCollectionMasterID = Int32.Parse(_exec.SelectSingleQuery(query, ref _msg));

                    sr.DataCollectionMasterID = DataCollectionMasterID;
                    sr.ParameterName = e.Parameters;
                    sr.Result = e.Mar;
                    sr.CollectionMonth = CollectionMonth;

                    sr.Remarks = "Bulk data imported from excel";
                    sr.IsActive = true;
                    sr.IsDelete = false;
                    sr.CreatedBy = User.Identity.GetUserId();
                    sr.CreatedDate = DateTime.Now;

                    sampleResults.Add(sr);
                }

                if (float.Parse(e.Apr) > 0)
                {
                    sr = new SampleResult();
                    //CollectionMonth = DateTime.Parse("01" + GetPropertyName(() => e.Apr).ToString() + DateTime.Parse(e.ResultDate).Year);
                    CollectionMonth = e.ResultDate;

                    query = "SET DATEFORMAT DMY; Exec sprGetDataCollectionMasterID @StrategicLocation = '" + e.StrategicLocations.Trim() + "', @PurposeOfSample = '" + e.PurposeOfSample.Trim() + "', @TypeOfSample = '" + e.TypeOfSample.Trim() + "', @LabCode = '0', @CollectionDate = '" + CollectionMonth + "', @UploadUser = '" + User.Identity.GetUserId() + "';";
                    DataCollectionMasterID = Int32.Parse(_exec.SelectSingleQuery(query, ref _msg));

                    sr.DataCollectionMasterID = DataCollectionMasterID;
                    sr.ParameterName = e.Parameters;
                    sr.Result = e.Apr;
                    sr.CollectionMonth = CollectionMonth;

                    sr.Remarks = "Bulk data imported from excel";
                    sr.IsActive = true;
                    sr.IsDelete = false;
                    sr.CreatedBy = User.Identity.GetUserId();
                    sr.CreatedDate = DateTime.Now;

                    sampleResults.Add(sr);
                }

                if (float.Parse(e.May) > 0)
                {
                    sr = new SampleResult();
                    //CollectionMonth = DateTime.Parse("01" + GetPropertyName(() => e.May).ToString() + DateTime.Parse(e.ResultDate).Year);
                    CollectionMonth = e.ResultDate;

                    query = "SET DATEFORMAT DMY; Exec sprGetDataCollectionMasterID @StrategicLocation = '" + e.StrategicLocations.Trim() + "', @PurposeOfSample = '" + e.PurposeOfSample.Trim() + "', @TypeOfSample = '" + e.TypeOfSample.Trim() + "', @LabCode = '0', @CollectionDate = '" + CollectionMonth + "', @UploadUser = '" + User.Identity.GetUserId() + "';";
                    DataCollectionMasterID = Int32.Parse(_exec.SelectSingleQuery(query, ref _msg));

                    sr.DataCollectionMasterID = DataCollectionMasterID;
                    sr.ParameterName = e.Parameters;
                    sr.Result = e.May;
                    sr.CollectionMonth = CollectionMonth;

                    sr.Remarks = "Bulk data imported from excel";
                    sr.IsActive = true;
                    sr.IsDelete = false;
                    sr.CreatedBy = User.Identity.GetUserId();
                    sr.CreatedDate = DateTime.Now;

                    sampleResults.Add(sr);
                }

                if (float.Parse(e.Jun) > 0)
                {
                    sr = new SampleResult();
                    //CollectionMonth = DateTime.Parse("01" + GetPropertyName(() => e.Jun).ToString() + DateTime.Parse(e.ResultDate).Year);
                    CollectionMonth = e.ResultDate;

                    query = "SET DATEFORMAT DMY; Exec sprGetDataCollectionMasterID @StrategicLocation = '" + e.StrategicLocations.Trim() + "', @PurposeOfSample = '" + e.PurposeOfSample.Trim() + "', @TypeOfSample = '" + e.TypeOfSample.Trim() + "', @LabCode = '0', @CollectionDate = '" + CollectionMonth + "', @UploadUser = '" + User.Identity.GetUserId() + "';";
                    DataCollectionMasterID = Int32.Parse(_exec.SelectSingleQuery(query, ref _msg));

                    sr.DataCollectionMasterID = DataCollectionMasterID;
                    sr.ParameterName = e.Parameters;
                    sr.Result = e.Jun;
                    sr.CollectionMonth = CollectionMonth;

                    sr.Remarks = "Bulk data imported from excel";
                    sr.IsActive = true;
                    sr.IsDelete = false;
                    sr.CreatedBy = User.Identity.GetUserId();
                    sr.CreatedDate = DateTime.Now;

                    sampleResults.Add(sr);
                }

                if (float.Parse(e.Jul) > 0)
                {
                    sr = new SampleResult();
                    //CollectionMonth = DateTime.Parse("01" + GetPropertyName(() => e.Jul).ToString() + DateTime.Parse(e.ResultDate).Year);
                    CollectionMonth = e.ResultDate;

                    query = "SET DATEFORMAT DMY; Exec sprGetDataCollectionMasterID @StrategicLocation = '" + e.StrategicLocations.Trim() + "', @PurposeOfSample = '" + e.PurposeOfSample.Trim() + "', @TypeOfSample = '" + e.TypeOfSample.Trim() + "', @LabCode = '0', @CollectionDate = '" + CollectionMonth + "', @UploadUser = '" + User.Identity.GetUserId() + "';";
                    DataCollectionMasterID = Int32.Parse(_exec.SelectSingleQuery(query, ref _msg));

                    sr.DataCollectionMasterID = DataCollectionMasterID;
                    sr.ParameterName = e.Parameters;
                    sr.Result = e.Jul;
                    sr.CollectionMonth = CollectionMonth;

                    sr.Remarks = "Bulk data imported from excel";
                    sr.IsActive = true;
                    sr.IsDelete = false;
                    sr.CreatedBy = User.Identity.GetUserId();
                    sr.CreatedDate = DateTime.Now;

                    sampleResults.Add(sr);
                }

                if (float.Parse(e.Aug) > 0)
                {
                    sr = new SampleResult();
                    //CollectionMonth = DateTime.Parse("01" + GetPropertyName(() => e.Aug).ToString() + DateTime.Parse(e.ResultDate).Year);
                    CollectionMonth = e.ResultDate;

                    query = "SET DATEFORMAT DMY; Exec sprGetDataCollectionMasterID @StrategicLocation = '" + e.StrategicLocations.Trim() + "', @PurposeOfSample = '" + e.PurposeOfSample.Trim() + "', @TypeOfSample = '" + e.TypeOfSample.Trim() + "', @LabCode = '0', @CollectionDate = '" + CollectionMonth + "', @UploadUser = '" + User.Identity.GetUserId() + "';";
                    DataCollectionMasterID = Int32.Parse(_exec.SelectSingleQuery(query, ref _msg));

                    sr.DataCollectionMasterID = DataCollectionMasterID;
                    sr.ParameterName = e.Parameters;
                    sr.Result = e.Aug;
                    sr.CollectionMonth = CollectionMonth;

                    sr.Remarks = "Bulk data imported from excel";
                    sr.IsActive = true;
                    sr.IsDelete = false;
                    sr.CreatedBy = User.Identity.GetUserId();
                    sr.CreatedDate = DateTime.Now;

                    sampleResults.Add(sr);
                }

                if (float.Parse(e.Sep) > 0)
                {
                    sr = new SampleResult();
                    //CollectionMonth = DateTime.Parse("01" + GetPropertyName(() => e.Sep).ToString() + DateTime.Parse(e.ResultDate).Year);
                    CollectionMonth = e.ResultDate;

                    query = "SET DATEFORMAT DMY; Exec sprGetDataCollectionMasterID @StrategicLocation = '" + e.StrategicLocations.Trim() + "', @PurposeOfSample = '" + e.PurposeOfSample.Trim() + "', @TypeOfSample = '" + e.TypeOfSample.Trim() + "', @LabCode = '0', @CollectionDate = '" + CollectionMonth + "', @UploadUser = '" + User.Identity.GetUserId() + "';";
                    DataCollectionMasterID = Int32.Parse(_exec.SelectSingleQuery(query, ref _msg));

                    sr.DataCollectionMasterID = DataCollectionMasterID;
                    sr.ParameterName = e.Parameters;
                    sr.Result = e.Sep;
                    sr.CollectionMonth = CollectionMonth;

                    sr.Remarks = "Bulk data imported from excel";
                    sr.IsActive = true;
                    sr.IsDelete = false;
                    sr.CreatedBy = User.Identity.GetUserId();
                    sr.CreatedDate = DateTime.Now;

                    sampleResults.Add(sr);
                }

                if (float.Parse(e.Oct) > 0)
                {
                    sr = new SampleResult();
                    //CollectionMonth = DateTime.Parse("01" + GetPropertyName(() => e.Oct).ToString() + DateTime.Parse(e.ResultDate).Year);
                    CollectionMonth = e.ResultDate;

                    query = "SET DATEFORMAT DMY; Exec sprGetDataCollectionMasterID @StrategicLocation = '" + e.StrategicLocations.Trim() + "', @PurposeOfSample = '" + e.PurposeOfSample.Trim() + "', @TypeOfSample = '" + e.TypeOfSample.Trim() + "', @LabCode = '0', @CollectionDate = '" + CollectionMonth + "', @UploadUser = '" + User.Identity.GetUserId() + "';";
                    DataCollectionMasterID = Int32.Parse(_exec.SelectSingleQuery(query, ref _msg));

                    sr.DataCollectionMasterID = DataCollectionMasterID;
                    sr.ParameterName = e.Parameters;
                    sr.Result = e.Oct;
                    sr.CollectionMonth = CollectionMonth;

                    sr.Remarks = "Bulk data imported from excel";
                    sr.IsActive = true;
                    sr.IsDelete = false;
                    sr.CreatedBy = User.Identity.GetUserId();
                    sr.CreatedDate = DateTime.Now;

                    sampleResults.Add(sr);
                }

                if (float.Parse(e.Nov) > 0)
                {
                    sr = new SampleResult();
                    //CollectionMonth = DateTime.Parse("01" + GetPropertyName(() => e.Nov).ToString() + DateTime.Parse(e.ResultDate).Year);
                    CollectionMonth = e.ResultDate;

                    query = "SET DATEFORMAT DMY; Exec sprGetDataCollectionMasterID @StrategicLocation = '" + e.StrategicLocations.Trim() + "', @PurposeOfSample = '" + e.PurposeOfSample.Trim() + "', @TypeOfSample = '" + e.TypeOfSample.Trim() + "', @LabCode = '0', @CollectionDate = '" + CollectionMonth + "', @UploadUser = '" + User.Identity.GetUserId() + "';";
                    DataCollectionMasterID = Int32.Parse(_exec.SelectSingleQuery(query, ref _msg));

                    sr.DataCollectionMasterID = DataCollectionMasterID;
                    sr.ParameterName = e.Parameters;
                    sr.Result = e.Nov;
                    sr.CollectionMonth = CollectionMonth;

                    sr.Remarks = "Bulk data imported from excel";
                    sr.IsActive = true;
                    sr.IsDelete = false;
                    sr.CreatedBy = User.Identity.GetUserId();
                    sr.CreatedDate = DateTime.Now;

                    sampleResults.Add(sr);
                }

                if (float.Parse(e.Dec) > 0)
                {
                    sr = new SampleResult();
                    //CollectionMonth = DateTime.Parse("01" + GetPropertyName(() => e.Dec).ToString() + DateTime.Parse(e.ResultDate).Year);
                    CollectionMonth = e.ResultDate;

                    query = "SET DATEFORMAT DMY; Exec sprGetDataCollectionMasterID @StrategicLocation = '" + e.StrategicLocations.Trim() + "', @PurposeOfSample = '" + e.PurposeOfSample.Trim() + "', @TypeOfSample = '" + e.TypeOfSample.Trim() + "', @LabCode = '0', @CollectionDate = '" + CollectionMonth + "', @UploadUser = '" + User.Identity.GetUserId() + "';";
                    DataCollectionMasterID = Int32.Parse(_exec.SelectSingleQuery(query, ref _msg));

                    sr.DataCollectionMasterID = DataCollectionMasterID;
                    sr.ParameterName = e.Parameters;
                    sr.Result = e.Dec;
                    sr.CollectionMonth = CollectionMonth;

                    sr.Remarks = "Bulk data imported from excel";
                    sr.IsActive = true;
                    sr.IsDelete = false;
                    sr.CreatedBy = User.Identity.GetUserId();
                    sr.CreatedDate = DateTime.Now;

                    sampleResults.Add(sr);
                }
                #endregion
            };

            try
            {
                _db.SampleResults.AddRange(sampleResults);
                await _db.SaveChangesAsync();
                result = true;
            }
            catch (Exception ex)
            {
                string message = ExtractInnerException(ex);
                result = false;
            }

            return Json(result, JsonRequestBehavior.AllowGet);
        }
        #endregion

        #region Improve
        // GET: /DataCollection/Edit/5
        public ActionResult Improve(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var datacollection = new DataCollection();
            datacollection.DataCollectionMaster = _db.DataCollectionMaster.Find(id);
            if (datacollection.DataCollectionMaster == null)
            {
                return HttpNotFound();
            }

            datacollection.DataCollectionMaster.SampleCollectionDateTime = datacollection.DataCollectionMaster.SampleCollectionDateTime;

            //ViewBag.CustomID = "WQMS-###-" + DateTime.Now.ToString("ddMMyy") + "-" + _db.DataCollectionMaster.Count().ToString().PadLeft(5, '0');
            //ViewBag.StrategicLocationID = new SelectList(_db.StrategicLocations.Where(w => w.IsActive == true && w.IsDelete == false).OrderBy(o => o.StrategicLocationName), "StrategicLocationID", "StrategicLocationName");
            //ViewBag.WaterQualityParamID = new SelectList(_db.WaterQualityParams.Where(w => w.IsActive == true && w.IsDelete == false).GroupBy(g => g.ParameterName).Select(s => s.FirstOrDefault()).OrderBy(o => o.ParameterName), "WaterQualityParamID", "ParameterName");

            //ViewBag.Rivers = new SelectList(_db.Rivers.Where(w => w.IsActive == true && w.IsDelete == false).OrderBy(g => g.RiverName), "RiverID", "RiverName");
            //ViewBag.Parameters = new SelectList(_db.WaterQualityParams.Where(w => w.IsActive == true && w.IsDelete == false).GroupBy(g => g.ParameterName).Select(s => s.FirstOrDefault()).OrderBy(o => o.ParameterName), "WaterQualityParamID", "ParameterName");

            //ViewBag.SampleCollectionDateTime = "";
            //ViewBag.ReceivingDateTimeAtLab = "";

            //////////////////////////////////////////////////////
            ViewBag.DataCollectionMasterID = datacollection.DataCollectionMaster.DataCollectionMasterID;
            ViewBag.CustomID = datacollection.DataCollectionMaster.CustomID;
            ViewBag.StrategicLocationID = new SelectList(_db.StrategicLocations.Where(w => w.IsActive == true && w.IsDelete == false).OrderBy(o => o.StrategicLocationName), "StrategicLocationID", "StrategicLocationName", datacollection.DataCollectionMaster.StrategicLocationID);
            ViewBag.WaterQualityParamID = new SelectList(_db.WaterQualityParams.Where(w => w.IsActive == true && w.IsDelete == false).GroupBy(g => g.ParameterName).Select(s => s.FirstOrDefault()).OrderBy(o => o.ParameterName), "WaterQualityParamID", "ParameterName");

            ViewBag.PurposeOfSampleID = new SelectList(_db.PurposeOfSamples.Where(w => w.IsActive == true && w.IsDelete == false).OrderBy(o => o.PurposeOfSampleName), "PurposeOfSampleID", "PurposeOfSampleName", datacollection.DataCollectionMaster.PurposeOfSampleID);
            ViewBag.TypeOfSampleID = new SelectList(_db.WaterQualityCategories.Where(w => w.IsActive == true && w.IsDelete == false).OrderBy(o => o.CategoryName), "WaterQualityCategoryID", "CategoryName", datacollection.DataCollectionMaster.TypeOfSampleID);

            var WeatherConditionList = from WeatherCondition s in Enum.GetValues(typeof(WeatherCondition)) select new { ID = s, Name = s.ToDescription() };
            ViewBag.WeatherCondition = new SelectList(WeatherConditionList, "ID", "Name", datacollection.DataCollectionMaster.WeatherCondition);

            datacollection.SampleResults = _db.SampleResults.Where(w => w.DataCollectionMasterID == id).ToList();
            datacollection.HydrologicalInformation = _db.HydrologicalInformations.FirstOrDefault(w => w.DataCollectionMasterID == datacollection.DataCollectionMaster.DataCollectionMasterID && w.IsActive == true && w.IsDelete == false);

            return View(datacollection);
        }

        // POST: /DataCollection/Edit/5
        [HttpPost]
        [ValidateInput(false)]
        //[ValidateAntiForgeryToken]
        public async Task<ActionResult> Improve(DataCollection datacollection)
        {
            var dataCollectionMasterId = datacollection.DataCollectionMaster.DataCollectionMasterID;

            try
            {
                #region Data Collection Master: Update
                if (dataCollectionMasterId != 0)
                {
                    datacollection.DataCollectionMaster.SurveyDate = datacollection.DataCollectionMaster.SampleCollectionDateTime.Substring(0, 10).Trim();
                    //datacollection.DataCollectionMaster.SurveyDate = datacollection.DataCollectionMaster.SampleCollectionDateTime;
                    datacollection.DataCollectionMaster.UpdateBy = User.Identity.GetUserId();
                    datacollection.DataCollectionMaster.UpdatedDate = DateTime.Now;
                    datacollection.DataCollectionMaster.IsActive = true;
                    datacollection.DataCollectionMaster.IsDelete = false;

                    try
                    {
                        _db.Entry(datacollection.DataCollectionMaster).State = EntityState.Modified;
                        var x = await _db.SaveChangesAsync();

                        if (x > 0)
                        {
                            TempData["Message"] = ShowMessage(Sign.Success, OperationMessage.Update.ToDescription());
                        }
                        else
                        {
                            TempData["Message"] = ShowMessage(Sign.Error, OperationMessage.NotUpdate.ToDescription());
                        }
                    }
                    catch (Exception ex)
                    {
                        var message = ExtractInnerException(ex);
                        TempData["Message"] = ShowMessage(Sign.Danger, message);
                    }

                    ViewBag.DataCollectionMasterID = datacollection.DataCollectionMaster.DataCollectionMasterID;
                }
                #endregion
            }
            catch (Exception ex)
            {
                var message = ExtractInnerException(ex);
                TempData["Message"] = ShowMessage(Sign.Danger, message);
            }

            ViewBag.CustomID = datacollection.DataCollectionMaster.CustomID;
            ViewBag.StrategicLocationID = new SelectList(_db.StrategicLocations.Where(w => w.IsActive == true && w.IsDelete == false).OrderBy(o => o.StrategicLocationName), "StrategicLocationID", "StrategicLocationName", datacollection.DataCollectionMaster.StrategicLocationID);
            ViewBag.WaterQualityParamID = new SelectList(_db.WaterQualityParams.Where(w => w.IsActive == true && w.IsDelete == false).GroupBy(g => g.ParameterName).Select(s => s.FirstOrDefault()).OrderBy(o => o.ParameterName), "WaterQualityParamID", "ParameterName");

            ViewBag.PurposeOfSampleID = new SelectList(_db.PurposeOfSamples.Where(w => w.IsActive == true && w.IsDelete == false).OrderBy(o => o.PurposeOfSampleName), "PurposeOfSampleID", "PurposeOfSampleName", datacollection.DataCollectionMaster.PurposeOfSampleID);
            ViewBag.TypeOfSampleID = new SelectList(_db.WaterQualityCategories.Where(w => w.IsActive == true && w.IsDelete == false).OrderBy(o => o.CategoryName), "WaterQualityCategoryID", "CategoryName", datacollection.DataCollectionMaster.TypeOfSampleID);
            var WeatherConditionList = from WeatherCondition s in Enum.GetValues(typeof(WeatherCondition)) select new { ID = s, Name = s.ToDescription() };
            ViewBag.WeatherCondition = new SelectList(WeatherConditionList, "ID", "Name", datacollection.DataCollectionMaster.WeatherCondition);

            datacollection.SampleResults = _db.SampleResults.Where(w => w.DataCollectionMasterID == datacollection.DataCollectionMaster.DataCollectionMasterID).ToList();
            datacollection.HydrologicalInformation = _db.HydrologicalInformations.FirstOrDefault(w => w.DataCollectionMasterID == datacollection.DataCollectionMaster.DataCollectionMasterID && w.IsActive == true && w.IsDelete == false);

            return View(datacollection);
        }
        #endregion

        #region Delete
        // GET: DataCollection/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            DataCollectionMaster dataCollectionMaster = await _db.DataCollectionMaster.FindAsync(id);

            if (dataCollectionMaster == null)
            {
                return HttpNotFound();
            }

            return View(dataCollectionMaster);
        }

        // POST: WaterQualityParam/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            DataCollectionMaster dataCollectionMaster = await _db.DataCollectionMaster.FindAsync(id);

            if (dataCollectionMaster != null)
            {
                dataCollectionMaster.IsDelete = true;
                dataCollectionMaster.DeletedBy = User.Identity.GetUserId();
                dataCollectionMaster.DeletedDate = DateTime.Now;

                try
                {
                    _db.Entry(dataCollectionMaster).State = EntityState.Modified;
                    _db.Entry(dataCollectionMaster).State = EntityState.Modified;
                    await _db.SaveChangesAsync();

                    TempData["Message"] = ShowMessage(Sign.Success, OperationMessage.Delete.ToDescription());
                    return RedirectToAction("Index");
                }
                catch (Exception ex)
                {

                    string message = string.IsNullOrEmpty(ex.Message) ? ex.InnerException.Message : ex.Message;
                    TempData["Message"] = ShowMessage(Sign.Danger, message);
                    return View(dataCollectionMaster);
                }
            }

            return RedirectToAction("Index");
        }
        #endregion

        #region Responder Information
        [HttpPost]
        public async Task<ActionResult> AddResponder(Responder responder)
        {
            bool result = false;

            if (responder != null)
            {
                responder.CreatedBy = User.Identity.GetUserId();
                responder.CreatedDate = DateTime.Now;
                responder.IsActive = true;
                responder.IsDelete = false;

                try
                {
                    _db.Responders.Add(responder);
                    await _db.SaveChangesAsync();
                    result = true;
                }
                catch (Exception ex)
                {
                    string message = ExtractInnerException(ex);
                    result = false;
                }
            }

            return Json(result);
        }

        [HttpPost]
        public async Task<ActionResult> PutResponder(Responder responder)
        {
            bool result = false;

            if (responder != null)
            {
                try
                {
                    responder.IsActive = true;
                    responder.IsDelete = false;
                    responder.UpdateBy = User.Identity.GetUserId();
                    responder.UpdatedDate = DateTime.Now;

                    _db.Entry(responder).State = EntityState.Modified;
                    await _db.SaveChangesAsync();
                    result = true;
                }
                catch (Exception ex)
                {
                    result = false;
                }
            }

            return Json(result, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public ActionResult GetResponders(int id)
        {
            List<Responder> responders = new List<Responder>();
            try
            {
                responders = _db.Responders.Where(w => w.DataCollectionMasterID == id && w.IsActive == true && w.IsDelete == false).ToList();
            }
            catch (Exception ex)
            {
                responders = null;
            }

            return Json(responders, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public ActionResult GetSelectedResponder(int id)
        {
            Responder responder = new Responder();

            if (id != null)
            {
                try
                {
                    responder = _db.Responders.Where(w => w.ResponderID == id && w.IsActive == true && w.IsDelete == false).FirstOrDefault();
                }
                catch (Exception ex)
                {
                    responder = null;
                }
            }

            return Json(responder, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public async Task<ActionResult> DeleteSelectedResponder(int id)
        {
            bool result = false;
            Responder responder = _db.Responders.Find(id);

            if (responder != null)
            {
                try
                {
                    responder.IsActive = true;
                    responder.IsDelete = true;
                    responder.DeletedBy = User.Identity.GetUserId();
                    responder.DeletedDate = DateTime.Now;

                    _db.Entry(responder).State = EntityState.Modified;
                    await _db.SaveChangesAsync();
                    result = true;
                }
                catch (Exception ex)
                {
                    result = false;
                }
            }

            return Json(result, JsonRequestBehavior.AllowGet);
        }
        #endregion

        #region Interviewer Information
        [HttpPost]
        public async Task<ActionResult> AddInterviewer(Interviewer interviewer)
        {
            bool result = false;

            if (interviewer != null)
            {
                interviewer.CreatedBy = User.Identity.GetUserId();
                interviewer.CreatedDate = DateTime.Now;
                interviewer.IsActive = true;
                interviewer.IsDelete = false;

                try
                {
                    _db.Interviewers.Add(interviewer);
                    await _db.SaveChangesAsync();
                    result = true;
                }
                catch (Exception ex)
                {
                    string message = ExtractInnerException(ex);
                    result = false;
                }
            }

            return Json(result);
        }

        [HttpPost]
        public async Task<ActionResult> PutInterviewer(Interviewer interviewer)
        {
            bool result = false;

            if (interviewer != null)
            {
                try
                {
                    interviewer.IsActive = true;
                    interviewer.IsDelete = false;
                    interviewer.UpdateBy = User.Identity.GetUserId();
                    interviewer.UpdatedDate = DateTime.Now;

                    _db.Entry(interviewer).State = EntityState.Modified;
                    await _db.SaveChangesAsync();
                    result = true;
                }
                catch (Exception ex)
                {
                    result = false;
                }
            }

            return Json(result, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public ActionResult GetInterviewers(int id)
        {
            List<Interviewer> interviewers = new List<Interviewer>();
            try
            {
                interviewers = _db.Interviewers.Where(w => w.DataCollectionMasterID == id && w.IsActive == true && w.IsDelete == false).ToList();
            }
            catch (Exception ex)
            {
                interviewers = null;
            }

            return Json(interviewers, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public ActionResult GetSelectedInterviewer(int id)
        {
            Interviewer interviewer = new Interviewer();

            if (id != null)
            {
                try
                {
                    interviewer = _db.Interviewers.Where(w => w.InterviewerID == id && w.IsActive == true && w.IsDelete == false).FirstOrDefault();
                }
                catch (Exception ex)
                {
                    interviewer = null;
                }
            }

            return Json(interviewer, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public async Task<ActionResult> DeleteSelectedInterviewer(int id)
        {
            bool result = false;
            Interviewer interviewer = _db.Interviewers.Find(id);

            if (interviewer != null)
            {
                try
                {
                    interviewer.IsActive = true;
                    interviewer.IsDelete = true;
                    interviewer.DeletedBy = User.Identity.GetUserId();
                    interviewer.DeletedDate = DateTime.Now;

                    _db.Entry(interviewer).State = EntityState.Modified;
                    await _db.SaveChangesAsync();
                    result = true;
                }
                catch (Exception ex)
                {
                    result = false;
                }
            }

            return Json(result, JsonRequestBehavior.AllowGet);
        }
        #endregion

        #region Water Quality Parameter Operations
        [HttpPost]
        public async Task<ActionResult> AddWaterQualityParameter(InlineWaterQualityParameter wqpd)
        {
            InlineWQPResult result = new InlineWQPResult();

            if (wqpd != null)
            {
                var dataCollectionMasterId = wqpd.DataCollectionMasterID;

                //List<SampleResult> preData = _db.SampleResults.Where(i => i.DataCollectionMasterID == dataCollectionMasterId).ToList();
                //foreach (var data in preData)
                //{
                //    data.UpdateBy = User.Identity.GetUserId();
                //    data.UpdatedDate = DateTime.Now;
                //    data.IsActive = false;
                //    data.IsDelete = false;

                //    _db.SaveChanges();
                //}

                #region New Sample Result :: Save
                if (dataCollectionMasterId != 0)
                {
                    using (var dbContextTransaction = _db.Database.BeginTransaction())
                    {
                        List<SampleResult> _sampleResults = new List<SampleResult>();
                        foreach (ParameterResult pr in wqpd.ParameterResults)
                        {
                            if (!string.IsNullOrEmpty(pr.Result))
                            {
                                SampleResult _existSampleResult = _db.SampleResults.Where(w => w.DataCollectionMasterID == dataCollectionMasterId && w.ParameterName == pr.ParameterName).FirstOrDefault();

                                if (_existSampleResult == null)
                                {
                                    SampleResult _sampleResult = new SampleResult
                                    {
                                        DataCollectionMasterID = dataCollectionMasterId,
                                        ParameterName = pr.ParameterName,
                                        Result = pr.Result,
                                        Remarks = wqpd.Remarks,
                                        CollectionMonth = wqpd.CollectionMonth,

                                        CreatedBy = User.Identity.GetUserId(),
                                        CreatedDate = DateTime.Now,
                                        IsActive = true,
                                        IsDelete = false
                                    };

                                    _sampleResults.Add(_sampleResult);
                                }
                                else
                                {
                                    _existSampleResult.Result = pr.Result;
                                    _existSampleResult.UpdateBy = User.Identity.GetUserId();
                                    _existSampleResult.UpdatedDate = DateTime.Now;

                                    _db.Entry(_existSampleResult).State = EntityState.Modified;
                                }
                            }
                        }

                        try
                        {
                            if (_sampleResults.Count > 0)
                            {
                                _db.SampleResults.AddRange(_sampleResults);
                            }

                            int x = await _db.SaveChangesAsync();

                            if (x > 0)
                            {
                                dbContextTransaction.Commit();

                                result.DCMID = dataCollectionMasterId;
                                result.RESULT = (x > 0) ? true : false;
                                result.EXCEPTION = "";
                            }
                            else
                            {
                                dbContextTransaction.Rollback();

                                string message = "Saving process has been rollbacked!";
                                result.DCMID = 0;
                                result.RESULT = false;
                                result.EXCEPTION = message;
                            }
                        }
                        catch (Exception ex)
                        {
                            dbContextTransaction.Rollback();

                            string message = ExtractInnerException(ex);
                            result.DCMID = 0;
                            result.RESULT = false;
                            result.EXCEPTION = ExtractInnerException(ex);
                        }
                    }
                }
                else
                {
                    result.DCMID = 0;
                    result.RESULT = false;
                    result.EXCEPTION = "Master data not exist.<br />Please add a new master data and try again later.";
                }
                #endregion
            }

            return Json(result, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public async Task<ActionResult> AddWQPResult(SampleResult sampleResult)
        {
            bool result = false;

            if (sampleResult != null)
            {
                sampleResult.CreatedBy = User.Identity.GetUserId();
                sampleResult.CreatedDate = DateTime.Now;
                sampleResult.IsActive = true;
                sampleResult.IsDelete = false;

                try
                {
                    _db.SampleResults.Add(sampleResult);
                    int x = await _db.SaveChangesAsync();

                    if (x > 0)
                    {
                        result = true;
                    }
                    else
                    {
                        result = false;
                    }
                }
                catch (Exception ex)
                {
                    string message = ExtractInnerException(ex);
                    result = false;
                }
            }

            return Json(result);
        }

        [HttpPost]
        public async Task<ActionResult> PutWQPResult(SampleResult sampleResult)
        {
            bool result = false;

            if (sampleResult != null)
            {
                try
                {
                    sampleResult.IsActive = true;
                    sampleResult.IsDelete = false;
                    sampleResult.UpdateBy = User.Identity.GetUserId();
                    sampleResult.UpdatedDate = DateTime.Now;

                    _db.Entry(sampleResult).State = EntityState.Modified;
                    int x = await _db.SaveChangesAsync();

                    if (x > 0)
                    {
                        result = true;
                    }
                    else
                    {
                        result = false;
                    }
                }
                catch (Exception ex)
                {
                    result = false;
                }
            }

            return Json(result, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public ActionResult GetWQPResult(int id)
        {
            List<SampleOutcome> sampleResults = new List<SampleOutcome>();
            try
            {
                sampleResults = _cc.GetSampleOutcomes(id);
            }
            catch (Exception ex)
            {
                sampleResults = null;
            }

            return Json(sampleResults, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public ActionResult GetSelectedWQP(int id)
        {
            SampleResult sampleResult = new SampleResult();

            if (id != null)
            {
                try
                {
                    sampleResult = _db.SampleResults.Where(w => w.SampleResultID == id && w.IsActive == true && w.IsDelete == false).FirstOrDefault();
                }
                catch (Exception ex)
                {
                    sampleResult = null;
                }
            }

            return Json(sampleResult, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public async Task<ActionResult> DeleteSelectedWQP(int id)
        {
            bool result = false;
            SampleResult sampleResult = _db.SampleResults.Find(id);

            if (sampleResult != null)
            {
                try
                {
                    sampleResult.IsActive = true;
                    sampleResult.IsDelete = true;
                    sampleResult.DeletedBy = User.Identity.GetUserId();
                    sampleResult.DeletedDate = DateTime.Now;

                    _db.Entry(sampleResult).State = EntityState.Modified;
                    await _db.SaveChangesAsync();
                    result = true;
                }
                catch (Exception ex)
                {
                    result = false;
                }
            }

            return Json(result, JsonRequestBehavior.AllowGet);
        }
        #endregion

        #region Hydrological Information
        [HttpPost]
        public async Task<ActionResult> AddHydrologicalInfo(HydrologicalInformation hydrologicalInformation)
        {
            var result = false;

            if (hydrologicalInformation == null) return Json(false);

            hydrologicalInformation.CreatedBy = User.Identity.GetUserId();
            hydrologicalInformation.CreatedDate = DateTime.Now;
            hydrologicalInformation.IsActive = true;
            hydrologicalInformation.IsDelete = false;

            try
            {
                _db.HydrologicalInformations.Add(hydrologicalInformation);
                var x = await _db.SaveChangesAsync();
                result = x > 0 ? true : false;
            }
            catch (Exception ex)
            {
                var message = ExtractInnerException(ex);
                result = false;
            }

            return Json(result);
        }

        [HttpPost]
        public async Task<ActionResult> PutHydrologicalInfo(HydrologicalInformation hydrologicalInformation)
        {
            var result = false;

            if (hydrologicalInformation == null) return Json(false, JsonRequestBehavior.AllowGet);

            try
            {
                var hi = _db.HydrologicalInformations.FirstOrDefault(w => w.DataCollectionMasterID == hydrologicalInformation.DataCollectionMasterID);

                if (hi != null)
                {
                    hi.DepthOfWater = hydrologicalInformation.DepthOfWater;
                    hi.RiverFlowCondition = hydrologicalInformation.RiverFlowCondition;
                    hi.Other = hydrologicalInformation.Other;
                    hi.CreatedBy = hi.CreatedBy;
                    hi.CreatedDate = hi.CreatedDate;
                    hi.IsActive = hi.IsActive;
                    hi.IsDelete = hi.IsDelete;

                    hi.UpdateBy = User.Identity.GetUserId();
                    hi.UpdatedDate = DateTime.Now;
                }

                _db.Entry(hi).State = EntityState.Modified;
                var x = await _db.SaveChangesAsync();

                result = (x > 0) ? true : false;
            }
            catch (Exception ex)
            {
                var message = ExtractInnerException(ex);
                result = false;
            }

            return Json(result, JsonRequestBehavior.AllowGet);
        }
        #endregion

        #region Dispose
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _db.Dispose();
            }
            base.Dispose(disposing);
        }
        #endregion

        #region Get Property Name
        static string GetPropertyName<T>(Expression<Func<T>> exp)
        {
            return (((MemberExpression)(exp.Body)).Member).Name;
        }

        // requires explicit specification of both object type and property type
        static string GetPropertyName<TObject, TResult>(Expression<Func<TObject, TResult>> exp)
        {
            // extract property name
            return (((MemberExpression)(exp.Body)).Member).Name;
        }

        // requires explicit specification of object type
        static string GetPropertyName<TObject>(Expression<Func<TObject, object>> exp)
        {
            var body = exp.Body;
            var convertExpression = body as UnaryExpression;
            if (convertExpression != null)
            {
                if (convertExpression.NodeType != ExpressionType.Convert)
                {
                    throw new ArgumentException("Invalid property expression.", "exp");
                }
                body = convertExpression.Operand;
            }
            return ((MemberExpression)body).Member.Name;
        }
        #endregion
    }
}