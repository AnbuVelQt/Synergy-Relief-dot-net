using System;

namespace Synergy.ReliefCenter.Core.Models
{
    public interface IEntity
    {
        public long Id { get; }

        public DateTime CreatedOn { get; }

        public DateTime UpdatedOn { get; }

        public int CreatedBy { get; }

        public int UpdatedBy { get; }
    }
}
