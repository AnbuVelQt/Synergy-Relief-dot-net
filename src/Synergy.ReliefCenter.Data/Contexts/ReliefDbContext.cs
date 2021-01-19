using Microsoft.EntityFrameworkCore;
using Synergy.ReliefCenter.Core.Models;
using Synergy.ReliefCenter.Data.Entities;
using System;

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
            modelBuilder
             .Entity<Contract>()
             .Property(e => e.Status)
             .HasConversion(
                 v => v.ToString(),
                 v => (ContractStatus)Enum.Parse(typeof(ContractStatus), v));
        }
    }
}
