using Synergy.ReliefCenter.Core.Models;

namespace Synergy.ReliefCenter.Api.Models
{
    public class Contract
    {
        public long Id { get; set; }

        public decimal? Salary { get; set; }

        public ContractStatus Status { get; set; }

        public TravelDetail TravelInfo { get; set; }

        public VesselDetail  VesselInfo { get; set; }

        public SeafarerDetail SeafarerDetail { get; set; }

        public ContractAttachmentDetail AttachmentDetail { get; set; }

        public ContractWages Wages { get; set; }
    }
}
