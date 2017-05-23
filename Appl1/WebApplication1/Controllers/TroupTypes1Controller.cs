using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    public class TroupTypes1Controller : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: TroupTypes1
        public ActionResult Index()
        {
            return View(db.TroupTypes.ToList());
        }

        // GET: TroupTypes1/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TroupType troupType = db.TroupTypes.Find(id);
            if (troupType == null)
            {
                return HttpNotFound();
            }
            return View(troupType);
        }

        // GET: TroupTypes1/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: TroupTypes1/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "TroupTypeId,Name,Attack,Defence,CreationSpeed")] TroupType troupType)
        {
            if (ModelState.IsValid)
            {
                db.TroupTypes.Add(troupType);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(troupType);
        }

        // GET: TroupTypes1/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TroupType troupType = db.TroupTypes.Find(id);
            if (troupType == null)
            {
                return HttpNotFound();
            }
            return View(troupType);
        }

        // POST: TroupTypes1/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "TroupTypeId,Name,Attack,Defence,CreationSpeed")] TroupType troupType)
        {
            if (ModelState.IsValid)
            {
                db.Entry(troupType).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(troupType);
        }

        // GET: TroupTypes1/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TroupType troupType = db.TroupTypes.Find(id);
            if (troupType == null)
            {
                return HttpNotFound();
            }
            return View(troupType);
        }

        // POST: TroupTypes1/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            TroupType troupType = db.TroupTypes.Find(id);
            db.TroupTypes.Remove(troupType);
            db.SaveChanges();
            return RedirectToAction("Index");
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
