using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using StartAppModel;

namespace StartApp.Cms.Controllers
{
    public class PlacePropertiesController : Controller
    {
        private StartAppEntities db = new StartAppEntities();

        // GET: PlaceProperties
        public ActionResult Index()
        {
            var placeProperties = db.PlaceProperties.Include(p => p.Places).Include(p => p.Properties);
            return View(placeProperties.ToList());
        }

        // GET: PlaceProperties/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PlaceProperties placeProperties = db.PlaceProperties.Find(id);
            if (placeProperties == null)
            {
                return HttpNotFound();
            }
            return View(placeProperties);
        }

        // GET: PlaceProperties/Create
        public ActionResult Create()
        {
            ViewBag.PlaceId = new SelectList(db.Places, "Id", "Name");
            ViewBag.PropertyId = new SelectList(db.Properties, "Id", "Name");
            return View();
        }

        // POST: PlaceProperties/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,PropertyId,PlaceId,IsActive")] PlaceProperties placeProperties)
        {
            if (ModelState.IsValid)
            {
                db.PlaceProperties.Add(placeProperties);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.PlaceId = new SelectList(db.Places, "Id", "Name", placeProperties.PlaceId);
            ViewBag.PropertyId = new SelectList(db.Properties, "Id", "Name", placeProperties.PropertyId);
            return View(placeProperties);
        }

        // GET: PlaceProperties/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PlaceProperties placeProperties = db.PlaceProperties.Find(id);
            if (placeProperties == null)
            {
                return HttpNotFound();
            }
            ViewBag.PlaceId = new SelectList(db.Places, "Id", "Name", placeProperties.PlaceId);
            ViewBag.PropertyId = new SelectList(db.Properties, "Id", "Name", placeProperties.PropertyId);
            return View(placeProperties);
        }

        // POST: PlaceProperties/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,PropertyId,PlaceId,IsActive")] PlaceProperties placeProperties)
        {
            if (ModelState.IsValid)
            {
                db.Entry(placeProperties).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.PlaceId = new SelectList(db.Places, "Id", "Name", placeProperties.PlaceId);
            ViewBag.PropertyId = new SelectList(db.Properties, "Id", "Name", placeProperties.PropertyId);
            return View(placeProperties);
        }

        // GET: PlaceProperties/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PlaceProperties placeProperties = db.PlaceProperties.Find(id);
            if (placeProperties == null)
            {
                return HttpNotFound();
            }
            return View(placeProperties);
        }

        // POST: PlaceProperties/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            PlaceProperties placeProperties = db.PlaceProperties.Find(id);
            db.PlaceProperties.Remove(placeProperties);
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
