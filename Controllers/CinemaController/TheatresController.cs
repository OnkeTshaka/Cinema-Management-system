using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Firewalls.Models;
using Firewalls.Models.Cinema_Models;

namespace Firewalls.Controllers.CinemaController
{
    public class TheatresController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Theatres
        public ActionResult Index()
        {
            return View(db.Theatres.ToList());
        }

        // GET: Theatres/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Theatre theatre = db.Theatres.Find(id);
            if (theatre == null)
            {
                return HttpNotFound();
            }
            return View(theatre);
        }

        // GET: Theatres/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Theatres/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Theatre_id,Theatre_name,City")] Theatre theatre)
        {
            if (ModelState.IsValid)
            {
                db.Theatres.Add(theatre);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(theatre);
        }

        // GET: Theatres/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Theatre theatre = db.Theatres.Find(id);
            if (theatre == null)
            {
                return HttpNotFound();
            }
            return View(theatre);
        }

        // POST: Theatres/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Theatre_id,Theatre_name,City")] Theatre theatre)
        {
            if (ModelState.IsValid)
            {
                db.Entry(theatre).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(theatre);
        }

        // GET: Theatres/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Theatre theatre = db.Theatres.Find(id);
            if (theatre == null)
            {
                return HttpNotFound();
            }
            return View(theatre);
        }

        // POST: Theatres/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Theatre theatre = db.Theatres.Find(id);
            db.Theatres.Remove(theatre);
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
