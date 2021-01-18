using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace Synergy.ReliefCenter.Data.Models
{
    [Table("shore_employee_notification_logs")]
    [Index(nameof(NotifiableType), nameof(NotifiableId), Name = "index_shore_employee_notification_logs_on_notifiable")]
    [Index(nameof(SeafarerId), Name = "index_shore_employee_notification_logs_on_seafarer_id")]
    public partial class ShoreEmployeeNotificationLog
    {
        [Key]
        [Column("id")]
        public long Id { get; set; }
        [Column("notifiable_type", TypeName = "character varying")]
        public string NotifiableType { get; set; }
        [Column("notifiable_id")]
        public long? NotifiableId { get; set; }
        [Column("seafarer_id")]
        public long SeafarerId { get; set; }
        [Column("shore_employee_id", TypeName = "character varying")]
        public string ShoreEmployeeId { get; set; }
        [Column("notification_type", TypeName = "character varying")]
        public string NotificationType { get; set; }
        [Column("email_sent_at")]
        public DateTime? EmailSentAt { get; set; }
        [Column("status")]
        public int? Status { get; set; }
        [Column("created_at", TypeName = "timestamp(6) without time zone")]
        public DateTime CreatedAt { get; set; }
        [Column("updated_at", TypeName = "timestamp(6) without time zone")]
        public DateTime UpdatedAt { get; set; }
        [Column("push_sent_at")]
        public DateTime? PushSentAt { get; set; }
        [Column("push_failed_at")]
        public DateTime? PushFailedAt { get; set; }
        [Column("email_failed_at")]
        public DateTime? EmailFailedAt { get; set; }
        [Column("title", TypeName = "character varying")]
        public string Title { get; set; }
        [Column("body", TypeName = "character varying")]
        public string Body { get; set; }
        [Column("read")]
        public bool? Read { get; set; }
        [Column("deleted_at")]
        public DateTime? DeletedAt { get; set; }
    }
}
