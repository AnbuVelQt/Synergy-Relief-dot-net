using Microsoft.EntityFrameworkCore;
using Synergy.ReliefCenter.Data.Entities.Vessel;

namespace Synergy.ReliefCenter.Data.Contexts
{
    public class VesselContext : DbContext
    {
        public VesselContext(DbContextOptions<VesselContext> options) : base(options) 
        {

        }

        public DbSet<FleetVessels> fleets_vessels { get; set; }

        public DbSet<Fleets> fleets { get; set; }

        public DbSet<Vessel> vessels { get; set; }
        public DbSet<VesselOwner> vessel_owners { get; set; }
        public DbSet<Ports> ports { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            //ConfigurationHelper.SetUpConfiguration(optionsBuilder, "VesselString");
        }
    }
}
