using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace Synergy.ReliefCenter.Data.Models
{
    [Table("agent_notification_logs")]
    [Index(nameof(AgentId), Name = "index_agent_notification_logs_on_agent_id")]
    [Index(nameof(NotifiableType), nameof(NotifiableId), Name = "index_notification_logs_on_notifiable_type_and_notifiable_id")]
    public partial class AgentNotificationLog
    {
        [Key]
        [Column("id")]
        public long Id { get; set; }
        [Column("notifiable_type", TypeName = "character varying")]
        public string NotifiableType { get; set; }
        [Column("notifiable_id")]
        public long? NotifiableId { get; set; }
        [Column("status")]
        public int? Status { get; set; }
        [Column("email_sent_at")]
        public DateTime? EmailSentAt { get; set; }
        [Column("email_failed_at")]
        public DateTime? EmailFailedAt { get; set; }
        [Column("created_by_id", TypeName = "character varying")]
        public string CreatedById { get; set; }
        [Column("created_by_name", TypeName = "character varying")]
        public string CreatedByName { get; set; }
        [Column("updated_by_id", TypeName = "character varying")]
        public string UpdatedById { get; set; }
        [Column("updated_by_name", TypeName = "character varying")]
        public string UpdatedByName { get; set; }
        [Column("agent_id")]
        public long? AgentId { get; set; }
        [Column("email_failed_reason", TypeName = "character varying")]
        public string EmailFailedReason { get; set; }
        [Column("created_at", TypeName = "timestamp(6) without time zone")]
        public DateTime CreatedAt { get; set; }
        [Column("updated_at", TypeName = "timestamp(6) without time zone")]
        public DateTime UpdatedAt { get; set; }
    }
}
