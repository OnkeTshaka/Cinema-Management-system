using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Firewalls.Models.Cinema_Models
{
    public class Theatre
    {
        [Key]
        public int Theatre_id { get; set; }
        public string Theatre_name { get; set; }
        public string City { get; set; }
    }
}