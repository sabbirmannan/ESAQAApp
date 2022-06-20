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
using PagedList;
using PagedList.Mvc;

namespace DOF003.Controllers
{
    [AccessDeniedAuthorize]
    public class WaterQualityParamController : BaseController
    {
        #region DB Context Initilization
        private ApplicationDbContext db = new ApplicationDbContext();
        private string message = string.Empty;
        #endregion

        #region Index
        // GET: WaterQualityParam
        public ActionResult Index()
        {
            ViewBag.TotalParams = db.WaterQualityParams.Where(w => w.IsActive == true && w.IsDelete == false).Count();
            return View();
        }

        // GET: WaterQualityParam List
        [HttpGet]
        public async Task<ActionResult> List(int page = 1, int pageSize = 10)
        {
            List<WaterQualityParam> wqps = await db.WaterQualityParams.Where(w => w.IsActive == true && w.IsDelete == false).Include(w => w.WaterQualityCategory).ToListAsync();
            PagedList<WaterQualityParam> pl = new PagedList<WaterQualityParam>(wqps, page, pageSize);
            return View(pl);
        }

        // POST: WaterQualityParam Searching
        [HttpGet]
        public async Task<ActionResult> Search(string SearchText, int page = 1, int pageSize = 10)
        {
            List<WaterQualityParam> wqps = await db.WaterQualityParams.Where(w => w.IsActive == true && w.IsDelete == false).Include(w => w.WaterQualityCategory).ToListAsync();
            PagedList<WaterQualityParam> pl = new PagedList<WaterQualityParam>(wqps, page, pageSize);

            try
            {
                wqps = wqps.Where(w =>
                                    w.ParameterName.Contains(SearchText) ||
                                    w.WaterQualityCategory.CategoryName.Contains(SearchText) ||
                                        //w.StandardRange.Contains(SearchText) ||
                                    w.Remarks.Contains(SearchText)
                                 ).ToList();

                pl = new PagedList<WaterQualityParam>(wqps, page, pageSize);
            }
            catch (Exception ex)
            {
                pl = new PagedList<WaterQualityParam>(wqps, page, pageSize);
                message = string.IsNullOrEmpty(ex.Message) ? ex.InnerException.Message : ex.Message;
                TempData["Message"] = ShowMessage(Sign.Danger, message);
            }

            return View("List", pl);
        }
        #endregion

        #region Details
        // GET: WaterQualityParam/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            WaterQualityParam waterQualityParam = await db.WaterQualityParams.FindAsync(id);

            if (waterQualityParam == null)
            {
                return HttpNotFound();
            }

            ViewBag.WaterQualityCategoryID = new SelectList(db.WaterQualityCategories, "WaterQualityCategoryID", "CategoryName");
            return View(waterQualityParam);
        }
        #endregion

        #region Create
        // GET: WaterQualityParam/Create
        public ActionResult Create()
        {
            //ViewBag.WaterQualityCategoryID = new SelectList(db.WaterQualityCategories, "WaterQualityCategoryID", "CategoryName");
            return View();
        }

        // POST: WaterQualityParam/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "WaterQualityParamID,ParameterName,WaterQualityCategoryID,Unit,IsRange,MinRange,MaxRange,FixedVal,Remarks")] WaterQualityParam waterQualityParam)
        {
            if (ModelState.IsValid)
            {
                if (waterQualityParam != null)
                {
                    WaterQualityParamAPIController wqpAPI = new WaterQualityParamAPIController();

                    waterQualityParam.CreatedBy = User.Identity.GetUserId();
                    waterQualityParam.CreatedDate = DateTime.Now;
                    waterQualityParam.IsActive = true;
                    waterQualityParam.IsDelete = false;

                    try
                    {
                        var response = await wqpAPI.PostWaterQualityParam(waterQualityParam);
                        var result = response as OkNegotiatedContentResult<bool>;
                        if (result.Content)
                        {
                            TempData["Message"] = ShowMessage(Sign.Success, OperationMessage.Success.ToDescription());
                            return RedirectToAction("List");
                        }
                        else
                        {
                            TempData["Message"] = ShowMessage(Sign.Danger, OperationMessage.NotSuccess.ToDescription());
                            ViewBag.WaterQualityCategoryID = new SelectList(db.WaterQualityCategories, "WaterQualityCategoryID", "CategoryName", waterQualityParam.WaterQualityCategoryID);
                            return View(waterQualityParam);
                        }
                    }
                    catch (Exception ex)
                    {
                        string message = string.IsNullOrEmpty(ex.Message) ? ex.InnerException.Message : ex.Message;
                        TempData["Message"] = ShowMessage(Sign.Danger, message);
                    }
                }
            }

            //ViewBag.WaterQualityCategoryID = new SelectList(db.WaterQualityCategories, "WaterQualityCategoryID", "CategoryName", waterQualityParam.WaterQualityCategoryID);
            TempData["Message"] = ShowMessage(Sign.Warning, OperationMessage.Error.ToDescription());
            return View(waterQualityParam);
        }
        #endregion

        #region Edit
        // GET: WaterQualityParam/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            WaterQualityParam waterQualityParam = await db.WaterQualityParams.FindAsync(id);

            if (waterQualityParam == null)
            {
                return HttpNotFound();
            }

            ViewBag.WaterQualityCategoryID = new SelectList(db.WaterQualityCategories.Where(w => w.IsActive == true && w.IsDelete == false), "WaterQualityCategoryID", "CategoryName", waterQualityParam.WaterQualityCategoryID);
            return View(waterQualityParam);
        }

        // POST: WaterQualityParam/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "WaterQualityParamID,ParameterName,WaterQualityCategoryID,Unit,IsRange,MinRange,MaxRange,FixedVal,Remarks,IsActive,IsDelete,CreatedBy,CreatedDate,DeletedBy,DeletedDate")] WaterQualityParam waterQualityParam)
        {
            if (ModelState.IsValid)
            {
                WaterQualityParamAPIController wqpAPI = new WaterQualityParamAPIController();

                if (waterQualityParam != null)
                {
                    waterQualityParam.UpdateBy = User.Identity.GetUserId();
                    waterQualityParam.UpdatedDate = DateTime.Now;
                    waterQualityParam.IsActive = true;
                    waterQualityParam.IsDelete = false;

                    try
                    {
                        var response = await wqpAPI.PutWaterQualityParam(waterQualityParam.WaterQualityParamID, waterQualityParam);
                        var result = response as OkNegotiatedContentResult<bool>;
                        if (result.Content)
                        {
                            TempData["Message"] = ShowMessage(Sign.Success, OperationMessage.Update.ToDescription());
                            return RedirectToAction("List");
                        }
                        else
                        {
                            TempData["Message"] = ShowMessage(Sign.Danger, OperationMessage.NotUpdate.ToDescription());
                            ViewBag.WaterQualityCategoryID = new SelectList(db.WaterQualityCategories, "WaterQualityCategoryID", "CategoryName", waterQualityParam.WaterQualityCategoryID);
                            return View(waterQualityParam);
                        }
                    }
                    catch (Exception ex)
                    {
                        string message = string.IsNullOrEmpty(ex.Message) ? ex.InnerException.Message : ex.Message;
                        TempData["Message"] = Sign.Danger + ": " + message;
                    }
                }
            }

            TempData["Message"] = ShowMessage(Sign.Warning, OperationMessage.Error.ToDescription());
            //ViewBag.WaterQualityCategoryID = new SelectList(db.WaterQualityCategories, "WaterQualityCategoryID", "CategoryName", waterQualityParam.WaterQualityCategoryID);
            return View(waterQualityParam);
        }
        #endregion

        #region Delete
        // GET: WaterQualityParam/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            WaterQualityParam waterQualityParam = await db.WaterQualityParams.FindAsync(id);
            if (waterQualityParam == null)
            {
                return HttpNotFound();
            }

            return View(waterQualityParam);
        }

        // POST: WaterQualityParam/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            WaterQualityParam waterQualityParam = await db.WaterQualityParams.FindAsync(id);

            if (waterQualityParam != null)
            {
                waterQualityParam.IsDelete = true;
                waterQualityParam.DeletedBy = User.Identity.GetUserId();
                waterQualityParam.DeletedDate = DateTime.Now;

                try
                {
                    db.Entry(waterQualityParam).State = EntityState.Modified;
                    await db.SaveChangesAsync();

                    TempData["Message"] = ShowMessage(Sign.Success, OperationMessage.Delete.ToDescription());
                    return RedirectToAction("List");
                }
                catch (Exception ex)
                {

                    string message = string.IsNullOrEmpty(ex.Message) ? ex.InnerException.Message : ex.Message;
                    TempData["Message"] = ShowMessage(Sign.Danger, message);
                    return View(waterQualityParam);
                }

            }

            return RedirectToAction("List");
        }
        #endregion

        #region Dashboard Setup
        // GET: WaterQualityParam/Dashboard
        public ActionResult DashboardSetup()
        {
            List<SelectListItem> param = (from wqp in db.WaterQualityParams.Where(w => w.IsActive == true && w.IsDelete == false).GroupBy(g => g.ParameterName).Select(s => s.FirstOrDefault()).OrderBy(o => o.ParameterName)
                                          select new SelectListItem
                                          {
                                              Text = wqp.ParameterName,
                                              Value = wqp.WaterQualityParamID.ToString()
                                          }).ToList();
            ViewBag.Params = param;
            return View();
        }

        // POST: WaterQualityParam/Dashboard
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DashboardSetup(FormCollection formCollection)
        {
            List<SelectListItem> paramList = (from wqp in db.WaterQualityParams.Where(w => w.IsActive == true && w.IsDelete == false).GroupBy(g => g.ParameterName).Select(s => s.FirstOrDefault()).OrderBy(o => o.ParameterName)
                                              select new SelectListItem
                                              {
                                                  Text = wqp.ParameterName,
                                                  Value = wqp.WaterQualityParamID.ToString()
                                              }).ToList();

            if (ModelState.IsValid)
            {
                var param = formCollection["Parameters"];
                string[] Parameters = param.Split(',');

                //if (Parameters.Length > 0)
                //{
                //    db.Dashboards.RemoveRange(db.Dashboards);
                //    int d = await db.SaveChangesAsync();

                //    foreach (string item in Parameters)
                //    {
                //        if (!string.IsNullOrEmpty(item))
                //        {
                //            Dashboard dashboard = new Dashboard();
                //            dashboard.Parameter = item;
                //            dashboard.IsActive = true;
                //            dashboard.IsDelete = false;

                //            db.Dashboards.Add(dashboard);
                //            int x = await db.SaveChangesAsync();
                //        }
                //    }
                //}
            }

            ViewBag.Params = paramList;
            return View();
        }
        #endregion

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
