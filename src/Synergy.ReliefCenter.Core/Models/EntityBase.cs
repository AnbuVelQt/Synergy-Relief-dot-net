using System;

namespace Synergy.ReliefCenter.Core.Models
{
    public class EntityBase : IEntity
    {
        public long Id { get; set; }

        public DateTime CreatedOn { get; set; }

        public DateTime UpdatedOn { get; set; }

        public int? CreatedBy { get; set; }

        public int? UpdatedBy { get; set; }
    }
}
