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
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.Owin.Security;
using DOF003.Models;
using DOF003.Helpers;

namespace DOF003.Controllers
{
    [AccessDeniedAuthorize]
    public class WaterQualityCategoryController : BaseController
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: /WaterQualityCategory/
        [AccessDeniedAuthorize(Roles = "Admin, CanEditUser, CanEditRole, CanEditGroup, User")]
        public ActionResult Index()
        {
            ViewBag.TotalCat = db.WaterQualityCategories.Where(w => w.IsActive == true && w.IsDelete == false).Count();
            return View();
        }

        // GET: WaterQualityParam List
        [AccessDeniedAuthorize(Roles = "Admin, CanEditUser, CanEditRole, CanEditGroup, User")]
        public async Task<ActionResult> List()
        {
            return View(await db.WaterQualityCategories.Where(w => w.IsActive == true && w.IsDelete == false).ToListAsync());
        }

        // GET: /WaterQualityCategory/Details/5
        [AccessDeniedAuthorize(Roles = "Admin, CanEditUser, CanEditRole, CanEditGroup, User")]
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            WaterQualityCategory waterqualitycategory = await db.WaterQualityCategories.FindAsync(id);

            if (waterqualitycategory == null)
            {
                return HttpNotFound();
            }

            return View(waterqualitycategory);
        }

        // GET: /WaterQualityCategory/Create
        [AccessDeniedAuthorize(Roles = "Admin, CanEditUser, CanEditRole, CanEditGroup, User")]
        public ActionResult Create()
        {
            return View();
        }

        // POST: /WaterQualityCategory/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [AccessDeniedAuthorize(Roles = "Admin, CanEditUser, CanEditRole, CanEditGroup, User")]
        public async Task<ActionResult> Create([Bind(Include = "WaterQualityCategoryID,CategoryName,Symbol,StandardRange,Remarks")] WaterQualityCategory waterqualitycategory)
        {
            if (ModelState.IsValid)
            {
                if (waterqualitycategory != null)
                {
                    waterqualitycategory.CreatedBy = User.Identity.GetUserId();
                    waterqualitycategory.CreatedDate = DateTime.Now;
                    waterqualitycategory.IsActive = true;
                    waterqualitycategory.IsDelete = false;
                    try
                    {
                        db.WaterQualityCategories.Add(waterqualitycategory);
                        await db.SaveChangesAsync();

                        TempData["Message"] = ShowMessage(Sign.Success, OperationMessage.Success.ToDescription());
                        return RedirectToAction("List");
                    }
                    catch (Exception ex)
                    {

                        string message = string.IsNullOrEmpty(ex.Message) ? ex.InnerException.Message : ex.Message;
                        TempData["Message"] = ShowMessage(Sign.Danger, message);
                        return View(waterqualitycategory);
                    }
                }
            }

            return View(waterqualitycategory);
        }

        // GET: /WaterQualityCategory/Edit/5
        [AccessDeniedAuthorize(Roles = "Admin, CanEditGroup")]
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            WaterQualityCategory waterqualitycategory = await db.WaterQualityCategories.FindAsync(id);

            if (waterqualitycategory == null)
            {
                return HttpNotFound();
            }

            return View(waterqualitycategory);
        }

        // POST: /WaterQualityCategory/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [AccessDeniedAuthorize(Roles = "Admin, CanEditGroup")]
        public async Task<ActionResult> Edit([Bind(Include = "WaterQualityCategoryID,CategoryName,Symbol,StandardRange,Remarks,IsActive,IsDelete,CreatedBy,CreatedDate,DeletedBy,DeletedDate")] WaterQualityCategory waterqualitycategory)
        {
            if (ModelState.IsValid)
            {
                if (waterqualitycategory != null)
                {
                    waterqualitycategory.UpdateBy = User.Identity.GetUserId();
                    waterqualitycategory.UpdatedDate = DateTime.Now;
                    waterqualitycategory.IsActive = true;
                    waterqualitycategory.IsDelete = false;

                    try
                    {
                        db.Entry(waterqualitycategory).State = EntityState.Modified;
                        await db.SaveChangesAsync();
                        TempData["Message"] = ShowMessage(Sign.Success, OperationMessage.Update.ToDescription());
                        return RedirectToAction("List");
                    }
                    catch (Exception ex)
                    {
                        string message = string.IsNullOrEmpty(ex.Message) ? ex.InnerException.Message : ex.Message;
                        TempData["Message"] = ShowMessage(Sign.Danger, message);
                        return View(waterqualitycategory);
                    }
                }
            }

            TempData["Message"] = ShowMessage(Sign.Warning, OperationMessage.Error.ToDescription());
            return View(waterqualitycategory);
        }

        // GET: /WaterQualityCategory/Delete/5
        [AccessDeniedAuthorize(Roles = "Admin")]
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }


            WaterQualityCategory waterqualitycategory = await db.WaterQualityCategories.FindAsync(id);

            if (waterqualitycategory == null)
            {
                return HttpNotFound();
            }

            return View(waterqualitycategory);
        }

        // POST: /WaterQualityCategory/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [AccessDeniedAuthorize(Roles = "Admin")]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            WaterQualityCategory waterqualitycategory = await db.WaterQualityCategories.FindAsync(id);

            if (waterqualitycategory != null)
            {
                waterqualitycategory.IsDelete = true;
                waterqualitycategory.DeletedBy = User.Identity.GetUserId();
                waterqualitycategory.DeletedDate = DateTime.Now;

                try
                {
                    db.Entry(waterqualitycategory).State = EntityState.Modified;
                    await db.SaveChangesAsync();

                    TempData["Message"] = ShowMessage(Sign.Success, OperationMessage.Delete.ToDescription());
                    return RedirectToAction("List");
                }
                catch (Exception ex)
                {
                    string message = string.IsNullOrEmpty(ex.Message) ? ex.InnerException.Message : ex.Message;
                    TempData["Message"] = ShowMessage(Sign.Danger, message);
                    return View(waterqualitycategory);
                }
            }

            return RedirectToAction("List");
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
}
