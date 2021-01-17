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
    public class MailConfigsController : Controller
    {
        private StartAppEntities db = new StartAppEntities();

        // GET: MailConfigs
        public ActionResult Index()
        {
            return View(db.MailConfigs.ToList());
        }

        // GET: MailConfigs/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MailConfigs mailConfigs = db.MailConfigs.Find(id);
            if (mailConfigs == null)
            {
                return HttpNotFound();
            }
            return View(mailConfigs);
        }

        // GET: MailConfigs/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: MailConfigs/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,SmtpHost,SmtpPort,SmtpEmail,SmtpPassword,BccMail,FromMail,DisplayName")] MailConfigs mailConfigs)
        {
            if (ModelState.IsValid)
            {
                db.MailConfigs.Add(mailConfigs);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(mailConfigs);
        }

        // GET: MailConfigs/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MailConfigs mailConfigs = db.MailConfigs.Find(id);
            if (mailConfigs == null)
            {
                return HttpNotFound();
            }
            return View(mailConfigs);
        }

        // POST: MailConfigs/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,SmtpHost,SmtpPort,SmtpEmail,SmtpPassword,BccMail,FromMail,DisplayName")] MailConfigs mailConfigs)
        {
            if (ModelState.IsValid)
            {
                db.Entry(mailConfigs).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(mailConfigs);
        }

        // GET: MailConfigs/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MailConfigs mailConfigs = db.MailConfigs.Find(id);
            if (mailConfigs == null)
            {
                return HttpNotFound();
            }
            return View(mailConfigs);
        }

        // POST: MailConfigs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            MailConfigs mailConfigs = db.MailConfigs.Find(id);
            db.MailConfigs.Remove(mailConfigs);
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
