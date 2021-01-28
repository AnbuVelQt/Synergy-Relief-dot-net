using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Synergy.ReliefCenter.Core.Models.Dtos
{
    public class ContractFormDataDTO
    {
        public TravelDetailDTO TravelInfo { get; set; }

        public VesselDetailDTO VesselInfo { get; set; }

        public SeafarerDetailDTO SeafarerDetail { get; set; }

        public ContractAttachmentDetailDTO AttachmentDetail { get; set; }

        public ContractWagesDTO Wages { get; set; }

        public IList<ReviewersDTO> ContractReviewers { get; set; }

        public ReviewersDTO NextReviewer { get; set; }

        public IList<RevisedSalaryDTO> RevisedSalaries { get; set; }
    }
}
