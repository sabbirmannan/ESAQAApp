using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using DOF003.Models;
using DOF003.Helpers;
using System.Web.Http.Results;
using DOF003.ViewModels;
using System.IO;
using Microsoft.VisualBasic.FileIO;

namespace DOF003.Controllers
{
    [AccessDeniedAuthorize]
    public class StrategicLocationController : BaseController
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: /StrategicLocation/
        public ActionResult Index()
        {
            ViewBag.Count = db.StrategicLocations.Where(w => w.IsActive == true && w.IsDelete == false).Count();
            return View();
        }

        // GET: /StrategicLocation/
        public async Task<ActionResult> List()
        {
            var strategiclocations = db.StrategicLocations.Where(w => w.IsActive == true && w.IsDelete == false).Include(s => s.District).Include(s => s.Division).Include(s => s.Upazilla).Include(s => s.River);
            return View(await strategiclocations.ToListAsync());
        }

        // GET: /StrategicLocation/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            StrategicLocation strategiclocation = await db.StrategicLocations.FindAsync(id);

            if (strategiclocation == null)
            {
                return HttpNotFound();
            }

            return View(strategiclocation);
        }

        // GET: /StrategicLocation/Import
        public ActionResult Import()
        {
            return View();
        }

        // GET: /StrategicLocation/Import
        [HttpPost]
        public ActionResult Import(List<StrategicLocationList> sll)
        {
            string result = string.Empty;
            List<StrategicLocation> strategicLocations = new List<StrategicLocation>();

            if (sll.Count > 0)
            {
                foreach (StrategicLocationList sl in sll)
                {
                    StrategicLocation strategicLocation = new StrategicLocation();
                    int? DivisionID = db.District.Where(w => w.DistrictName.Contains(sl.District)).Select(s => s.DivisionID).FirstOrDefault();
                    int? DistrictID = db.District.Where(w => w.DistrictName.Contains(sl.District)).Select(s => s.DistrictID).FirstOrDefault();
                    int? UpazillaID = db.Upazilla.Where(w => w.UpazillaName.Contains(sl.Upazilla)).Select(s => s.UpazillaID).FirstOrDefault();

                    strategicLocation.SLID = (string.IsNullOrEmpty(sl.SLID)) ? 0 : int.Parse(sl.SLID);
                    strategicLocation.StrategicLocationName = sl.Location;
                    strategicLocation.DivisionID = DivisionID;
                    strategicLocation.DistrictID = DistrictID;
                    strategicLocation.UpazillaID = UpazillaID;
                    strategicLocation.UnionName = sl.Union;

                    strategicLocation.Latitude = sl.Latitude ?? 0;
                    strategicLocation.Longitude = sl.Longitude ?? 0;

                    strategicLocation.DateOfESTD = sl.DateOfESTD;
                    strategicLocation.UseOfWater = sl.UseOfWater;
                    strategicLocation.PolutionSource = sl.PolutionSource;
                    strategicLocation.Status = sl.Status;
                    strategicLocation.Remarks = sl.Remarks;

                    strategicLocation.CreatedBy = User.Identity.GetUserId();
                    strategicLocation.CreatedDate = DateTime.Now;
                    strategicLocation.IsActive = true;
                    strategicLocation.IsDelete = false;

                    strategicLocations.Add(strategicLocation);
                }

                if (strategicLocations.Count > 0)
                {
                    try
                    {
                        strategicLocations.Where(w => w.DivisionID == 0).Select(myObject => { myObject.DivisionID = null; return myObject; }).ToList();
                        strategicLocations.Where(w => w.DistrictID == 0).Select(myObject => { myObject.DistrictID = null; return myObject; }).ToList();
                        strategicLocations.Where(w => w.UpazillaID == 0).Select(myObject => { myObject.UpazillaID = null; return myObject; }).ToList();

                        db.StrategicLocations.AddRange(strategicLocations);
                        db.SaveChanges();

                        TempData["Message"] = ShowMessage(Sign.Success, OperationMessage.ImportSuccess.ToDescription());
                        result = ShowMessage(Sign.Success, OperationMessage.ImportSuccess.ToDescription());
                        return RedirectToAction("List");
                    }
                    catch (Exception ex)
                    {
                        string message = (string.IsNullOrEmpty(ex.Message) || ex.Message.Contains("See the inner exception")) ? ex.InnerException.InnerException.Message : ex.Message;
                        TempData["Message"] = ShowMessage(Sign.Danger, message);
                        result = ShowMessage(Sign.Danger, message);
                    }
                }
            }

            return Json(result, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult ProcessUploadFileData()
        {
            string userID = System.Web.HttpContext.Current.User.Identity.GetUserId();
            List<StrategicLocationList> sll = new List<StrategicLocationList>();
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
                        if (System.IO.File.Exists(path))
                        {
                            System.IO.File.Delete(path);
                        }

                        file.SaveAs(path);

                        if (extension == ".csv")
                        {
                            dt = Utility.ConvertCSVtoDataTable(path);
                            ViewBag.Data = dt;
                        }
                        else if (extension.Trim() == ".xls")
                        {
                            connString = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + path + ";Extended Properties=\"Excel 8.0;HDR=Yes;IMEX=2\"";
                            dt = Utility.ConvertXSLXtoDataTable(path, connString);
                            ViewBag.Data = dt;
                        }
                        else if (extension.Trim() == ".xlsx")
                        {
                            connString = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + path + ";Extended Properties=\"Excel 12.0;HDR=Yes;IMEX=2\"";
                            dt = Utility.ConvertXSLXtoDataTable(path, connString);
                            ViewBag.Data = dt;
                        }

                        sll = ProcessDataTable(dt);
                    }
                    else
                    {
                        ViewBag.Error = "Please Upload Files in .xls, .xlsx or .csv format";
                    }
                }
            }

            return Json(sll, JsonRequestBehavior.AllowGet);
        }

        private static List<StrategicLocationList> ProcessDataTable(DataTable dataTable)
        {
            List<StrategicLocationList> sll = new List<StrategicLocationList>();

            if (dataTable.Rows.Count > 0)
            {
                for (int i = 0; i < dataTable.Rows.Count; i++)
                {
                    StrategicLocationList sl = new StrategicLocationList();
                    sl.Serial = i + 1;
                    sl.SLID = dataTable.Rows[i][1].ToString();
                    sl.Location = dataTable.Rows[i][2].ToString();
                    sl.District = dataTable.Rows[i][3].ToString();
                    sl.Upazilla = dataTable.Rows[i][4].ToString();
                    sl.Union = dataTable.Rows[i][5].ToString();
                    sl.Latitude = string.IsNullOrEmpty(dataTable.Rows[i][6].ToString()) ? 0 : decimal.Parse(dataTable.Rows[i][6].ToString());
                    sl.Longitude = string.IsNullOrEmpty(dataTable.Rows[i][7].ToString()) ? 0 : decimal.Parse(dataTable.Rows[i][7].ToString());
                    sl.DateOfESTD = dataTable.Rows[i][8].ToString();
                    sl.UseOfWater = dataTable.Rows[i][9].ToString();
                    sl.PolutionSource = dataTable.Rows[i][10].ToString();
                    sl.Status = dataTable.Rows[i][11].ToString();
                    sl.Remarks = dataTable.Rows[i][12].ToString();
                    sll.Add(sl);
                }
            }
            else
            {
                sll = new List<StrategicLocationList>();
            }

            return sll;
        }

        // GET: /StrategicLocation/Create
        public ActionResult Create()
        {
            ViewBag.RiverID = new SelectList(db.Rivers.Where(w => w.IsActive == true && w.IsDelete == false).OrderBy(o => o.RiverName), "RiverID", "RiverName");
            ViewBag.DivisionID = new SelectList(db.Division, "DivisionID", "DivisionName", "");
            ViewBag.DistrictID = null;// new SelectList(db.District, "DistrictID", "DistrictName");
            ViewBag.UpazillaID = null;// new SelectList(db.Upazilla, "UpazillaID", "UpazillaName");

            return View();
        }

        // POST: /StrategicLocation/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "StrategicLocationID,SLID,StrategicLocationName,RiverID,DivisionID,DistrictID,UpazillaID,UnionName,Village,Latitude,Longitude,UseOfWater,PolutionSource")] StrategicLocation strategiclocation)
        {
            if (ModelState.IsValid)
            {
                if (strategiclocation != null)
                {
                    WaterQualityParamAPIController wqpAPI = new WaterQualityParamAPIController();

                    strategiclocation.CreatedBy = User.Identity.GetUserId();
                    strategiclocation.CreatedDate = DateTime.Now;
                    strategiclocation.IsActive = true;
                    strategiclocation.IsDelete = false;

                    try
                    {
                        db.StrategicLocations.Add(strategiclocation);
                        int x = await db.SaveChangesAsync();

                        if (x > 0)
                        {
                            TempData["Message"] = ShowMessage(Sign.Success, OperationMessage.Success.ToDescription());
                            return RedirectToAction("List");
                        }
                        else
                        {
                            TempData["Message"] = ShowMessage(Sign.Danger, OperationMessage.NotSuccess.ToDescription());
                            return View(strategiclocation);
                        }
                    }
                    catch (Exception ex)
                    {
                        string message = string.IsNullOrEmpty(ex.Message) ? ex.InnerException.Message : ex.Message;
                        TempData["Message"] = ShowMessage(Sign.Danger, message);
                    }
                }
            }

            ViewBag.RiverID = new SelectList(db.Rivers.Where(w => w.IsActive == true && w.IsDelete == false).OrderBy(o => o.RiverName), "RiverID", "RiverName", strategiclocation.RiverID);
            TempData["Message"] = ShowMessage(Sign.Warning, OperationMessage.Error.ToDescription());
            return View(strategiclocation);
        }

        // GET: /StrategicLocation/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            StrategicLocation strategiclocation = await db.StrategicLocations.FindAsync(id);
            ViewStrategicLocation viewStrategicLocation = new ViewStrategicLocation();

            if (strategiclocation == null)
            {
                return HttpNotFound();
            }
            else
            {
                viewStrategicLocation.StrategicLocationID = strategiclocation.StrategicLocationID;
                viewStrategicLocation.SLID = strategiclocation.SLID;
                viewStrategicLocation.StrategicLocationName = strategiclocation.StrategicLocationName;
                viewStrategicLocation.IsMonitoring = strategiclocation.IsMonitoring;
                viewStrategicLocation.RiverID = strategiclocation.RiverID;
                viewStrategicLocation.DivisionID = strategiclocation.DivisionID;
                viewStrategicLocation.DistrictID = strategiclocation.DistrictID;
                viewStrategicLocation.UpazillaID = strategiclocation.UpazillaID;
                viewStrategicLocation.UnionName = strategiclocation.UnionName;
                viewStrategicLocation.Village = strategiclocation.Village;
                viewStrategicLocation.Latitude = strategiclocation.Latitude.ToString();
                viewStrategicLocation.Longitude = strategiclocation.Longitude.ToString();
                viewStrategicLocation.UseOfWater = strategiclocation.UseOfWater;
            }

            ViewBag.RiverID = new SelectList(db.Rivers.Where(w => w.IsActive == true && w.IsDelete == false).OrderBy(o => o.RiverName), "RiverID", "RiverName", strategiclocation.RiverID);
            ViewBag.DivisionID = new SelectList(db.Division.Where(w => w.IsActive == true && w.IsDelete == false).OrderBy(o => o.DivisionName), "DivisionID", "DivisionName", strategiclocation.DivisionID);
            ViewBag.DistrictID = null;
            ViewBag.UpazillaID = null;

            ViewBag.River = strategiclocation.RiverID;
            ViewBag.Division = strategiclocation.DivisionID;
            ViewBag.District = strategiclocation.DistrictID;
            ViewBag.Upazilla = strategiclocation.UpazillaID;

            return View(viewStrategicLocation);
        }

        // POST: /StrategicLocation/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "StrategicLocationID,SLID,StrategicLocationName,RiverID,DivisionID,DistrictID,UpazillaID,UnionName,Village,Latitude,Longitude,UseOfWater,PolutionSource")] StrategicLocation strategiclocation)
        {
            if (ModelState.IsValid)
            {
                strategiclocation.UpdateBy = User.Identity.GetUserId();
                strategiclocation.UpdatedDate = DateTime.Now;
                strategiclocation.IsActive = true;
                strategiclocation.IsDelete = false;

                try
                {
                    db.Entry(strategiclocation).State = EntityState.Modified;
                    int x = await db.SaveChangesAsync();

                    if (x > 0)
                    {
                        TempData["Message"] = ShowMessage(Sign.Success, OperationMessage.Success.ToDescription());
                        return RedirectToAction("List");
                    }
                    else
                    {
                        TempData["Message"] = ShowMessage(Sign.Danger, OperationMessage.NotSuccess.ToDescription());
                        return View(strategiclocation);
                    }
                }
                catch (Exception ex)
                {
                    string message = string.IsNullOrEmpty(ex.Message) ? ex.InnerException.Message : ex.Message;
                    TempData["Message"] = Sign.Danger + ": " + message;
                }
            }

            ViewBag.RiverID = new SelectList(db.Rivers.Where(w => w.IsActive == true && w.IsDelete == false).OrderBy(o => o.RiverName), "RiverID", "RiverName", strategiclocation.RiverID);
            ViewBag.DistrictID = new SelectList(db.District.Where(w => w.IsActive == true && w.IsDelete == false).OrderBy(o => o.DistrictName), "DistrictID", "DistrictName", strategiclocation.DistrictID);
            ViewBag.DivisionID = new SelectList(db.Division.Where(w => w.IsActive == true && w.IsDelete == false).OrderBy(o => o.DivisionName), "DivisionID", "DivisionName", strategiclocation.DivisionID);
            ViewBag.UpazillaID = new SelectList(db.Upazilla.Where(w => w.IsActive == true && w.IsDelete == false).OrderBy(o => o.UpazillaName), "UpazillaID", "UpazillaName", strategiclocation.UpazillaID);

            return View(strategiclocation);
        }

        // GET: /StrategicLocation/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            StrategicLocation strategiclocation = await db.StrategicLocations.FindAsync(id);
            if (strategiclocation == null)
            {
                return HttpNotFound();
            }
            return View(strategiclocation);
        }

        // POST: /StrategicLocation/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            StrategicLocation strategiclocation = await db.StrategicLocations.FindAsync(id);
            db.StrategicLocations.Remove(strategiclocation);
            await db.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        // GET: /StrategicLocation/Import
        public ActionResult select2()
        {
            return View();
        }

        [HttpGet]
        public ActionResult select2data()
        {
            List<select2datalist> lists = new List<select2datalist>();

            try
            {
                lists = db.StrategicLocations
                    .Where(w => w.IsActive == true)
                    .Select(s => new select2datalist
                    {
                        id = s.StrategicLocationID,
                        text = s.StrategicLocationName,
                        html = "<b>" + s.ShortFormName + "</b>"
                    }).ToList();
            }
            catch (Exception ex)
            {
                lists = null;
            }

            //var result = string.Join(", ", lists);
            return Json(lists, JsonRequestBehavior.AllowGet);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }

    public class select2datalist
    {
        public int id { get; set; }
        public string text { get; set; }
        public string html { get; set; }
    }
}
