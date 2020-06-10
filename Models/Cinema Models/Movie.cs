using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;


namespace Firewalls.Models.Cinema_Models
{
    //Add Movies
    public class Movie
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [ScaffoldColumn(false)]
        [Display(Name = "Movie ID")]
        public int MovieID { get; set; }

        [Required, StringLength(50), Display(Name = "Name")]
        public string MovieName { get; set; }

        [Required, StringLength(20), Display(Name = "Genre")]
        public string Genre { get; set; }
        [Required, StringLength(10000), Display(Name = "Description"), DataType(DataType.MultilineText)]
        public string Description { get; set; }

        [Required, Display(Name = "Showing Date")]
        public string ShowingDate { get; set; }
        public byte[] Picture { get; set; }
        [ForeignKey("theatre")]
        public int Thea_id { get; set; }
        public virtual Theatre theatre { get; set; }



    }
  
    }

