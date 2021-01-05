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
    public class ContentPlacesController : Controller
    {
        private StartAppEntities db = new StartAppEntities();

        // GET: ContentPlaces
        public ActionResult Index()
        {
            var contentPlaces = db.ContentPlaces.Include(c => c.Places).Include(c => c.Contents);
            return View(contentPlaces.ToList());
        }

        // GET: ContentPlaces/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ContentPlaces contentPlaces = db.ContentPlaces.Find(id);
            if (contentPlaces == null)
            {
                return HttpNotFound();
            }
            return View(contentPlaces);
        }

        // GET: ContentPlaces/Create
        public ActionResult Create()
        {
            ViewBag.PlacesId = new SelectList(db.Places, "Id", "Name");
            ViewBag.ContentId = new SelectList(db.Contents, "Id", "Name");
            return View();
        }

        // POST: ContentPlaces/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,ContentId,PlacesId,IsActive")] ContentPlaces contentPlaces)
        {
            if (ModelState.IsValid)
            {
                db.ContentPlaces.Add(contentPlaces);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.PlacesId = new SelectList(db.Places, "Id", "Name", contentPlaces.PlacesId);
            ViewBag.ContentId = new SelectList(db.Contents, "Id", "Name", contentPlaces.ContentId);
            return View(contentPlaces);
        }

        // GET: ContentPlaces/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ContentPlaces contentPlaces = db.ContentPlaces.Find(id);
            if (contentPlaces == null)
            {
                return HttpNotFound();
            }
            ViewBag.PlacesId = new SelectList(db.Places, "Id", "Name", contentPlaces.PlacesId);
            ViewBag.ContentId = new SelectList(db.Contents, "Id", "Name", contentPlaces.ContentId);
            return View(contentPlaces);
        }

        // POST: ContentPlaces/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,ContentId,PlacesId,IsActive")] ContentPlaces contentPlaces)
        {
            if (ModelState.IsValid)
            {
                db.Entry(contentPlaces).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.PlacesId = new SelectList(db.Places, "Id", "Name", contentPlaces.PlacesId);
            ViewBag.ContentId = new SelectList(db.Contents, "Id", "Name", contentPlaces.ContentId);
            return View(contentPlaces);
        }

        // GET: ContentPlaces/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ContentPlaces contentPlaces = db.ContentPlaces.Find(id);
            if (contentPlaces == null)
            {
                return HttpNotFound();
            }
            return View(contentPlaces);
        }

        // POST: ContentPlaces/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            ContentPlaces contentPlaces = db.ContentPlaces.Find(id);
            db.ContentPlaces.Remove(contentPlaces);
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
