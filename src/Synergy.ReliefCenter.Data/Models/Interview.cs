using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace Synergy.ReliefCenter.Data.Models
{
    [Table("interviews")]
    [Index(nameof(AssignedTo), Name = "index_interviews_on_assigned_to")]
    [Index(nameof(SfId), Name = "index_interviews_on_sf_id")]
    public partial class Interview
    {
        [Key]
        [Column("id")]
        public long Id { get; set; }
        [Column("sf_id", TypeName = "character varying")]
        public string SfId { get; set; }
        [Column("assigned_to", TypeName = "character varying")]
        public string AssignedTo { get; set; }
        [Column("approved_by", TypeName = "character varying")]
        public string ApprovedBy { get; set; }
        [Column("approved_date", TypeName = "date")]
        public DateTime? ApprovedDate { get; set; }
        [Column("date", TypeName = "date")]
        public DateTime? Date { get; set; }
        [Column("feedback")]
        public string Feedback { get; set; }
        [Column("created_at")]
        public DateTime CreatedAt { get; set; }
        [Column("updated_at")]
        public DateTime UpdatedAt { get; set; }
    }
}
