using SendGrid;
using SendGrid.Helpers.Mail;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Configuration;
using System.Linq;
using System.Net.Mail;
using System.Web;


namespace Firewalls.Models
{
    public class Email
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int MailId { get; set; }
        public string To { get; set; }
        public string From { get; set; }
        public string Subject { get; set; }
        public string Body { get; set; }
    }
    //    //overload with what you want to show in the email.
    //    public void SendConfirmation(string email, string CustomerName, double TotalCost, DateTime BookingDate, string Cinema, string Mname, int Quantity, int BookingID)
    //    {
    //        //try
    //        //{
    //        //    var myMessage = new SendGridMessage
    //        //    {
    //        //        From = new EmailAddress("no-reply@homify.co.za", "Firewalls")
    //        //    };


    //        //myMessage.AddTo(email);
    //        string subject = "ticket reciept: " + BookingID;
    //        string body = (
    //            "Dear " + CustomerName + "<br/>"
    //            + "<br/>"
    //            + "Please find below your details of your recent reservation: "
    //            + "<br/>"
    //            + "<br/>" + "Total Price       :" + "R" + TotalCost
    //            + "<br/>" + "Booking Date     :" + BookingDate
    //            + "<br/>" + "Theatre name     :" + Cinema
    //            + "<br/>" + "Movie Name     :" + Mname
    //            + "<br/>" + "Number of Tickets  :" + Quantity +
    //            "<br/>" +
    //            "<br/>" +
    //            "<br/>" +

    //            "Regards, " +
    //            "<br/>" +
    //            "Firewalls Team ");
    //    }
    //}
                //myMessage.Subject = subject;
                //myMessage.HtmlContent = body;

    //            var transportWeb = new SendGrid.SendGridClient("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");

    //            transportWeb.SendEmailAsync(myMessage);
    //        }
    //        catch (Exception x)
    //        {
    //            Console.WriteLine(x);
    //        }

    //    }

    //}


}
