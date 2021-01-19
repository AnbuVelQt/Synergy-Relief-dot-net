using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace Synergy.ReliefCenter.Data.Models
{
    [Table("audits")]
    [Index(nameof(AuditableType), nameof(AuditableId), Name = "index_audits_on_auditable_type_and_auditable_id")]
    public partial class Audit
    {
        [Key]
        [Column("id")]
        public long Id { get; set; }
        [Column("created_by_id", TypeName = "character varying")]
        public string CreatedById { get; set; }
        [Column("created_by_name", TypeName = "character varying")]
        public string CreatedByName { get; set; }
        [Column("created_by_role", TypeName = "character varying")]
        public string CreatedByRole { get; set; }
        [Column("action", TypeName = "character varying")]
        public string Action { get; set; }
        [Column("audited_changes", TypeName = "jsonb")]
        public string AuditedChanges { get; set; }
        [Column("auditable_type", TypeName = "character varying")]
        public string AuditableType { get; set; }
        [Column("auditable_id")]
        public long? AuditableId { get; set; }
        [Column("created_at", TypeName = "timestamp(6) without time zone")]
        public DateTime CreatedAt { get; set; }
        [Column("updated_at", TypeName = "timestamp(6) without time zone")]
        public DateTime UpdatedAt { get; set; }
        [Column("app_name", TypeName = "character varying")]
        public string AppName { get; set; }
    }
}
