using Synergy.ReliefCenter.Core.Models;
using System;

namespace Synergy.ReliefCenter.Data.Entities
{
    public class Contract : EntityBase
    {
        public DateTime? StartDate { get; set; }

        public DateTime? EndDate { get; set; }

        public long VesselId { get; set; }

        public long SeafarerId { get; set; }

        public ContractStatus Status { get; set; }
    }
}
