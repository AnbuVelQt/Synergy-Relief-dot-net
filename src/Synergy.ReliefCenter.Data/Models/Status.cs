using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace Synergy.ReliefCenter.Data.Models
{
    [Table("statuses")]
    public partial class Status
    {
        [Key]
        [Column("id")]
        public long Id { get; set; }
        [Column("code")]
        public long? Code { get; set; }
        [Column("status", TypeName = "character varying")]
        public string Status1 { get; set; }
        [Column("description", TypeName = "character varying")]
        public string Description { get; set; }
        [Column("created_at", TypeName = "timestamp(6) without time zone")]
        public DateTime CreatedAt { get; set; }
        [Column("updated_at", TypeName = "timestamp(6) without time zone")]
        public DateTime UpdatedAt { get; set; }
    }
}
