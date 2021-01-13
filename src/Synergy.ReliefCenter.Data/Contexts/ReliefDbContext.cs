using Microsoft.EntityFrameworkCore;
using Synergy.ReliefCenter.Data.Entities;

namespace Synergy.ReliefCenter.Data.Contexts
{
    public class ReliefDbContext : DbContext
    {
        public ReliefDbContext(DbContextOptions<ReliefDbContext> options) : base(options)
        {

        }

        public DbSet<Contract> Contracts { get; set; }

        public DbSet<ContractForm> ContractForms { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
           
        }
    }
}
