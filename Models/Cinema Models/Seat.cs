using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Firewalls.Models.Cinema_Models
{
    public class Seat
    {
        [Key]
        public int Seat_id { get; set; }
        public bool Seat_state { get; set; }
        public int Seat_cost { get; set; }
        [ForeignKey("movie")]
        public int? MovieID { get; set; }
        [ForeignKey("theatre")]
        public int? Thea_id { get; set; }

        public virtual Movie movie { get; set; }
        public virtual Theatre theatre { get; set; }
    }
}