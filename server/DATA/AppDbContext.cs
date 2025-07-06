using JaMoveo.Models;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics.Metrics;

namespace JaMoveo.DATA
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }
        public DbSet<InstrumentItem> Instruments => Set<InstrumentItem>();
        public DbSet<User> Users => Set<User>();
    }
}
