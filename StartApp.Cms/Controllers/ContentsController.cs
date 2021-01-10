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
    public class ContentsController : Controller
    {
        private StartAppEntities db = new StartAppEntities();

        // GET: Contents
        public ActionResult Index()
        {
            return View(db.Contents.ToList());
        }

        // GET: Contents/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Contents contents = db.Contents.Find(id);
            if (contents == null)
            {
                return HttpNotFound();
            }
            return View(contents);
        }

        // GET: Contents/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Contents/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Name,Description,ImageUrl,IsActive,MainPage,OrderBy")] Contents contents)
        {
            if (ModelState.IsValid)
            {
                contents.UpdatedDate = DateTime.Now;
                contents.RecordDate = DateTime.Now;
                db.Contents.Add(contents);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(contents);
        }

        // GET: Contents/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Contents contents = db.Contents.Find(id);
            if (contents == null)
            {
                return HttpNotFound();
            }
            return View(contents);
        }

        // POST: Contents/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Name,Description,ImageUrl,IsActive,UpdatedDate,MainPage,OrderBy,RecordDate")] Contents contents)
        {
            if (ModelState.IsValid)
            {
                db.Entry(contents).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(contents);
        }

        // GET: Contents/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Contents contents = db.Contents.Find(id);
            if (contents == null)
            {
                return HttpNotFound();
            }
            return View(contents);
        }

        // POST: Contents/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Contents contents = db.Contents.Find(id);
            db.Contents.Remove(contents);
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

        [HttpGet]
        public ActionResult AddImage(int id)
        {
            return View(db.Contents.Find(id));
        }

        [HttpPost]
        public ActionResult AddImage(int id, HttpPostedFileBase file)
        {
            UploadImage.UploadAndSave(file, UploadImageType.Content, id, 500, 500);
            return RedirectToAction("Index", "Contents");
        }
    }
}
