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
    public class CategoriesController : Controller
    {
        private StartAppEntities db = new StartAppEntities();

        // GET: Categories
        public ActionResult Index()
        {
            var categories = db.Categories.Include(c => c.MainCategories);
            return View(categories.ToList());
        }

        // GET: Categories/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Categories categories = db.Categories.Find(id);
            if (categories == null)
            {
                return HttpNotFound();
            }
            return View(categories);
        }

        // GET: Categories/Create
        public ActionResult Create()
        {
            ViewBag.MainCategoryId = new SelectList(db.MainCategories, "Id", "Name");
            return View();
        }

        // POST: Categories/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,MainCategoryId,Name,IconUrl,OrderNo,IsActive")] Categories categories)
        {
            if (ModelState.IsValid)
            {
                db.Categories.Add(categories);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.MainCategoryId = new SelectList(db.MainCategories, "Id", "Name", categories.MainCategoryId);
            return View(categories);
        }

        // GET: Categories/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Categories categories = db.Categories.Find(id);
            if (categories == null)
            {
                return HttpNotFound();
            }
            ViewBag.MainCategoryId = new SelectList(db.MainCategories, "Id", "Name", categories.MainCategoryId);
            return View(categories);
        }

        // POST: Categories/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,MainCategoryId,Name,IconUrl,OrderNo,IsActive")] Categories categories)
        {
            if (ModelState.IsValid)
            {
                db.Entry(categories).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.MainCategoryId = new SelectList(db.MainCategories, "Id", "Name", categories.MainCategoryId);
            return View(categories);
        }

        // GET: Categories/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Categories categories = db.Categories.Find(id);
            if (categories == null)
            {
                return HttpNotFound();
            }
            return View(categories);
        }

        [HttpGet]
        public ActionResult CategoryProperties(int id)
        {
            var allProperties = db.Properties.Where(x => x.IsActive);
            ViewBag.SelectedCategory = db.Categories.Find(id);
            var categoryProperties = db.CategoryProperties.Where(x => x.IsActive && x.CategoryId== id);
            foreach (Properties property in allProperties)
            {
                bool temp = false;
                CategoryProperties pProp = categoryProperties.Where(x => x.PropertiesId == property.Id && x.IsActive).FirstOrDefault();
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

            CategoryProperties pCat = db.CategoryProperties.Where(x => x.CategoryId == id && x.PropertiesId == propId).FirstOrDefault();
            if (pCat != null)
            {
                pCat.IsActive = status;
            }
            else
            {
                pCat = new CategoryProperties();
                pCat.PropertiesId = propId;
                pCat.CategoryId = id;
                pCat.IsActive = status;
                db.CategoryProperties.Add(pCat);
            }
            db.SaveChanges();
            return Json(true);
        }


        // POST: Categories/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Categories categories = db.Categories.Find(id);
            db.Categories.Remove(categories);
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
