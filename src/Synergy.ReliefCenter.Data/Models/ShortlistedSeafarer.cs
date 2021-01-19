using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace Synergy.ReliefCenter.Data.Models
{
    [Table("shortlisted_seafarers")]
    [Index(nameof(ReliefId), Name = "index_shortlisted_seafarers_on_relief_id")]
    [Index(nameof(SeafarerId), Name = "index_shortlisted_seafarers_on_seafarer_id")]
    [Index(nameof(StatusCode), Name = "index_shortlisted_seafarers_on_status_code")]
    public partial class ShortlistedSeafarer
    {
        [Key]
        [Column("id")]
        public long Id { get; set; }
        [Column("relief_id")]
        public long? ReliefId { get; set; }
        [Column("seafarer_id")]
        public long? SeafarerId { get; set; }
        [Column("status_code")]
        public long? StatusCode { get; set; }
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
        [Column("state", TypeName = "character varying")]
        public string State { get; set; }

        [ForeignKey(nameof(ReliefId))]
        [InverseProperty("ShortlistedSeafarers")]
        public virtual Relief Relief { get; set; }
    }
}
