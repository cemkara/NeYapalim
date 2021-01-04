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

    public class MainCategoriesController : Controller
    {
        private StartAppEntities db = new StartAppEntities();

        // GET: MainCategories
        public ActionResult Index()
        {
            return View(db.MainCategories.ToList());
        }

        // GET: MainCategories/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MainCategories mainCategories = db.MainCategories.Find(id);
            if (mainCategories == null)
            {
                return HttpNotFound();
            }
            return View(mainCategories);
        }

        // GET: MainCategories/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: MainCategories/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Name,IconUrl,OrderNo,IsActive")] MainCategories mainCategories)
        {
            if (ModelState.IsValid)
            {
                db.MainCategories.Add(mainCategories);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(mainCategories);
        }

        // GET: MainCategories/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MainCategories mainCategories = db.MainCategories.Find(id);
            if (mainCategories == null)
            {
                return HttpNotFound();
            }
            return View(mainCategories);
        }

        // POST: MainCategories/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Name,IconUrl,OrderNo,IsActive")] MainCategories mainCategories)
        {
            if (ModelState.IsValid)
            {
                db.Entry(mainCategories).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(mainCategories);
        }

        // GET: MainCategories/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MainCategories mainCategories = db.MainCategories.Find(id);
            if (mainCategories == null)
            {
                return HttpNotFound();
            }
            return View(mainCategories);
        }

        // POST: MainCategories/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            MainCategories mainCategories = db.MainCategories.Find(id);
            db.MainCategories.Remove(mainCategories);
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
