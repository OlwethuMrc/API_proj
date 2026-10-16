using API_proj.Controllers;
using API_proj.Models;
using Microsoft.EntityFrameworkCore;

namespace API_proj.Data
{
    public class ApplicationDBContext : DbContext
    {
        public ApplicationDBContext(DbContextOptions<ApplicationDBContext> options)
            : base(options)
        { 

        }
        public DbSet<API_proj.Models.User> Users { get; set; }
        public DbSet<API_proj.Models.Organiser> Organisers { get; set; }
        public DbSet<API_proj.Models.Participant> Participants { get; set; }
        public DbSet<API_proj.Models.Category> Categories { get; set; }
        public DbSet<API_proj.Models.Event> Events { get; set; }
        public DbSet<API_proj.Models.Enrolment> Enrolments { get; set; }
        public DbSet<API_proj.Models.Result> Results { get; set; }


        //DbSet<T> property here for each model,
        //e.g public DbSet<User> Users { get; set; }

       
    }
}
     
    

