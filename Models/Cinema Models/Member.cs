using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Firewalls.Models.Cinema_Models
{
    public class Member
    {
        public int ID { get; set; }

        [Required(ErrorMessage = "Seller Email is required.")]
        [EmailAddress]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Seller Username is required to differentiate.")]
        [Display(Name = "Username")]
        public string Username { get; set; }

        [Required(ErrorMessage = "Seller Name is a must required.")]
        [Display(Name = "Name")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Mobile number is required to contact the seller for their Ads.")]
        [Display(Name = "Mobile Number")]
        public string MobileNumber { get; set; }

        [Required(ErrorMessage = "Joining Date of Seller is required to keep record.")]
        [Display(Name = "Joining Date")]
        public string JoinDate { get; set; }

    }
}