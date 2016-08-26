///Created by: Bartul Kovačić
///Github: https:github.com/BKova
using Evidencija.Database.Models;
using Microsoft.EntityFrameworkCore;

namespace Evidencija.Database
{
    public class EvidencijaDbContext : DbContext
    {
        public EvidencijaDbContext() : base() { }

        public EvidencijaDbContext(DbContextOptions options) : base(options) { }

        public DbSet<User> Users { get; set; }

        public DbSet<TimeStamp> Stamps { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>()
                  .HasMany(user => user.UserTimeStamps)
                  .WithOne(stamp => stamp.User);
        }
    }
}
