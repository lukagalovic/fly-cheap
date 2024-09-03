using FlyCheap.API.Entities;
using Microsoft.EntityFrameworkCore;

namespace FlyCheap.API.Data
{
    public class FlyCheapDbContext : DbContext
    {
        public FlyCheapDbContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<Airport> Airports { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
    }
}
