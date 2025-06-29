using System.Diagnostics;
using API.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;


namespace API.Data
{
    public class ApplicationDBcontext:IdentityDbContext<AppUser>
    {
        public ApplicationDBcontext(DbContextOptions dbContextOptions):base(dbContextOptions)
        {
            
        }

        public DbSet<Stock> Stock { get; set; }
        public DbSet<Comments> Comments { get; set; }
        public DbSet<Portfolio> Portfolios { get; set; }
        
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            
            builder.Entity<Portfolio>(x=>x.HasKey(p=>new {p.AppUserId,p.StockId}));
            builder.Entity<Portfolio>()
                .HasOne(x=>x.AppUser)
                .WithMany(x=>x.Portfolios)
                .HasForeignKey(x=>x.AppUserId);
            
            builder.Entity<Portfolio>()
                .HasOne(x=>x.stock)
                .WithMany(x=>x.Portfolios)
                .HasForeignKey(x=>x.StockId);
            
            List<IdentityRole> roles = new List<IdentityRole>
            {
                new IdentityRole
                {
                    Name = "Admin",
                    NormalizedName = "ADMIN"
                },
                new IdentityRole
                {
                    Name="User",
                    NormalizedName = "USER"
                }
            };
            builder.Entity<IdentityRole>().HasData(roles);
        }
    }
}