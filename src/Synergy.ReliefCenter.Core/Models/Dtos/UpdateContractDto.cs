namespace Synergy.ReliefCenter.Core.Models.Dtos
{
    public class UpdateContractDto
    {
        public TravelDetailDto TravelInfo { get; set; }
        public UpdateContractWagesDto Wages { get; set; }
        public ContractAttachmentDetailDto AttachmentDetail { get; set; }
    }
}
