using SendGrid.Helpers.Mail;
using System.Net.Mail;
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
using Microsoft.AspNet.Identity;
using System.Text;

namespace Firewalls.Controllers.CinemaController
{[Authorize]
    public class BookingsController : Controller
    {
       
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Bookings
        [Authorize(Roles = "Admin")]
        public ActionResult Index()
        {
            return View(db.Bookings.ToList());
        }

        // GET: Bookings/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Booking booking = db.Bookings.Find(id);
            if (booking == null)
            {
                return HttpNotFound();
            }
            return View(booking);
        }

        // GET: Bookings/Create
        public ActionResult Create()
        {
            ApplicationDbContext db = new ApplicationDbContext();
            ViewBag.MovieID = new SelectList(db.Movies, "MovieID", "MovieName");
            ViewBag.MappingID = new SelectList(db.AssignMovies, "MappingID", "TheatreName", "hour");
            ;
            return View();
        }

        // POST: Bookings/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "BookingID,Quantity,BookingDate,MovieID,Mname,MovieCost,ShowDate,TheatreID,Cinema,MappingID,ShowTime,TotalCost")] Booking booking)
        {
            if (ModelState.IsValid)
            {
                var n = db.Users.ToList().Where(p => p.UserName == User.Identity.GetUserName()).Select(p => p.Email).FirstOrDefault(); ;
                var np = db.Users.ToList().Where(p => p.UserName == User.Identity.GetUserName()).Select(p => p.CustomerName).FirstOrDefault(); ;


                booking.BookingDate = DateTime.Now;
                booking.Mname = booking.getMovieName();
                booking.MovieCost = booking.getMoviePrice();
                booking.email = n;
                booking.cust = np;
                //booking.CustName = booking.GetCustomer();
                //booking.ShowDate = booking.getMovieDate();
                
                booking.Cinema = booking.getTheatre();
                //booking.hold = booking.CalcRefreshments();
                booking.storethename = booking.CalcReference();
                //string mail = "";
                //mail = booking.email;
                //booking.Hold = booking.CalcRefr();
                booking.seats = booking.CalcSeats();
                booking.ShowTime = booking.getTime();
                booking.TotalCost = booking.calcTotalCharge();
                db.Bookings.Add(booking);
                db.SaveChanges();
                Session["bookID"] = booking.BookingID;

                var nn = db.Users.ToList().Where(p => p.Email == User.Identity.GetUserName()).Select(p => p.CustomerName).FirstOrDefault();

                //Email emails = new Email();
                //emails.SendConfirmation(n, booking.cust,booking.TotalCost,booking.BookingDate,booking.Cinema,booking.Mname,booking.Quantity,booking.BookingID);
                Email emails = new Email();
                emails.Subject = booking.BookingID.ToString();
                emails.Body = (n, booking.cust, booking.TotalCost, booking.BookingDate, booking.Cinema, booking.Mname, booking.Quantity).ToString();
                TempData["AlertMessage"] = "Thank you for Booking in our Cinema, Here are your booking details";

                //return RedirectToAction("ConfirmBooking", new { id = booking.BookingID});
                return RedirectToAction("Details", new { id = booking.BookingID });

                //return RedirectToAction("Index");
            }
            ViewBag.MovieID = new SelectList(db.Movies, "MovieID", "MovieName");
            ViewBag.MappingID = new SelectList(db.AssignMovies, "MappingID", "TheatreName", "hour");

            return View(booking);
        }

        // GET: Bookings/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Booking booking = db.Bookings.Find(id);
            if (booking == null)
            {
                return HttpNotFound();
            }
            ViewBag.MovieID = new SelectList(db.Movies, "MovieID", "MovieName");
            ViewBag.MappingID = new SelectList(db.AssignMovies, "MappingID", "TheatreName", "hour");
            return View(booking);
        }

        // POST: Bookings/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "BookingID,Quantity,BookingDate,MovieID,Mname,MovieCost,ShowDate,TheatreID,Cinema,MappingID,ShowTime,TotalCost")] Booking booking)
        {
            if (ModelState.IsValid)
            {
                db.Entry(booking).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.MovieID = new SelectList(db.Movies, "MovieID", "MovieName");
            ViewBag.MappingID = new SelectList(db.AssignMovies, "MappingID", "TheatreName", "hour");

            return View(booking);
        }

        // GET: Bookings/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Booking booking = db.Bookings.Find(id);
            if (booking == null)
            {
                return HttpNotFound();
            }
            return View(booking);
        }

        // POST: Bookings/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Booking booking = db.Bookings.Find(id);
            db.Bookings.Remove(booking);
            db.SaveChanges();
            if(User.IsInRole("Admin"))
             {
                return RedirectToAction("Index");

            }
            else
            {
                return Redirect("~");
            }
            
            
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
        public ActionResult PayFast()
        {
            //string email = User.Identity.Name;
            int ID = int.Parse(Session["bookID"].ToString());
           // string cust = (Session["customerName"].ToString());

            Booking res = new Booking();

            res = db.Bookings.Find(ID);
           // res = db.Bookings.Find(cust);
            res.TotalCost = res.TotalCost;
            try
            {
                // Create the order in your DB and get the ID
                double amount = res.TotalCost;
                string orderId = new Random().Next(1, 9999).ToString();
                //string orderId = res.applicationUser.FirstName + "  " + res.applicationUser.LastName; 
                string name = $"Total Cost for Ticket Booking | Ref No.: {res.BookingID}";
                string description = "Payments For Booking";

                string site = "";
                string merchant_id = "";
                string merchant_key = "";

                // Check if we are using the test or live system
                string paymentMode = System.Configuration.ConfigurationManager.AppSettings["PaymentMode"];

                if (paymentMode == "test")
                {
                    site = "https://sandbox.payfast.co.za/eng/process?";
                    merchant_id = "10010464";
                    merchant_key = "r8y2nuhvkd3kd";
                }
                //if (paymentMode == "test")
                //{
                //    site = "https://sandbox.payfast.co.za/eng/process?";
                //    merchant_id = "10705100";
                //    merchant_key = "wrxuh9kx1umku";
                //}

                else if (paymentMode == "live")
                {
                    site = "https://www.payfast.co.za/eng/process?";
                    merchant_id = System.Configuration.ConfigurationManager.AppSettings["PF_MerchantID"];
                    merchant_key = System.Configuration.ConfigurationManager.AppSettings["PF_MerchantKey"];
                }
                else
                {
                    throw new InvalidOperationException("Cannot process payment if PaymentMode (in web.config) value is unknown.");
                }

                // Build the query string for payment site

                StringBuilder str = new StringBuilder();
                str.Append("merchant_id=" + HttpUtility.UrlEncode(merchant_id));
                str.Append("&merchant_key=" + HttpUtility.UrlEncode(merchant_key));
                str.Append("&return_url=" + HttpUtility.UrlEncode(System.Configuration.ConfigurationManager.AppSettings["PF_ReturnURL"]));
                str.Append("&cancel_url=" + HttpUtility.UrlEncode(System.Configuration.ConfigurationManager.AppSettings["PF_CancelURL"]));
                str.Append("&notify_url=" + HttpUtility.UrlEncode(System.Configuration.ConfigurationManager.AppSettings["PF_NotifyURL"]));

                str.Append("&m_payment_id=" + HttpUtility.UrlEncode(orderId));
                str.Append("&amount=" + HttpUtility.UrlEncode(amount.ToString()));
                str.Append("&item_name=" + HttpUtility.UrlEncode(name));
                str.Append("&item_description=" + HttpUtility.UrlEncode(description));

                // Redirect to PayFast
                TempData["payment"] = "Payment for Ticket Booking was successful";
                Response.Redirect(site + str.ToString());
                // RedirectToAction("guestDetails", "Reservations1");
            }
            catch (Exception ex)
            {
                // Handle your errors here (log them and tell the user that there was an error)
                Console.WriteLine(ex);
            }
            return View();
        }
        public ActionResult SPayFast()
        {
            var userId = User.Identity.Name;

            var email = User.Identity.Name;
            Booking res = new Booking();
            double amount = res.TotalCost;
            string details = "Payment For: " + "Firewalls";
            string orderId = new Random().Next(1, 99999).ToString();
            string name = "Movie, Order#" + orderId;
            string description = "Movies";


            string site = "";
            string merchant_id = "";
            string merchant_key = "";

            // Check if we are using the mmor live system
            string paymentMode = System.Configuration.ConfigurationManager.AppSettings["PaymentMode"];
            merchant_id = "10000100";
            merchant_key = "46f0cd694581a";

            if (paymentMode == "test")
            {
                site = "https://sandbox.payfast.co.za/eng/process?";
                merchant_id = "10000100";
                merchant_key = "46f0cd694581a";
            }
            //if (paymentMode == "test")
            //{
            //    site = "https://sandbox.payfast.co.za/eng/process?";
            //    merchant_id = "10705100";
            //    merchant_key = "wrxuh9kx1umku";
            //}
            else if (paymentMode == "live")
            {
                site = "https://www.payfast.co.za/eng/process?";
                merchant_id = System.Configuration.ConfigurationManager.AppSettings["PF_MerchantID"];
                merchant_key = System.Configuration.ConfigurationManager.AppSettings["PF_MerchantKey"];
            }
            else
            {
                throw new InvalidOperationException("Cannot process payment if PaymentMode (in web.config) value is unknown.");
            }
            // Build the query string for payment site

            StringBuilder str = new StringBuilder();
            str.Append("merchant_id=" + HttpUtility.UrlEncode(merchant_id));
            str.Append("&merchant_key=" + HttpUtility.UrlEncode(merchant_key));
            str.Append("&return_url=" + HttpUtility.UrlEncode(System.Configuration.ConfigurationManager.AppSettings["PF_ReturnURL"]));
            str.Append("&cancel_url=" + HttpUtility.UrlEncode(System.Configuration.ConfigurationManager.AppSettings["PF_CancelURL"]));
            //str.Append("&notify_url=" + HttpUtility.UrlEncode(System.Configuration.ConfigurationManager.AppSettings["PF_NotifyURL"]));

            str.Append("&m_payment_id=" + HttpUtility.UrlEncode(orderId));
            str.Append("&amount=" + HttpUtility.UrlEncode(amount.ToString()));
            str.Append("&item_name=" + HttpUtility.UrlEncode(name));
            str.Append("&item_description=" + HttpUtility.UrlEncode(description));

            // Redirect to PayFast
            Response.Redirect(site + str.ToString());

            return View();
        }

    }

}
