using Firewalls.Models.Cinema_Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System.Data.Entity;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Firewalls.Models
{
    // You can add profile data for the user by adding more properties to your ApplicationUser class, please visit https://go.microsoft.com/fwlink/?LinkID=317594 to learn more.
    public class ApplicationUser : IdentityUser
    {
        public string CustomerName { get; set; }
        public string LastName { get; set; }
        public string Number { get; set; }

        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser> manager)
        {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            // Add custom user claims here
            return userIdentity;
        }
    }

    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext()
            : base("DefaultConnection", throwIfV1Schema: false)
        {
        }
        //protected override void OnModelCreating(DbModelBuilder modelBuilder)
        //{
        //    //Configure default schema
        //    modelBuilder.Entity<AspNetUsers>().ToTable("StudentData");
        //    modelBuilder.Entity<Course>().ToTable("CourseDetail");
        //    modelBuilder.Entity<Enrollment>().ToTable("EnrollmentInfo");
        //} public virtual DbSet<Seat> Seats { get; set; }
        public virtual DbSet<Theatre> Theatres { get; set; }
        public virtual DbSet<Ticket> Tickets { get; set; }
        public virtual DbSet<watchTrailer> watchTrailers { get; set; }
        public virtual DbSet<Member> Members { get; set; }
        public virtual DbSet<Movie> Movies { get; set; }
        public virtual DbSet<Seat> Seats { get; set; }
        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }
    }
}