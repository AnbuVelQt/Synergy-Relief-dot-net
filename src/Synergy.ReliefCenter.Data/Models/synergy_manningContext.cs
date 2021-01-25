using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

#nullable disable

namespace Synergy.ReliefCenter.Data.Models
{
    public partial class synergy_manningContext : DbContext
    {
        public synergy_manningContext(DbContextOptions<synergy_manningContext> options)
            : base(options)
        {
        }

        public virtual DbSet<AgentLetter> AgentLetters { get; set; }
        public virtual DbSet<AgentNotificationLog> AgentNotificationLogs { get; set; }
        public virtual DbSet<ArInternalMetadatum> ArInternalMetadata { get; set; }
        public virtual DbSet<Audit> Audits { get; set; }
        public virtual DbSet<AvailabilityRequest> AvailabilityRequests { get; set; }
        public virtual DbSet<DepartureChecklist> DepartureChecklists { get; set; }
        public virtual DbSet<FeedbackHistory> FeedbackHistories { get; set; }
        public virtual DbSet<FleetCombinationMatrix> FleetCombinationMatrices { get; set; }
        public virtual DbSet<Interview> Interviews { get; set; }
        public virtual DbSet<OpenCase> OpenCases { get; set; }
        public virtual DbSet<RankCombination> RankCombinations { get; set; }
        public virtual DbSet<RecommendationList> RecommendationLists { get; set; }
        public virtual DbSet<Relief> Reliefs { get; set; }
        public virtual DbSet<SchemaMigration> SchemaMigrations { get; set; }
        public virtual DbSet<SeafarerChecklist> SeafarerChecklists { get; set; }
        public virtual DbSet<SeafarerDeparture> SeafarerDepartures { get; set; }
        public virtual DbSet<SeafarerReliefRequest> SeafarerReliefRequests { get; set; }
        public virtual DbSet<ShoreEmployeeDeviceToken> ShoreEmployeeDeviceTokens { get; set; }
        public virtual DbSet<ShoreEmployeeNotificationLog> ShoreEmployeeNotificationLogs { get; set; }
        public virtual DbSet<ShortlistedSeafarer> ShortlistedSeafarers { get; set; }
        public virtual DbSet<Status> Statuses { get; set; }
        public virtual DbSet<StatusLog> StatusLogs { get; set; }
        public virtual DbSet<Tag> Tags { get; set; }
        public virtual DbSet<Tagging> Taggings { get; set; }
        public virtual DbSet<TravelDocument> TravelDocuments { get; set; }
        public virtual DbSet<TravelDocumentList> TravelDocumentLists { get; set; }
        public virtual DbSet<TravelTicketRequest> TravelTicketRequests { get; set; }
        public virtual DbSet<UnmatchedSeafarer> UnmatchedSeafarers { get; set; }
        public virtual DbSet<VesselCombinationMatrix> VesselCombinationMatrices { get; set; }
        public virtual DbSet<VesselContract> VesselContracts { get; set; }

        public virtual DbSet<ContractForm> ContractForm { get; set; }

        public virtual DbSet<ContractReviewer> ContractReviewers { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {                
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "en_US.UTF-8");

            modelBuilder.Entity<AgentLetter>(entity =>
            {
                entity.HasOne(d => d.Relief)
                    .WithMany(p => p.AgentLetters)
                    .HasForeignKey(d => d.ReliefId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_rails_759038253d");
            });

            modelBuilder.Entity<AgentNotificationLog>(entity =>
            {
                entity.Property(e => e.Status).HasDefaultValueSql("0");
            });

            modelBuilder.Entity<ArInternalMetadatum>(entity =>
            {
                entity.HasKey(e => e.Key)
                    .HasName("ar_internal_metadata_pkey");
            });

            modelBuilder.Entity<Audit>(entity =>
            {
                entity.Property(e => e.AppName).HasDefaultValueSql("'Ahoy'::character varying");
            });

            modelBuilder.Entity<AvailabilityRequest>(entity =>
            {
                entity.Property(e => e.Status).HasDefaultValueSql("0");
            });

            modelBuilder.Entity<FleetCombinationMatrix>(entity =>
            {
                entity.Property(e => e.IsAppraisalBased).HasDefaultValueSql("false");

                entity.Property(e => e.IsSalaryBased).HasDefaultValueSql("false");

                entity.HasOne(d => d.RankCombination)
                    .WithMany(p => p.FleetCombinationMatrices)
                    .HasForeignKey(d => d.RankCombinationId)
                    .HasConstraintName("fk_rails_a4cbe22a41");
            });

            modelBuilder.Entity<RecommendationList>(entity =>
            {
                entity.Property(e => e.IsSystemGenerated).HasDefaultValueSql("false");

                entity.HasOne(d => d.Relief)
                    .WithMany(p => p.RecommendationLists)
                    .HasForeignKey(d => d.ReliefId)
                    .HasConstraintName("fk_rails_6e39a05c1d");
            });

            modelBuilder.Entity<Relief>(entity =>
            {
                entity.HasOne(d => d.VesselContract)
                    .WithMany(p => p.Reliefs)
                    .HasForeignKey(d => d.VesselContractId)
                    .HasConstraintName("fk_rails_0fce2aacd2");
            });

            modelBuilder.Entity<SchemaMigration>(entity =>
            {
                entity.HasKey(e => e.Version)
                    .HasName("schema_migrations_pkey");
            });

            modelBuilder.Entity<SeafarerChecklist>(entity =>
            {
                entity.Property(e => e.IsCompleted).HasDefaultValueSql("false");
            });

            modelBuilder.Entity<SeafarerReliefRequest>(entity =>
            {
                entity.Property(e => e.Status).HasDefaultValueSql("0");
            });

            modelBuilder.Entity<ShoreEmployeeNotificationLog>(entity =>
            {
                entity.Property(e => e.Read).HasDefaultValueSql("false");

                entity.Property(e => e.Status).HasDefaultValueSql("0");
            });

            modelBuilder.Entity<ShortlistedSeafarer>(entity =>
            {
                entity.HasOne(d => d.Relief)
                    .WithMany(p => p.ShortlistedSeafarers)
                    .HasForeignKey(d => d.ReliefId)
                    .HasConstraintName("fk_rails_1b45fbe51e");
            });

            modelBuilder.Entity<Tag>(entity =>
            {
                entity.Property(e => e.TaggingsCount).HasDefaultValueSql("0");
            });

            modelBuilder.Entity<Tagging>(entity =>
            {
                entity.HasOne(d => d.Tag)
                    .WithMany(p => p.Taggings)
                    .HasForeignKey(d => d.TagId)
                    .HasConstraintName("fk_rails_9fcd2e236b");
            });

            modelBuilder.Entity<TravelDocument>(entity =>
            {
                entity.HasOne(d => d.Relief)
                    .WithMany(p => p.TravelDocuments)
                    .HasForeignKey(d => d.ReliefId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_rails_cf196a1cf9");

                entity.HasOne(d => d.TravelDocumentList)
                    .WithMany(p => p.TravelDocuments)
                    .HasForeignKey(d => d.TravelDocumentListId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_rails_6fd76d1d05");
            });

            modelBuilder.Entity<TravelTicketRequest>(entity =>
            {
                entity.Property(e => e.EmailStatus).HasDefaultValueSql("0");

                entity.Property(e => e.TravelMode).HasDefaultValueSql("0");

                entity.HasOne(d => d.Relief)
                    .WithMany(p => p.TravelTicketRequests)
                    .HasForeignKey(d => d.ReliefId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_rails_94af2b1244");
            });

            modelBuilder.Entity<VesselCombinationMatrix>(entity =>
            {
                entity.Property(e => e.IsAppraisalBased).HasDefaultValueSql("false");

                entity.Property(e => e.IsSalaryBased).HasDefaultValueSql("false");

                entity.HasOne(d => d.RankCombination)
                    .WithMany(p => p.VesselCombinationMatrices)
                    .HasForeignKey(d => d.RankCombinationId)
                    .HasConstraintName("fk_rails_9160600425");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
