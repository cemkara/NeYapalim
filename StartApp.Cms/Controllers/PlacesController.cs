using StartApp.Cms.App_Start;
using StartAppModel;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Security.Policy;
using System.Web;
using System.Web.Mvc;

namespace StartApp.Cms.Controllers
{
    [_SessionControl]

    public class PlacesController : Controller
    {
        private StartAppEntities db = new StartAppEntities();

        // GET: Places
        public ActionResult Index()
        {
            var places = db.Places.Include(p => p.Districts).Include(p => p.PlaceTypes);
            return View(places.ToList());
        }

        // GET: Places/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Places places = db.Places.Find(id);
            if (places == null)
            {
                return HttpNotFound();
            }
            return View(places);
        }

        // GET: Places/Create
        public ActionResult Create()
        {
            ViewBag.DistrictId = new SelectList(db.Districts, "Id", "Name");
            ViewBag.PlaceTypeID = new SelectList(db.PlaceTypes, "Id", "Name");
            return View();
        }

        // POST: Places/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Name,DistrictId,AveragePrice,Address,Phone,PlaceTypeID,IsActive,Latitude,Longitude")] Places places)
        {
            if (ModelState.IsValid)
            {
                places.RecordDate = DateTime.Now;
                db.Places.Add(places);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.DistrictId = new SelectList(db.Districts, "Id", "Name", places.DistrictId);
            ViewBag.PlaceTypeID = new SelectList(db.PlaceTypes, "Id", "Name", places.PlaceTypeID);
            return View(places);
        }

        // GET: Places/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Places places = db.Places.Find(id);
            if (places == null)
            {
                return HttpNotFound();
            }
            ViewBag.DistrictId = new SelectList(db.Districts, "Id", "Name", places.DistrictId);
            ViewBag.PlaceTypeID = new SelectList(db.PlaceTypes, "Id", "Name", places.PlaceTypeID);
            return View(places);
        }

        // POST: Places/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Name,DistrictId,AveragePrice,Address,Phone,PlaceTypeID,IsActive,Latitude,Longitude")] Places places)
        {
            if (ModelState.IsValid)
            {
                places.RecordDate = DateTime.Now;
                db.Entry(places).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.DistrictId = new SelectList(db.Districts, "Id", "Name", places.DistrictId);
            ViewBag.PlaceTypeID = new SelectList(db.PlaceTypes, "Id", "Name", places.PlaceTypeID);
            return View(places);
        }

        // GET: Places/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Places places = db.Places.Find(id);
            if (places == null)
            {
                return HttpNotFound();
            }
            return View(places);
        }

        // POST: Places/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Places places = db.Places.Find(id);
            db.Places.Remove(places);
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

        public ActionResult Categories(int id)
        {
            var places = db.PlaceCategories.Where(x => x.PlaceId == id);
            ViewBag.SelectedPlace = db.Places.Find(id);
            return View(places.ToList());
        }

        public ActionResult CategoryCreate(int id)
        {
            ViewBag.CategoryId = new SelectList(db.Categories.Except(db.PlaceCategories.Where(x => x.PlaceId == id).Select(x => x.Categories)), "Id", "Name");
            ViewBag.PlaceId = new SelectList(db.Places.Where(x => x.Id == id), "Id", "Name");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CategoryCreate([Bind(Include = "Id,CategoryId,PlaceId,Note,IsActive")] PlaceCategories placeCategories)
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
        public ActionResult CategoryEdit(int? id)
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
            ViewBag.CategoryId = new SelectList(db.Categories.Where(x => x.Id == placeCategories.CategoryId), "Id", "Name", placeCategories.CategoryId);
            ViewBag.PlaceId = new SelectList(db.Places.Where(x => x.Id == placeCategories.PlaceId), "Id", "Name", placeCategories.PlaceId);
            return View(placeCategories);
        }

        // POST: PlaceCategories/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CategoryEdit([Bind(Include = "Id,CategoryId,PlaceId,Note,IsActive")] PlaceCategories placeCategories)
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

        [HttpGet]
        public ActionResult PlacesCategories(int id)
        {
            var allCategories = db.Categories.Where(x => x.IsActive);
            ViewBag.SelectedPlace = db.Places.Find(id);
            var placeCategories = db.PlaceCategories.Where(x => x.IsActive && x.PlaceId == id);
            foreach (Categories category in allCategories)
            {
                bool temp = false;
                PlaceCategories pCat = placeCategories.Where(x => x.CategoryId == category.Id && x.IsActive).FirstOrDefault();
                if (pCat != null)
                {
                    category.adminTempSelect = true;
                }
                else
                {
                    category.adminTempSelect = false;
                }

            }
            return View(allCategories);
        }

        [HttpPost]
        public ActionResult ChangeStatus(int id, int catId, bool status)
        {

            PlaceCategories pCat = db.PlaceCategories.Where(x => x.PlaceId == id && x.CategoryId == catId).FirstOrDefault();
            if (pCat != null)
            {
                pCat.IsActive = status;
            }
            else
            {
                pCat = new PlaceCategories();
                pCat.CategoryId = catId;
                pCat.PlaceId = id;
                pCat.IsActive = status;
                pCat.Note = "";
                db.PlaceCategories.Add(pCat);
            }
            db.SaveChanges();
            return Json(true);
        }

        [HttpGet]
        public ActionResult PlacesProperties(int id)
        {
            var allProperties = db.Properties.Where(x => x.IsActive);
            ViewBag.SelectedPlace = db.Places.Find(id);
            var placeProperties = db.PlaceProperties.Where(x => x.IsActive && x.PlaceId == id);
            foreach (Properties property in allProperties)
            {
                bool temp = false;
                PlaceProperties pProp = placeProperties.Where(x => x.PropertyId == property.Id && x.IsActive).FirstOrDefault();
                if (pProp != null)
                {
                    property.adminTempSelect = true;
                }
                else
                {
                    property.adminTempSelect = false;
                }

            }
            return View(allProperties);
        }

        [HttpPost]
        public ActionResult ChangePropertyStatus(int id, int propId, bool status)
        {

            PlaceProperties pProp = db.PlaceProperties.Where(x => x.PlaceId == id && x.PropertyId == propId).FirstOrDefault();
            if (pProp != null)
            {
                pProp.IsActive = status;
            }
            else
            {
                pProp = new PlaceProperties();
                pProp.PropertyId = propId;
                pProp.PlaceId = id;
                pProp.IsActive = status;
                db.PlaceProperties.Add(pProp);
            }
            db.SaveChanges();
            return Json(true);
        }
    }
}
