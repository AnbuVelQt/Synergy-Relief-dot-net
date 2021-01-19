using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace Synergy.ReliefCenter.Data.Models
{
    [Table("seafarer_checklists")]
    [Index(nameof(DepartureChecklistId), Name = "index_seafarer_checklists_on_departure_checklist_id")]
    [Index(nameof(SeafarerDepartureId), Name = "index_seafarer_checklists_on_seafarer_departure_id")]
    public partial class SeafarerChecklist
    {
        [Key]
        [Column("id")]
        public long Id { get; set; }
        [Column("departure_checklist_id")]
        public long? DepartureChecklistId { get; set; }
        [Column("seafarer_departure_id")]
        public long? SeafarerDepartureId { get; set; }
        [Column("is_completed")]
        public bool? IsCompleted { get; set; }
        [Column("created_by_id", TypeName = "character varying")]
        public string CreatedById { get; set; }
        [Column("created_by_name", TypeName = "character varying")]
        public string CreatedByName { get; set; }
        [Column("updated_by_id", TypeName = "character varying")]
        public string UpdatedById { get; set; }
        [Column("updated_by_name", TypeName = "character varying")]
        public string UpdatedByName { get; set; }
        [Column("created_at", TypeName = "timestamp(6) without time zone")]
        public DateTime CreatedAt { get; set; }
        [Column("updated_at", TypeName = "timestamp(6) without time zone")]
        public DateTime UpdatedAt { get; set; }
        [Column("deleted_at")]
        public DateTime? DeletedAt { get; set; }
    }
}
