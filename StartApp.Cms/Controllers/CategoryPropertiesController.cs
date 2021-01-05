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
    public class CategoryPropertiesController : Controller
    {
        private StartAppEntities db = new StartAppEntities();

        // GET: CategoryProperties
        public ActionResult Index()
        {
            var categoryProperties = db.CategoryProperties.Include(c => c.Categories).Include(c => c.Properties);
            return View(categoryProperties.ToList());
        }

        // GET: CategoryProperties/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CategoryProperties categoryProperties = db.CategoryProperties.Find(id);
            if (categoryProperties == null)
            {
                return HttpNotFound();
            }
            return View(categoryProperties);
        }

        // GET: CategoryProperties/Create
        public ActionResult Create()
        {
            ViewBag.CategoryId = new SelectList(db.Categories, "Id", "Name");
            ViewBag.PropertiesId = new SelectList(db.Properties, "Id", "Name");
            return View();
        }

        // POST: CategoryProperties/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,CategoryId,PropertiesId,IsActive")] CategoryProperties categoryProperties)
        {
            if (ModelState.IsValid)
            {
                db.CategoryProperties.Add(categoryProperties);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.CategoryId = new SelectList(db.Categories, "Id", "Name", categoryProperties.CategoryId);
            ViewBag.PropertiesId = new SelectList(db.Properties, "Id", "Name", categoryProperties.PropertiesId);
            return View(categoryProperties);
        }

        // GET: CategoryProperties/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CategoryProperties categoryProperties = db.CategoryProperties.Find(id);
            if (categoryProperties == null)
            {
                return HttpNotFound();
            }
            ViewBag.CategoryId = new SelectList(db.Categories, "Id", "Name", categoryProperties.CategoryId);
            ViewBag.PropertiesId = new SelectList(db.Properties, "Id", "Name", categoryProperties.PropertiesId);
            return View(categoryProperties);
        }

        // POST: CategoryProperties/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,CategoryId,PropertiesId,IsActive")] CategoryProperties categoryProperties)
        {
            if (ModelState.IsValid)
            {
                db.Entry(categoryProperties).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.CategoryId = new SelectList(db.Categories, "Id", "Name", categoryProperties.CategoryId);
            ViewBag.PropertiesId = new SelectList(db.Properties, "Id", "Name", categoryProperties.PropertiesId);
            return View(categoryProperties);
        }

        // GET: CategoryProperties/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CategoryProperties categoryProperties = db.CategoryProperties.Find(id);
            if (categoryProperties == null)
            {
                return HttpNotFound();
            }
            return View(categoryProperties);
        }

        // POST: CategoryProperties/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            CategoryProperties categoryProperties = db.CategoryProperties.Find(id);
            db.CategoryProperties.Remove(categoryProperties);
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
