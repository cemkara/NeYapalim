﻿using StartApp.Cms.App_Start;
using StartAppModel;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace StartApp.Cms.Controllers
{
    [_SessionControl]

    public class DistrictsController : Controller
    {
        private StartAppEntities db = new StartAppEntities();

        // GET: Districts
        public ActionResult Index()
        {
            var districts = db.Districts.Include(d => d.Cities);
            return View(districts.ToList());
        }

        // GET: Districts/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Districts districts = db.Districts.Find(id);
            if (districts == null)
            {
                return HttpNotFound();
            }
            return View(districts);
        }

        // GET: Districts/Create
        public ActionResult Create()
        {
            ViewBag.CityId = new SelectList(db.Cities, "Id", "Name");
            return View();
        }

        // POST: Districts/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Name,CityId,IsActive,Latitude,Longitude")] Districts districts)
        {
            if (ModelState.IsValid)
            {
                db.Districts.Add(districts);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.CityId = new SelectList(db.Cities, "Id", "Name", districts.CityId);
            return View(districts);
        }

        // GET: Districts/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Districts districts = db.Districts.Find(id);
            if (districts == null)
            {
                return HttpNotFound();
            }
            ViewBag.CityId = new SelectList(db.Cities, "Id", "Name", districts.CityId);
            return View(districts);
        }

        // POST: Districts/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Name,CityId,IsActive,Latitude,Longitude")] Districts districts)
        {
            if (ModelState.IsValid)
            {
                db.Entry(districts).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.CityId = new SelectList(db.Cities, "Id", "Name", districts.CityId);
            return View(districts);
        }

        // GET: Districts/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Districts districts = db.Districts.Find(id);
            if (districts == null)
            {
                return HttpNotFound();
            }
            return View(districts);
        }

        // POST: Districts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Districts districts = db.Districts.Find(id);
            db.Districts.Remove(districts);
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
