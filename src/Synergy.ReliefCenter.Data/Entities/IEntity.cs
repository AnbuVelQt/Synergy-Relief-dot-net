using System;

namespace Synergy.ReliefCenter.Data.Entities
{
    public interface IEntity
    {
        public long Id { get; }

        public DateTime CreatedOn { get; }

        public DateTime UpdatedOn { get; }

        public int? CreatedBy { get; }

        public int? UpdatedBy { get; }
    }
}
