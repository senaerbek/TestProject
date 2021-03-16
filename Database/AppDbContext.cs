using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using TestProject.Models;

namespace TestProject.Database
{
    public class AppDbContext : IdentityDbContext<Users>
    {
        public AppDbContext(DbContextOptions options) : base(options)
        {
        }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.Entity<Marka>()
            .HasMany(c => c.Urun)
            .WithOne(e => e.Marka);

            builder.Entity<Urun>()
            .HasMany(c => c.Fiyat)
            .WithOne(e => e.Urun);
        }
        public DbSet<Marka> Marka { get; set; }
        public DbSet<Urun> Urun { get; set; }
        public DbSet<Fiyat> Fiyat { get; set; }

    }
}