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
    public class PlaceCategoriesController : Controller
    {
        private StartAppEntities db = new StartAppEntities();

        // GET: PlaceCategories
        public ActionResult Index()
        {
            var placeCategories = db.PlaceCategories.Include(p => p.Categories).Include(p => p.Places);
            return View(placeCategories.ToList());
        }

        // GET: PlaceCategories/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PlaceCategories placeCategories = db.PlaceCategories.Find(id);
            if (placeCategories == null)
            {
                return HttpNotFound();
            }
            return View(placeCategories);
        }

        // GET: PlaceCategories/Create
        public ActionResult Create()
        {
            ViewBag.CategoryId = new SelectList(db.Categories, "Id", "Name");
            ViewBag.PlaceId = new SelectList(db.Places, "Id", "Name");
            return View();
        }

        // POST: PlaceCategories/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,CategoryId,PlaceId,Note,IsActive")] PlaceCategories placeCategories)
        {
            if (ModelState.IsValid)
            {
                db.PlaceCategories.Add(placeCategories);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.CategoryId = new SelectList(db.Categories, "Id", "Name", placeCategories.CategoryId);
            ViewBag.PlaceId = new SelectList(db.Places, "Id", "Name", placeCategories.PlaceId);
            return View(placeCategories);
        }

        // GET: PlaceCategories/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PlaceCategories placeCategories = db.PlaceCategories.Find(id);
            if (placeCategories == null)
            {
                return HttpNotFound();
            }
            ViewBag.CategoryId = new SelectList(db.Categories, "Id", "Name", placeCategories.CategoryId);
            ViewBag.PlaceId = new SelectList(db.Places, "Id", "Name", placeCategories.PlaceId);
            return View(placeCategories);
        }

        // POST: PlaceCategories/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,CategoryId,PlaceId,Note,IsActive")] PlaceCategories placeCategories)
        {
            if (ModelState.IsValid)
            {
                db.Entry(placeCategories).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.CategoryId = new SelectList(db.Categories, "Id", "Name", placeCategories.CategoryId);
            ViewBag.PlaceId = new SelectList(db.Places, "Id", "Name", placeCategories.PlaceId);
            return View(placeCategories);
        }

        // GET: PlaceCategories/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PlaceCategories placeCategories = db.PlaceCategories.Find(id);
            if (placeCategories == null)
            {
                return HttpNotFound();
            }
            return View(placeCategories);
        }

        // POST: PlaceCategories/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            PlaceCategories placeCategories = db.PlaceCategories.Find(id);
            db.PlaceCategories.Remove(placeCategories);
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
