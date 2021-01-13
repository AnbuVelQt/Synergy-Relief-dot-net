using Microsoft.EntityFrameworkCore;
using Synergy.ReliefCenter.Data.Entities.VesselCenter;

namespace Synergy.ReliefCenter.Data.Contexts
{
    public class VesselDbContext : DbContext
    {
        public VesselDbContext(DbContextOptions<VesselDbContext> options) : base(options) 
        {
        }

        public DbSet<FleetVessels> FleetsVessels { get; set; }

        public DbSet<Fleets> Fleets { get; set; }

        public DbSet<Vessel> Vessels { get; set; }

        public DbSet<VesselOwner> VesselOwners { get; set; }

        public DbSet<Ports> Ports { get; set; }
    }
}
