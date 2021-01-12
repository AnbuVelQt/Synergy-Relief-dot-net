using Microsoft.EntityFrameworkCore;
using Synergy.ReliefCenter.Data.Entities.Seafarer;

namespace Synergy.ReliefCenter.Data.Contexts
{
    public class SeafarerDbContext : DbContext
    {
        public SeafarerDbContext(DbContextOptions<SeafarerDbContext> options) : base(options) 
        { 
        }

        public DbSet<SeaExperience> SeaExperiences { get; set; }

        public DbSet<Seafarer> Seafarers { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            //ConfigurationHelper.SetUpConfiguration(optionsBuilder, "SeafarerString");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
        }
    }
}
