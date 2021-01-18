using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace Synergy.ReliefCenter.Data.Models
{
    [Table("unmatched_seafarers")]
    public partial class UnmatchedSeafarer
    {
        [Key]
        [Column("id")]
        public long Id { get; set; }
        [Column("seafarer_id")]
        public long? SeafarerId { get; set; }
        [Column("vessel_id")]
        public long? VesselId { get; set; }
        [Column("next_availability_date")]
        public DateTime? NextAvailabilityDate { get; set; }
        [Column("created_at", TypeName = "timestamp(6) without time zone")]
        public DateTime CreatedAt { get; set; }
        [Column("updated_at", TypeName = "timestamp(6) without time zone")]
        public DateTime UpdatedAt { get; set; }
    }
}
