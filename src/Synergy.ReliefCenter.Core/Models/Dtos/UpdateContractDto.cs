using System.Collections.Generic;

namespace Synergy.ReliefCenter.Core.Models.Dtos
{
    public class UpdateContractDTO
    {
        public TravelDetailDTO TravelInfo { get; set; }

        public UpdateContractWagesDTO Wages { get; set; }

        public ContractAttachmentDetailDTO AttachmentDetail { get; set; }

        public IList<RevisedSalaryDTO> RevisedSalaries { get; set; }
    }
}
