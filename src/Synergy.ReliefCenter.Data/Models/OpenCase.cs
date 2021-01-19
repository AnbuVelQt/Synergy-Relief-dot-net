using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace Synergy.ReliefCenter.Data.Models
{
    [Table("open_cases")]
    [Index(nameof(CaseType), nameof(CaseId), Name = "index_open_cases_on_case_type_and_case_id")]
    public partial class OpenCase
    {
        [Key]
        [Column("id")]
        public long Id { get; set; }
        [Column("case_type", TypeName = "character varying")]
        public string CaseType { get; set; }
        [Column("case_id")]
        public long? CaseId { get; set; }
        [Column("shore_employee_id", TypeName = "character varying")]
        public string ShoreEmployeeId { get; set; }
        [Column("case_for", TypeName = "character varying")]
        public string CaseFor { get; set; }
        [Column("created_at", TypeName = "timestamp(6) without time zone")]
        public DateTime CreatedAt { get; set; }
        [Column("updated_at", TypeName = "timestamp(6) without time zone")]
        public DateTime UpdatedAt { get; set; }
        [Column("created_by_id", TypeName = "character varying")]
        public string CreatedById { get; set; }
        [Column("created_by_name", TypeName = "character varying")]
        public string CreatedByName { get; set; }
        [Column("shore_employee_name", TypeName = "character varying")]
        public string ShoreEmployeeName { get; set; }
        [Column("updated_by_id", TypeName = "character varying")]
        public string UpdatedById { get; set; }
        [Column("updated_by_name", TypeName = "character varying")]
        public string UpdatedByName { get; set; }
        [Column("seafarer_id")]
        public long? SeafarerId { get; set; }
        [Column("comment")]
        public string Comment { get; set; }
        [Column("state", TypeName = "character varying")]
        public string State { get; set; }
    }
}
