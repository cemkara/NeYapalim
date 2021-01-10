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
    public class PlaceImagesController : Controller
    {
        private StartAppEntities db = new StartAppEntities();

        // GET: PlaceImages
        public ActionResult Index()
        {
            var placeImages = db.PlaceImages.Include(p => p.Places);
            return View(placeImages.ToList());
        }

        // GET: PlaceImages/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PlaceImages placeImages = db.PlaceImages.Find(id);
            if (placeImages == null)
            {
                return HttpNotFound();
            }
            return View(placeImages);
        }

        // GET: PlaceImages/Create
        public ActionResult Create()
        {
            ViewBag.PlaceId = new SelectList(db.Places, "Id", "Name");
            return View();
        }

        // POST: PlaceImages/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,PlaceId,ImageUrl,MainImage,RecordDate")] PlaceImages placeImages)
        {
            if (ModelState.IsValid)
            {
                db.PlaceImages.Add(placeImages);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.PlaceId = new SelectList(db.Places, "Id", "Name", placeImages.PlaceId);
            return View(placeImages);
        }

        // GET: PlaceImages/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PlaceImages placeImages = db.PlaceImages.Find(id);
            if (placeImages == null)
            {
                return HttpNotFound();
            }
            ViewBag.PlaceId = new SelectList(db.Places, "Id", "Name", placeImages.PlaceId);
            return View(placeImages);
        }

        // POST: PlaceImages/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,PlaceId,ImageUrl,MainImage,RecordDate")] PlaceImages placeImages)
        {
            if (ModelState.IsValid)
            {
                db.Entry(placeImages).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.PlaceId = new SelectList(db.Places, "Id", "Name", placeImages.PlaceId);
            return View(placeImages);
        }

        // GET: PlaceImages/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PlaceImages placeImages = db.PlaceImages.Find(id);
            if (placeImages == null)
            {
                return HttpNotFound();
            }
            return View(placeImages);
        }

        // POST: PlaceImages/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            PlaceImages placeImages = db.PlaceImages.Find(id);
            db.PlaceImages.Remove(placeImages);
            db.SaveChanges();
            return RedirectToAction("PlaceImages/" + placeImages.PlaceId, "Places");
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
