using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace Synergy.ReliefCenter.Data.Models
{
    [Table("rank_combinations")]
    public partial class RankCombination
    {
        public RankCombination()
        {
            FleetCombinationMatrices = new HashSet<FleetCombinationMatrix>();
            VesselCombinationMatrices = new HashSet<VesselCombinationMatrix>();
        }

        [Key]
        [Column("id")]
        public long Id { get; set; }
        [Column("name", TypeName = "character varying")]
        public string Name { get; set; }
        [Column("rank_ids")]
        public string[] RankIds { get; set; }
        [Column("created_at", TypeName = "timestamp(6) without time zone")]
        public DateTime CreatedAt { get; set; }
        [Column("updated_at", TypeName = "timestamp(6) without time zone")]
        public DateTime UpdatedAt { get; set; }

        [InverseProperty(nameof(FleetCombinationMatrix.RankCombination))]
        public virtual ICollection<FleetCombinationMatrix> FleetCombinationMatrices { get; set; }
        [InverseProperty(nameof(VesselCombinationMatrix.RankCombination))]
        public virtual ICollection<VesselCombinationMatrix> VesselCombinationMatrices { get; set; }
    }
}
