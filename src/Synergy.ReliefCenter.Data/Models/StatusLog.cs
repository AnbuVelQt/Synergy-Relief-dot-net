using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace Synergy.ReliefCenter.Data.Models
{
    [Table("status_logs")]
    [Index(nameof(LoggableId), Name = "index_status_logs_on_loggable_id")]
    [Index(nameof(LoggableType), Name = "index_status_logs_on_loggable_type")]
    [Index(nameof(SeafarerId), Name = "index_status_logs_on_seafarer_id")]
    [Index(nameof(StatusCode), Name = "index_status_logs_on_status_code")]
    public partial class StatusLog
    {
        [Key]
        [Column("id")]
        public long Id { get; set; }
        [Column("loggable_id")]
        public long? LoggableId { get; set; }
        [Column("loggable_type", TypeName = "character varying")]
        public string LoggableType { get; set; }
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
    }
}
