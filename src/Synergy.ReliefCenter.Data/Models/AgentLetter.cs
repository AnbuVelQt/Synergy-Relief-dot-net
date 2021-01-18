using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace Synergy.ReliefCenter.Data.Models
{
    [Table("agent_letters")]
    [Index(nameof(AgentId), Name = "index_agent_letters_on_agent_id")]
    [Index(nameof(ReliefId), Name = "index_agent_letters_on_relief_id")]
    [Index(nameof(SeafarerId), Name = "index_agent_letters_on_seafarer_id")]
    public partial class AgentLetter
    {
        [Key]
        [Column("id")]
        public long Id { get; set; }
        [Column("seafarer_id")]
        public long SeafarerId { get; set; }
        [Column("relief_id")]
        public long ReliefId { get; set; }
        [Column("agent_id")]
        public long AgentId { get; set; }
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

        [ForeignKey(nameof(ReliefId))]
        [InverseProperty("AgentLetters")]
        public virtual Relief Relief { get; set; }
    }
}
