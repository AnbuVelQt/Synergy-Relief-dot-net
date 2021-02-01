using Microsoft.EntityFrameworkCore;
using Synergy.ReliefCenter.Data.Entities.Master;

namespace Synergy.ReliefCenter.Data.Contexts
{
    public class MasterDbContext : DbContext
    {
        public MasterDbContext(DbContextOptions<MasterDbContext> options) : base(options)
        {
        }

        public DbSet<ShipManagementCompanies> ShipManagementCompanies { get; set; }

        public DbSet<Rank> Ranks { get; set; }

        public DbSet<Country> Countries { get; set; }

        public DbSet<Nationality> Nationalities { get; set; }

        public DbSet<AccessPolicies> AccessPolicies { get; set; }

        public DbSet<PolicyRoles> PolicyRoles { get; set; }

        public DbSet<PolicyUsers> PolicyUsers { get; set; }
    }
}
