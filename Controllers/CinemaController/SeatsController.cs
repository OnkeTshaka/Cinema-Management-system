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
    public class SeatsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Seats
        public ActionResult Index()
        {
            var seats = db.Seats.Include(s => s.movie).Include(s => s.theatre);
            return View(seats.ToList());
        }

        // GET: Seats/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Seat seat = db.Seats.Find(id);
            if (seat == null)
            {
                return HttpNotFound();
            }
            return View(seat);
        }

        // GET: Seats/Create
        public ActionResult Create()
        {
            ViewBag.MovieID = new SelectList(db.Movies, "MovieID", "MovieName");
            ViewBag.Thea_id = new SelectList(db.Theatres, "Theatre_id", "Theatre_name");
            return View();
        }

        // POST: Seats/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Seat_id,Seat_state,Seat_cost,MovieID,Thea_id")] Seat seat)
        {
            if (ModelState.IsValid)
            {
                db.Seats.Add(seat);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.MovieID = new SelectList(db.Movies, "MovieID", "MovieName", seat.MovieID);
            ViewBag.Thea_id = new SelectList(db.Theatres, "Theatre_id", "Theatre_name", seat.Thea_id);
            return View(seat);
        }

        // GET: Seats/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Seat seat = db.Seats.Find(id);
            if (seat == null)
            {
                return HttpNotFound();
            }
            ViewBag.MovieID = new SelectList(db.Movies, "MovieID", "MovieName", seat.MovieID);
            ViewBag.Thea_id = new SelectList(db.Theatres, "Theatre_id", "Theatre_name", seat.Thea_id);
            return View(seat);
        }

        // POST: Seats/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Seat_id,Seat_state,Seat_cost,MovieID,Thea_id")] Seat seat)
        {
            if (ModelState.IsValid)
            {
                db.Entry(seat).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.MovieID = new SelectList(db.Movies, "MovieID", "MovieName", seat.MovieID);
            ViewBag.Thea_id = new SelectList(db.Theatres, "Theatre_id", "Theatre_name", seat.Thea_id);
            return View(seat);
        }

        // GET: Seats/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Seat seat = db.Seats.Find(id);
            if (seat == null)
            {
                return HttpNotFound();
            }
            return View(seat);
        }

        // POST: Seats/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Seat seat = db.Seats.Find(id);
            db.Seats.Remove(seat);
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
