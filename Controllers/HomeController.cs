using Firewalls.Models;
using System.Linq;
using System.Web.Mvc;

namespace Firewalls.Controllers
{
    public class HomeController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        public ActionResult Welcome()
        {
            return View();
        }

        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Booking()
        {
            return View();
        }
        //public ActionResult BookNow(int MovieID)
        //{
        //    BookNowViewModel vm = new BookNowViewModel();
        //    var item = db.Movies.Where(a => a.MovieID == MovieID).FirstOrDefault();
        //    vm.MovieName = item.MovieName;
        //    vm.ShowingDate = item.ShowingDate;
        //    vm.MovieID = item.MovieID;
        //    return View(vm);
        //}


        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}