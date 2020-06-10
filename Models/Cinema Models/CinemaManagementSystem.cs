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

        [Required, StringLength(50), Display(Name = "Movie Name")]
        public string MovieName { get; set; }

        [Required, StringLength(20), Display(Name = "Genre")]
        public string Genre { get; set; }

        [Required, StringLength(1000), Display(Name = "Cast"), DataType(DataType.MultilineText)]
        public string Cast { get; set; }


        [Required, StringLength(10000), Display(Name = "Movie Description"), DataType(DataType.MultilineText)]
        public string Description { get; set; }

        [Required, Display(Name = "Showing Date")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime ShowingDate { get; set; }


        [Display(Name = "Movie Image")]
        public byte[] Image { get; set; }


        [Display(Name = "Price"), Range(0,1000)]
        public double Amount { get; set; }

    }

    //Assign Movies class
    public class AssignMovie
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [ScaffoldColumn(false)]
        [Display(Name = "Mapping ID")]
        public int MappingID { get; set; }
        public virtual List<Movie> Movie { get; set; }

        [Display(Name = "Movie ID")]
        public int MovieID { get; set; }

        [Display(Name = "Movie Name")]
        public string Mnames { get; set; }

        [Required, Display(Name = "Show Time")]
        [DataType(DataType.Time)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:HH:mm}")]
        public DateTime Hour { get; set; }
        public virtual List<Theatre> Theatre { get; set; }
        public int TheatreID { get; set; }
        [Required, StringLength(50),Display(Name = " Theatre Name")]
        public string theatreName { get; set; }
        //[Display(Name = " Theatre Address")]
        //public string location { get; set; }

        public string getMovieName()
        {
            ApplicationDbContext db = new ApplicationDbContext();
            var gtName = (from m in db.Movies
                          where m.MovieID == MovieID
                          select m.MovieName
                          ).FirstOrDefault();

            return gtName;
        }


        //public string GetAddress()
        //{
        //    ApplicationDbContext db = new ApplicationDbContext();
        //    var GtAddress = (from m in db.Theatres
        //                         where m.TheatreID == TheatreID
        //                         select m.TheatreAddress
        //                  ).FirstOrDefault();

        //    return GtAddress;
        //}
    }


    //Theatre
    public class Theatre
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [ScaffoldColumn(false)]
        [Display(Name = "Theatre ID")]
        public int TheatreID { get; set; }
        [Required, StringLength(50), Display(Name = "Theatre Name")]

        public string TheatreName { get; set; }

        //[Required, StringLength(10000), Display(Name = "Theatre Address"),
        //DataType(DataType.MultilineText)]
        //public string TheatreAddress { get; set; }


        [Required, StringLength(10), Display(Name = "Theatre Manager")]
        public string Manager { get; set; }

        public string ChooseThreater()
        {
            string ret = "";
            if (TheatreName == "ML Sultan")
            {
                ret = "ML Sultan";
            }
            else if (TheatreName == "Ritson")
            {
                ret = "Ritson";
            }
            else if (TheatreName == "Steve Biko")
            {
                ret = "Steve Biko";
            }
            return ret;
        }
    }






    ///Booking

    public class Booking
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]

        [Display(Name = "BookingID")]
        public int BookingID { get; set; }
        //public string Id { get; set; }
        //public virtual List<RegisterViewModel> registerviewmodel { get; set; }

        //[Display(Name ="Please Enter your NickName")]

        //public string CustNames { get; set; }
        [Required,Display(Name = "Number Of Tickets"),Range(0,20)]
        public int Quantity { get; set; }

        [Display(Name = "Booking Date")]
        public DateTime BookingDate { get; set; }

        [Display(Name = "MovieID")]
        public int MovieID { get; set; }
        public virtual List<Movie> Movies { get; set; }

        [Display(Name = "Movie Name")]
        public string Mname { get; set; }

        [Display(Name = "Movie Cost")]
        public double MovieCost { get; set; }

        [Display(Name = "Your Reference")]
        public string storethename { get; set; }
        //public DateTime ShowDate { get; set; }

        
        public int TheatreID { get; set; }
        public virtual List<Theatre> Theatres { get; set; }


        public string Cinema { get; set; }
        [Display(Name ="Your Email Address")]
        public string email { get; set; }

        [Display(Name ="Name")]
        public string cust { get; set; }

        public int MappingID { get; set; }
        public virtual List<AssignMovie> AssignMovies { get; set; }

        [Display(Name = "Show Time")]
        public string ShowTime { get; set; }

        [Display(Name = "Total Cost")]
        public double TotalCost { get; set; }
        [Display(Name ="Seats Allocated")]
        public string seats { get; set; }

        //[Display(Name ="Add Refreshments?-R50")]
        //public bool Refreshments { get; set; }
        //public double hold { get; set; }
  



        // Get Movie Name FROM MOVIE CLASS
        public string getMovieName()
        {
            ApplicationDbContext db = new ApplicationDbContext();
            var gtName = (from m in db.Movies
                          where m.MovieID == MovieID
                          select m.MovieName
                          ).FirstOrDefault();

            return gtName;
        }


        // Get Movie Price FROM MOVIE CLASS

        //public string GetCustomer()
        //{
        //    ApplicationDbContext db = new ApplicationDbContext();
        //    var gtCust = (from m in db.Users
        //                  where m.Id == Id
        //                  select m.CustomerName
        //                   ).FirstOrDefault();

        //    return gtCust;
        //}




        public double getMoviePrice()
        {
            ApplicationDbContext db = new ApplicationDbContext();
            var gtPrice = (from m in db.Movies
                           where m.MovieID == MovieID
                           select m.Amount
                          ).FirstOrDefault();

            return gtPrice;
        }

        // Get ShowingDate FROM MOVIE CLASS
        //public DateTime getMovieDate()
        //{
        //    ApplicationDbContext db = new ApplicationDbContext();
        //    var gtMovieDate = (from m in db.Movies
        //                  where m.MovieID == MovieID
        //                  select m.ShowingDate
        //                  ).FirstOrDefault();

        //    return gtMovieDate;
        //}

        // Get Theatre Name FROM THEATRE CLASS
        public string getTheatre()
        {
            ApplicationDbContext db = new ApplicationDbContext();
            var gtTheatre = (from m in db.AssignMovies
                             where m.MappingID == MappingID
                             select m.theatreName
                          ).FirstOrDefault();

            return gtTheatre;
        }

        // Get Time FROM ASSIGN MOVIE CLASS
        public string getTime()
        {
            ApplicationDbContext db = new ApplicationDbContext();
            var gtTime = (from m in db.AssignMovies
                          where m.MappingID == MappingID
                          select m.Hour
                           ).FirstOrDefault();

            return gtTime.ToString();
        }

        public string CalcReference()
        {
            storethename =cust+"-"+ Quantity.ToString() + "-" + Cinema + "-" + Mname + "-" + BookingDate.ToString();
            return storethename;
        }
        //public double CalcRefr()
        //{

        //    if (Refresh == true)
        //    {
        //        Hold = 75;
        //    }
        //    return Hold;
        //}

        //public double CalcRefreshments()
        //{
        //    if (Refreshments == true)
        //    {
        //        hold = 50;
        //    }
        //    else
        //        hold = 0;
        //    return hold;

        //}
        public double calcTotalCharge()
        {

            //if (Refreshments == true)
            //{

            //    TotalCost = Quantity * (getMoviePrice() + 75);
            //}
            //else
                TotalCost = Quantity * getMoviePrice();

            return TotalCost;
        }
        public string CalcSeats()
        {
            if (Quantity == 1)
            {
                seats = "A1";
            }
            else if (Quantity == 2)
            {
                seats = "A2 , A3";
            }
            else if (Quantity == 3)
            {
                seats = "A4 , A5 , A6";
            }
            else if (Quantity == 4)
            {
                seats = "A7 , A8 , B1 , B2";
            }
            else if (Quantity == 5)
            {
                seats = "B3 , B4 , B5 , B6 , B7";
            }
            else if (Quantity == 6)
            {
                seats = "B8 , C1 , C2 , C3 , C4 , C5";
            }
            else if (Quantity == 7)
            {
                seats = "C6 , C7 , C8 , D1 , D2, D3 , D4";
            }
            else if (Quantity == 8)
            {
                seats = "D5 , D6 , D7 , D8 , E1 , E2 , E3 , E4";
            }
            else if (Quantity == 9)
            {
                seats = "E5 , E6 , E7 , E8 , F1 , F2 , F3 , F4 , F5";
            }
            else if (Quantity == 10)
            {
                seats = "F6 , F7 , F8 , G1 , G2 , G3 , G4 , G5 , G6, G7";
            }
            else if (Quantity == 11)
            {
                seats = "G8 , H1 , H2 , H3 , H4 , H5 , H6 , H7 , H8 , J1, J2 ";
            }
            else if (Quantity == 12)
            {
                seats = "J3 , J4 , J5 , J6 , J7 , J8 , K1 , K2 , K3 , K4, K5 , K6 , K7";
            }
            else if (Quantity == 13)
            {
                seats = "K8 , L1 , L2 , L3 , L4 , L5 , L6 , L7 , L8 , M1, M2 , M4 , M5 , M6 ";
            }
            else if (Quantity == 14)
            {
                seats = "M7 , M8 , N1 , N2 , N3 , N4 , N5 , N6 , N7 , N8 , O1 , O2 , O3 , O4 , O5";
            }
            else if (Quantity == 15)
            {
                seats = "O6 , O7 , O8 , P1 , P2 , P3 , P4 , P5 , P6 , P7 , P8 , Q1 , Q2 , Q3 , Q4 , ";
            }
            else
                seats = "Our Cinema Can Only Acoomodate 15 People";
            return seats;
        }














    }


}