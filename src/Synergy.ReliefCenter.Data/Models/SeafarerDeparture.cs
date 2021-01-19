using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace Synergy.ReliefCenter.Data.Models
{
    [Table("seafarer_departures")]
    [Index(nameof(ReliefId), Name = "index_seafarer_departures_on_relief_id")]
    public partial class SeafarerDeparture
    {
        [Key]
        [Column("id")]
        public long Id { get; set; }
        [Column("relief_id")]
        public long? ReliefId { get; set; }
        [Column("shore_user_id", TypeName = "character varying")]
        public string ShoreUserId { get; set; }
        [Column("seafarer_id")]
        public long? SeafarerId { get; set; }
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
        [Column("seafarer_signed_at")]
        public DateTime? SeafarerSignedAt { get; set; }
        [Column("shore_user_signed_at")]
        public DateTime? ShoreUserSignedAt { get; set; }
        [Column("file_name", TypeName = "character varying")]
        public string FileName { get; set; }
        [Column("file_content_type", TypeName = "character varying")]
        public string FileContentType { get; set; }
        [Column("file_url", TypeName = "character varying")]
        public string FileUrl { get; set; }
        [Column("file_size")]
        public int? FileSize { get; set; }
        [Column("shore_employee_name", TypeName = "character varying")]
        public string ShoreEmployeeName { get; set; }
    }
}
