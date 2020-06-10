using Firewalls.Models;
using Firewalls.Models.Cinema_Models;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;

namespace Firewalls.Controllers.CinemaController
{
    public class AssignMoviesController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: AssignMovies
        public ActionResult Index()
        {
            return View(db.AssignMovies.ToList());
        }

        // GET: AssignMovies/Details/5
        [Authorize(Roles = "Admin")]
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AssignMovie assignMovie = db.AssignMovies.Find(id);
            if (assignMovie == null)
            {
                return HttpNotFound();
            }
            return View(assignMovie);
        }

        // GET: AssignMovies/Create
        [Authorize(Roles = "Admin")]
        public ActionResult Create()
        {
            ApplicationDbContext db = new ApplicationDbContext();
            ViewBag.MovieID = new SelectList(db.Movies, "MovieID", "MovieName");
            ViewBag.TheatreID = new SelectList(db.Theatres, "TheatreID", "TheatreName", "TheatreAddress");
            return View();
        }

        // POST: AssignMovies/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public ActionResult Create([Bind(Include = "MappingID,MovieID,MovieName,TheatreID,TheatreName,Hour,location")] AssignMovie assignMovie)
        {
            if (ModelState.IsValid)
            {
                assignMovie.Mnames = assignMovie.getMovieName();
                //assignMovie.theatreName = assignMovie.GetTheaterName();
                //assignMovie.location = assignMovie.GetAddress();

                db.AssignMovies.Add(assignMovie);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.MovieID = new SelectList(db.Movies, "MovieID", "MovieName");
            ViewBag.TheatreID = new SelectList(db.Theatres, "TheatreID", "TheatreName", "TheatreAddress");
            return View();
        }

        // GET: AssignMovies/Edit/5
        [Authorize(Roles = "Admin")]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AssignMovie assignMovie = db.AssignMovies.Find(id);
            if (assignMovie == null)
            {
                return HttpNotFound();
            }
            ViewBag.Movie = new SelectList(db.Movies, "MovieID", "MovieName");
            ViewBag.Theatre = new SelectList(db.Theatres, "TheatreID", "TheatreName", "TheatreAddress");
            return View(assignMovie);
        }

        // POST: AssignMovies/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public ActionResult Edit([Bind(Include = "MappingID,MovieName,TheatreName,Hour")] AssignMovie assignMovie)
        {
            if (ModelState.IsValid)
            {
                var db = new ApplicationDbContext();
                db.Entry(assignMovie).State = EntityState.Added;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(assignMovie);
        }

        // GET: AssignMovies/Delete/5
        [Authorize(Roles = "Admin")]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AssignMovie assignMovie = db.AssignMovies.Find(id);
            if (assignMovie == null)
            {
                return HttpNotFound();
            }
            return View(assignMovie);
        }

        // POST: AssignMovies/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public ActionResult DeleteConfirmed(int id)
        {
            AssignMovie assignMovie = db.AssignMovies.Find(id);
            db.AssignMovies.Remove(assignMovie);
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
