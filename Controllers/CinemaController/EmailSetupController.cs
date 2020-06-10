using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Web;
using System.Web.Mvc;

namespace Firewalls.Controllers.CinemaController
{
    public class EmailSetupController : Controller
    {
        // GET: EmailSetup
        public ActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Index(Firewalls.Models.Email model, string email, string CustomerName, double TotalCost, DateTime BookingDate, string Cinema, string Mname, int Quantity, int BookingID)
        {
            MailMessage nn = new MailMessage("onktshaka200@gmail.com", model.To);
            model.To= email;
            nn.Subject = model.Subject;
            model.Subject = "ticket reciept: " + BookingID;
            nn.Body = model.Body;
            model.Body= "Dear " + CustomerName + "<br/>"
                + "<br/>"
                + "Please find below your details of your recent reservation: "
                + "<br/>"
                + "<br/>" + "Total Price       :" + "R" + TotalCost
                + "<br/>" + "Booking Date     :" + BookingDate
                + "<br/>" + "Theatre name     :" + Cinema
                + "<br/>" + "Movie Name     :" + Mname
                + "<br/>" + "Number of Tickets  :" + Quantity +
                "<br/>" +
                "<br/>" +
                "<br/>" +

                "Regards, " +
                "<br/>" +
                "Firewalls Team :)";

            nn.IsBodyHtml = false;

            SmtpClient smtp = new SmtpClient();
            smtp.Host = "smtp.gmail.com";
            smtp.Port = 587;
            smtp.EnableSsl = true;

            NetworkCredential nc = new NetworkCredential("onktshaka200@gmail.com", "Kick111Ass#@");
            smtp.UseDefaultCredentials = true;
            smtp.Credentials = nc;
            smtp.Send(nn);
            ViewBag.Message = "Mail has been sent";
            return View();
        }
    }
}