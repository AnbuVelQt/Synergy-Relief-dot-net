using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Synergy.ReliefCenter.Core.Models.Dtos
{
    public class ContractFormDataDto
    {
        public TravelDetailDto TravelInfo { get; set; }

        public VesselDetailDto VesselInfo { get; set; }

        public SeafarerDetailDto SeafarerDetail { get; set; }

        public ContractAttachmentDetailDto AttachmentDetail { get; set; }

        public ContractWagesDto Wages { get; set; }
    }
}
