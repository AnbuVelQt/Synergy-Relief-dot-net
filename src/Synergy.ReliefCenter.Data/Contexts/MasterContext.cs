using Microsoft.EntityFrameworkCore;
using Synergy.ReliefCenter.Data.Entities.Master;

namespace Synergy.ReliefCenter.Data.Contexts
{
    public class MasterContext : DbContext
    {
        public MasterContext(DbContextOptions<MasterContext> options) : base(options)
        {

        }

        public DbSet<ShipManagementCompanies> ship_management_companies { get; set; }

        public DbSet<Rank> ranks { get; set; }

        public DbSet<Country> countries { get; set; }

        public DbSet<Nationality> nationalities { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {           
            //ConfigurationHelper.SetUpConfiguration(optionsBuilder, "MasterString");
        }
    }
}
