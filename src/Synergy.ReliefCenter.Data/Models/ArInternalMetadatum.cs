using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace Synergy.ReliefCenter.Data.Models
{
    [Table("ar_internal_metadata")]
    public partial class ArInternalMetadatum
    {
        [Key]
        [Column("key", TypeName = "character varying")]
        public string Key { get; set; }
        [Column("value", TypeName = "character varying")]
        public string Value { get; set; }
        [Column("created_at", TypeName = "timestamp(6) without time zone")]
        public DateTime CreatedAt { get; set; }
        [Column("updated_at", TypeName = "timestamp(6) without time zone")]
        public DateTime UpdatedAt { get; set; }
    }
}
