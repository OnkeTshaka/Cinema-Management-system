using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Firewalls.Models.Cinema_Models
{
    public class MyTicket
    {
        public string TicketNo { get; set; }
        public string moviename { get; set; }
        public string Seats { get; set; }
        public string time { get; set; }
        public string theatrename { get; set; }
        public string city { get; set; }
    }
}