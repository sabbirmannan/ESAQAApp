using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using BAC007.Models;

namespace BAC007.Controllers
{
    public class TableInfoController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: TableInfo
        public ActionResult Index()
        {
            //return View(db.TableInfo.ToList());
            return View();
        }

        // GET: TableInfo/Details/5
        public ActionResult Details(int? id)
        {
            //if (id == null)
            //{
            //    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            //}
            //TableInfo tableInfo = db.TableInfo.Find(id);
            //if (tableInfo == null)
            //{
            //    return HttpNotFound();
            //}
            //return View(tableInfo);

            return View();
        }

        // GET: TableInfo/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: TableInfo/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "TableInfoId,FormOrder,WhichPart,FormTitle,TableTitle,LookupTableName,DataSaveTableName,IsIndividualTable,IsResponseCols,IsFiscalYearFlat,IsFiscalYearTwoCols")] TableInfo tableInfo)
        {
            //if (ModelState.IsValid)
            //{
            //    db.TableInfo.Add(tableInfo);
            //    db.SaveChanges();
            //    return RedirectToAction("Index");
            //}

            //return View(tableInfo);

            return View();
        }

        // GET: TableInfo/Edit/5
        public ActionResult Edit(int? id)
        {
            //if (id == null)
            //{
            //    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            //}
            //TableInfo tableInfo = db.TableInfo.Find(id);
            //if (tableInfo == null)
            //{
            //    return HttpNotFound();
            //}
            //return View(tableInfo);

            return View();
        }

        // POST: TableInfo/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "TableInfoId,FormOrder,WhichPart,FormTitle,TableTitle,LookupTableName,DataSaveTableName,IsIndividualTable,IsResponseCols,IsFiscalYearFlat,IsFiscalYearTwoCols")] TableInfo tableInfo)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tableInfo).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(tableInfo);
        }

        // GET: TableInfo/Delete/5
        public ActionResult Delete(int? id)
        {
            //if (id == null)
            //{
            //    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            //}
            //TableInfo tableInfo = db.TableInfo.Find(id);
            //if (tableInfo == null)
            //{
            //    return HttpNotFound();
            //}
            //return View(tableInfo);

            return View();
        }

        // POST: TableInfo/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            //TableInfo tableInfo = db.TableInfo.Find(id);
            //db.TableInfo.Remove(tableInfo);
            //db.SaveChanges();
            //return RedirectToAction("Index");

            return View();
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
