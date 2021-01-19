using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace Synergy.ReliefCenter.Data.Models
{
    [Table("vessel_combination_matrices")]
    [Index(nameof(RankCombinationId), Name = "index_vessel_combination_matrices_on_rank_combination_id")]
    [Index(nameof(VesselId), Name = "index_vessel_combination_matrices_on_vessel_id")]
    public partial class VesselCombinationMatrix
    {
        [Key]
        [Column("id")]
        public long Id { get; set; }
        [Column("vessel_id")]
        public long? VesselId { get; set; }
        [Column("rank_combination_id")]
        public long? RankCombinationId { get; set; }
        [Column("is_salary_based")]
        public bool? IsSalaryBased { get; set; }
        [Column("is_appraisal_based")]
        public bool? IsAppraisalBased { get; set; }
        [Column("experience_in_synergy")]
        public double? ExperienceInSynergy { get; set; }
        [Column("experience_in_rank")]
        public double? ExperienceInRank { get; set; }
        [Column("experience_in_vessel_type")]
        public double? ExperienceInVesselType { get; set; }
        [Column("salary")]
        public double? Salary { get; set; }
        [Column("created_at", TypeName = "timestamp(6) without time zone")]
        public DateTime CreatedAt { get; set; }
        [Column("updated_at", TypeName = "timestamp(6) without time zone")]
        public DateTime UpdatedAt { get; set; }
        [Column("created_by_id", TypeName = "character varying")]
        public string CreatedById { get; set; }
        [Column("created_by_name", TypeName = "character varying")]
        public string CreatedByName { get; set; }
        [Column("updated_by_id", TypeName = "character varying")]
        public string UpdatedById { get; set; }
        [Column("updated_by_name", TypeName = "character varying")]
        public string UpdatedByName { get; set; }

        [ForeignKey(nameof(RankCombinationId))]
        [InverseProperty("VesselCombinationMatrices")]
        public virtual RankCombination RankCombination { get; set; }
    }
}
