using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using StartApp.Cms.App_Start;
using StartAppModel;

namespace StartApp.Cms.Controllers
{
    [_SessionControl]

    public class PlaceTypesController : Controller
    {
        private StartAppEntities db = new StartAppEntities();

        // GET: PlaceTypes
        public ActionResult Index()
        {
            return View(db.PlaceTypes.ToList());
        }

        // GET: PlaceTypes/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PlaceTypes placeTypes = db.PlaceTypes.Find(id);
            if (placeTypes == null)
            {
                return HttpNotFound();
            }
            return View(placeTypes);
        }

        // GET: PlaceTypes/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: PlaceTypes/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Name")] PlaceTypes placeTypes)
        {
            if (ModelState.IsValid)
            {
                db.PlaceTypes.Add(placeTypes);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(placeTypes);
        }

        // GET: PlaceTypes/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PlaceTypes placeTypes = db.PlaceTypes.Find(id);
            if (placeTypes == null)
            {
                return HttpNotFound();
            }
            return View(placeTypes);
        }

        // POST: PlaceTypes/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Name")] PlaceTypes placeTypes)
        {
            if (ModelState.IsValid)
            {
                db.Entry(placeTypes).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(placeTypes);
        }

        // GET: PlaceTypes/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PlaceTypes placeTypes = db.PlaceTypes.Find(id);
            if (placeTypes == null)
            {
                return HttpNotFound();
            }
            return View(placeTypes);
        }

        // POST: PlaceTypes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            PlaceTypes placeTypes = db.PlaceTypes.Find(id);
            db.PlaceTypes.Remove(placeTypes);
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
