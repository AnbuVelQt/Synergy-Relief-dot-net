using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace Synergy.ReliefCenter.Data.Models
{
    [Table("seafarer_relief_requests")]
    [Index(nameof(SeafarerId), Name = "index_seafarer_relief_requests_on_seafarer_id")]
    [Index(nameof(SignOffReasonId), Name = "index_seafarer_relief_requests_on_sign_off_reason_id")]
    public partial class SeafarerReliefRequest
    {
        [Key]
        [Column("id")]
        public long Id { get; set; }
        [Column("seafarer_id")]
        public long? SeafarerId { get; set; }
        [Column("other_reason", TypeName = "character varying")]
        public string OtherReason { get; set; }
        [Column("reject_reason", TypeName = "character varying")]
        public string RejectReason { get; set; }
        [Column("approval_reason", TypeName = "character varying")]
        public string ApprovalReason { get; set; }
        [Column("status")]
        public int? Status { get; set; }
        [Column("sign_off_reason_id")]
        public long? SignOffReasonId { get; set; }
        [Column("requested_on", TypeName = "date")]
        public DateTime? RequestedOn { get; set; }
        [Column("created_at", TypeName = "timestamp(6) without time zone")]
        public DateTime CreatedAt { get; set; }
        [Column("updated_at", TypeName = "timestamp(6) without time zone")]
        public DateTime UpdatedAt { get; set; }
    }
}
