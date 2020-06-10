using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Firewalls.Models.Cinema_Models
{
    public class Ticket
    {
        [Key]
        public int Ticket_id { get; set; }

        public string T_NO { get; set; }
        [ForeignKey("seat")]
        public int? Seat_id { get; set; }
        [ForeignKey("Member")]
        public int? ID { get; set; }

        public virtual Seat seat { get; set; }
        public virtual Member Member { get; set; }
    }
}