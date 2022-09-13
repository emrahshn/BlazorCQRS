using Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace Data.Context
{
    public class WafelsDbContext : DbContext
    {
        public WafelsDbContext(DbContextOptions<WafelsDbContext> options) : base(options)
        {
        }
        public DbSet<Orders> Orders { get; set; }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
        }
    }
}
