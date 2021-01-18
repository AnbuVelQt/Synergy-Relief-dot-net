namespace Synergy.ReliefCenter.Api.Models
{
    public class UpdateContractRequest
    {   
        public TravelDetail TravelInfo { get; set; }
        public UpdateContractWages Wages { get; set; }
        public ContractAttachmentDetail AttachmentDetail { get; set; }
    }
}
