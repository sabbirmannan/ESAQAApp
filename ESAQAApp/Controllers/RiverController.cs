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
    public class RiverController : BaseController
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: /River/
        public ActionResult Index()
        {
            ViewBag.TotalCat = db.Rivers.Where(w => w.IsActive == true && w.IsDelete == false).Count();
            return View();
        }

        public async Task<ActionResult> List()
        {
            return View(await db.Rivers.Where(w => w.IsActive == true && w.IsDelete == false).OrderBy(o => o.RiverName).ToListAsync());
        }

        // GET: /River/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            River river = await db.Rivers.FindAsync(id);
            if (river == null)
            {
                return HttpNotFound();
            }
            return View(river);
        }

        // GET: /River/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: /River/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "RiverID,RiverStandardID,RiverName,RiverNameInBangla,Location")] River river)
        {
            if (ModelState.IsValid)
            {
                if (river != null)
                {
                    river.CreatedBy = User.Identity.GetUserId();
                    river.CreatedDate = DateTime.Now;
                    river.IsActive = true;
                    river.IsDelete = false;

                    try
                    {
                        db.Rivers.Add(river);
                        await db.SaveChangesAsync();

                        TempData["Message"] = ShowMessage(Sign.Success, OperationMessage.Success.ToDescription());
                        return RedirectToAction("List");
                    }
                    catch (Exception ex)
                    {
                        string message = string.IsNullOrEmpty(ex.Message) ? ex.InnerException.Message : ex.Message;
                        TempData["Message"] = ShowMessage(Sign.Danger, message);
                        return View(river);
                    }
                }
            }

            TempData["Message"] = ShowMessage(Sign.Warning, OperationMessage.Error.ToDescription());
            return View(river);
        }

        // GET: /River/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            River river = await db.Rivers.FindAsync(id);
            if (river == null)
            {
                return HttpNotFound();
            }
            return View(river);
        }

        // POST: /River/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "RiverID,RiverStandardID,RiverName,RiverNameInBangla,Location,IsActive,IsDelete,CreatedBy,CreatedDate,DeletedBy,DeletedDate")] River river)
        {
            if (ModelState.IsValid)
            {
                if (river != null)
                {
                    river.UpdateBy = User.Identity.GetUserId();
                    river.UpdatedDate = DateTime.Now;
                    river.IsActive = true;
                    river.IsDelete = false;

                    try
                    {
                        db.Entry(river).State = EntityState.Modified;
                        await db.SaveChangesAsync();
                        TempData["Message"] = ShowMessage(Sign.Success, OperationMessage.Update.ToDescription());
                        return RedirectToAction("List");
                    }
                    catch (Exception ex)
                    {
                        string message = string.IsNullOrEmpty(ex.Message) ? ex.InnerException.Message : ex.Message;
                        TempData["Message"] = ShowMessage(Sign.Danger, message);
                        return View(river);
                    }
                }
            }

            TempData["Message"] = ShowMessage(Sign.Warning, OperationMessage.Error.ToDescription());
            return View(river);
        }

        // GET: /River/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            River river = await db.Rivers.FindAsync(id);
            if (river == null)
            {
                return HttpNotFound();
            }
            return View(river);
        }

        // POST: /River/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            River river = await db.Rivers.FindAsync(id);

            if (river != null)
            {
                river.IsDelete = true;
                river.DeletedBy = User.Identity.GetUserId();
                river.DeletedDate = DateTime.Now;

                try
                {
                    db.Entry(river).State = EntityState.Modified;
                    await db.SaveChangesAsync();

                    TempData["Message"] = ShowMessage(Sign.Success, OperationMessage.Delete.ToDescription());
                    return RedirectToAction("List");
                }
                catch (Exception ex)
                {
                    string message = string.IsNullOrEmpty(ex.Message) ? ex.InnerException.Message : ex.Message;
                    TempData["Message"] = ShowMessage(Sign.Danger, message);
                    return View(river);
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
