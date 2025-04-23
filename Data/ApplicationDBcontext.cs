using System.Diagnostics;
using API.Models;
using Microsoft.EntityFrameworkCore;


namespace API.Data
{
    public class ApplicationDBcontext:DbContext
    {
        public ApplicationDBcontext(DbContextOptions dbContextOptions):base(dbContextOptions)
        {
            
        }

        public DbSet<Stock> Stock { get; set; }
        public DbSet<Comments> Comments { get; set; }
    }
}