using Microsoft.EntityFrameworkCore;
using API_proj.Models;

namespace API_proj.Data
{
    public class ApplicationDBContext : DbContext
    {
        public ApplicationDBContext(DbContextOptions<ApplicationDBContext> options)
            : base(options)
        {

        }
        //DbSet<T> property here for each model,
        //e.g public DbSet<User> Users { get; set; }
    }
}      
    

