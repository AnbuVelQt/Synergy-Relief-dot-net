using Microsoft.EntityFrameworkCore;
using Synergy.ReliefCenter.Data.Entities;

namespace Synergy.ReliefCenter.Data.Contexts
{
    public class ReliefContext : DbContext
    {
        public ReliefContext(DbContextOptions<ReliefContext> options) : base(options)
        {

        }

        public DbSet<Contract> Contracts { get; set; }

        public DbSet<ContractForm> ContractForms { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            
        }
    }
}
