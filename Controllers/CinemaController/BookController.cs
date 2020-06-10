using Firewalls.Models;
using Firewalls.Models.Cinema_Models;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Firewalls.Controllers
{
    public class BookController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        // GET: Book
       
       public ActionResult Shows(string search)
       {
            ViewData["city"] = search;
            IList<string> movies = new List<string>();
            var moviess = (from m in db.Movies where m.theatre.City == search select m.MovieName).Distinct();
            
            foreach (string movie in moviess)
                {
                    movies.Add(movie);
                }
            return View(movies);
       }
       
       public ActionResult showtheatres(string moviename,string cityname)
        {
            ViewData["movie"] = moviename;
            ViewData["city"] = cityname;
            ViewData["abc"]= (from m in db.Movies where m.MovieName == moviename && m.theatre.City == cityname select new temp { ab=m.ShowingDate, bc=m.theatre.Theatre_name }).ToList();
            var movies = db.Movies.ToList();
            return View(movies);
        }

        public ActionResult showseats(string showtime,string moviename,string theatrename)
        {
            ViewData["movie"] = moviename;
            ViewData["theatre"] = theatrename;
            ViewData["show"] = showtime;
            Session["thea"] =theatrename;
            Session["mov"] =moviename;
            var aa= from a in db.Seats where (a.theatre.Theatre_name == theatrename && a.Seat_state==true && a.movie.MovieName == moviename) select a.Seat_id;
            
            return View(aa);
        }

        public ActionResult createticket(string mystr)
        {
            Random randm = new Random();
            string upr = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
            string downr = "abcdefghijklmnopqrstuvwxyz";
            string digir = "1234567890";

            string thn = (string)Session["thea"];
            string move = (string)Session["mov"];
            var aa = (from a in db.Seats where (a.theatre.Theatre_name == thn &&  a.movie.MovieName == move) select a.Seat_id).FirstOrDefault();
            int diff = aa;
            var abc = from a in db.Seats where (a.theatre.Theatre_name == thn && a.Seat_state == true && a.movie.MovieName == move) select a.Seat_id;
            
            List<int> mylist2 = new List<int>();
            foreach (int ii in abc)
            {
                if (ii % 100 == 0)
                    mylist2.Add(100);
                else
                    mylist2.Add(ii % 100);
            }

            char[] tno = new char[8];
            int r1 = randm.Next(0,25);
            int r2 = randm.Next(0, 25);
            int r3 = randm.Next(0,9);
            tno[0] = upr[r1];
            tno[1] = downr[r2];
            tno[2] = digir[r3];
            r1 = randm.Next(0, 25);
            r2 = randm.Next(0, 25);
            r3 = randm.Next(0, 9);
            tno[3] = upr[r2];
            tno[4] = downr[r1];
            tno[5] = digir[r3];
            string t_no = new string(tno);
            string CurrentUserName = User.Identity.GetUserName();
            Member member = db.Members.Where(s => s.Username == CurrentUserName).FirstOrDefault();
            int user_id2 = member.ID;
            for (int i=0;i<100;i++)
            {
                
                if (mystr[i] == '1' && !mylist2.Contains(i+1))
                {

                    Seat result = (from p in db.Seats
                                     where p.Seat_id == (diff+i)
                                     select p).SingleOrDefault();

                    result.Seat_state = true;
                    Ticket ticket = new Ticket()
                    {
                        T_NO=t_no,
                        Seat_id=diff+i,
                        ID=user_id2
                    };
                    db.Tickets.Add(ticket);
                    db.SaveChanges();
                }
            }


            return Redirect("/Book/My_Shows");
        }

        public ActionResult My_Shows()
        {
            string CurrentUserName = User.Identity.GetUserName();
            Member member = db.Members.Where(s => s.Username == CurrentUserName).FirstOrDefault();
            int user = member.ID;
            List<MyTicket> aa = new List<MyTicket>();
            var allticket = from t in db.Tickets where t.ID == user select t;
            string tids="rp";
            MyTicket mm = new MyTicket();
            string seat;
            foreach (Ticket tt in allticket)
            {
                if(tids=="rp")
                {
                    tids = tt.T_NO;
                    mm.TicketNo = tids;
                    mm.moviename = tt.seat.movie.MovieName;
                    if (tt.Seat_id % 100 == 0)
                        seat = "100";
                    else
                        seat = (tt.Seat_id % 100).ToString();
                    mm.Seats = seat + "  ";
                    mm.time = tt.seat.movie.ShowingDate;
                    mm.theatrename = tt.seat.theatre.Theatre_name;
                    mm.city = tt.seat.theatre.City;
                    

                }
                else if(tids!=tt.T_NO)
                {
                    aa.Add(mm);
                    mm = new MyTicket();
                    tids = tt.T_NO;
                    mm.TicketNo = tids;
                    mm.moviename = tt.seat.movie.MovieName;
                    if (tt.Seat_id % 100 == 0)
                        seat = "100";
                    else
                        seat = (tt.Seat_id % 100).ToString();
                    mm.Seats = tt.Seat_id + "  ";
                    mm.time = tt.seat.movie.ShowingDate;
                    mm.theatrename = tt.seat.theatre.Theatre_name;
                    mm.city = tt.seat.theatre.City;
                    
                }
                else
                {
                    mm.Seats += "  " + tt.Seat_id;
                }
                
            }

            if(tids!="rp")
                aa.Add(mm);
            return View(aa);
        }

        public ActionResult Cancel(string cancel)
        {
            string tick_id=cancel;
            var aa = from t in db.Tickets where t.T_NO == tick_id select t;
            int seat;
            foreach (var ticket in aa)
            {
                seat = (int)ticket.Seat_id;
                Seat ss = (from s in db.Seats where s.Seat_id == seat select s).First();
                ss.Seat_state = false;
                db.Tickets.Remove(ticket);
                
            }
            db.SaveChanges();
            return Redirect("/Book/My_Shows");
        }
    }
}