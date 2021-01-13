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

        public DbSet<SeafarerContactDetails> ContactDetails { get; set; }


        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            //ConfigurationHelper.SetUpConfiguration(optionsBuilder, "SeafarerString");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //modelBuilder.Entity<Contract>(_ => _.Property(_=> _.CreatedBy)).
            modelBuilder.Entity<Seafarer>()
                    .Property(p => p.CreatedOn)
                    .HasColumnName("created_at")
                    .HasColumnType("DateTime");

            modelBuilder.Entity<Seafarer>()
                    .Property(p => p.UpdatedOn)
                    .HasColumnName("updated_at")
                    .HasColumnType("DateTime");
        }
    }
}
