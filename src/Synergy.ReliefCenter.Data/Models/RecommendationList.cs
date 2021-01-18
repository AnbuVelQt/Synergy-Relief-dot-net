using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace Synergy.ReliefCenter.Data.Models
{
    [Table("recommendation_lists")]
    [Index(nameof(RecommendedSeafarerId), Name = "index_recommendation_lists_on_recommended_seafarer_id")]
    [Index(nameof(ReliefId), Name = "index_recommendation_lists_on_relief_id")]
    public partial class RecommendationList
    {
        [Key]
        [Column("id")]
        public long Id { get; set; }
        [Column("relief_id")]
        public long? ReliefId { get; set; }
        [Column("recommended_seafarer_id")]
        public long? RecommendedSeafarerId { get; set; }
        [Column("is_system_generated")]
        public bool? IsSystemGenerated { get; set; }
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
        [Column("deleted_at")]
        public DateTime? DeletedAt { get; set; }

        [ForeignKey(nameof(ReliefId))]
        [InverseProperty("RecommendationLists")]
        public virtual Relief Relief { get; set; }
    }
}
