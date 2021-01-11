using Microsoft.EntityFrameworkCore;

namespace Synergy.ReliefCenter.Data.Contexts
{
    public class CrewWageContext : DbContext
    {
        public CrewWageContext(DbContextOptions<CrewWageContext> options) : base(options)
        {

        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            //ConfigurationHelper.SetUpConfiguration(optionsBuilder, "CrewWageString");
        }

        // For Seed Data
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // SeedDataHelper.RunSeedData(modelBuilder);
        }
    }
}
