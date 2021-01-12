using Microsoft.EntityFrameworkCore;
using Synergy.ReliefCenter.Data.Entities.Seafarer;

namespace Synergy.ReliefCenter.Data.Contexts
{
    public class SeafarerContext : DbContext
    {
        public SeafarerContext(DbContextOptions<SeafarerContext> options) : base(options) 
        { 
        }

        public DbSet<SeaExperience> SeaExperiences { get; set; }

        public DbSet<Seafarer> Seafarers { get; set; }

        public DbSet<SeafarerContactDetails> ContactDetails { get; set; }


        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            //ConfigurationHelper.SetUpConfiguration(optionsBuilder, "SeafarerString");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
        }
    }
}
