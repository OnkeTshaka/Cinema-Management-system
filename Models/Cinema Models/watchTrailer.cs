using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Firewalls.Models.Cinema_Models
{
    public class watchTrailer
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Vid { get; set; }

        public string Vname { get; set; }

        public string Vpath { get; set; }
    }
}