using Microsoft.EntityFrameworkCore;

namespace Synergy.ReliefCenter.Data.Contexts
{
    public class ManningContext : DbContext
    {
        public ManningContext(DbContextOptions<ManningContext> options) : base(options) 
        {

        }


        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            //ConfigurationHelper.SetUpConfiguration(optionsBuilder, "ManningString");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //// Map to the correct Chinook Database tables
            //modelBuilder.Entity<Artist>().ToTable("Artist", "public");
            //modelBuilder.Entity<Album>().ToTable("Album", "public");

            //// Chinook Database for PostgreSQL doesn't auto-increment Ids
            //modelBuilder.Conventions
            //    .Remove<StoreGeneratedIdentityKeyConvention>();
        }
    }
}
