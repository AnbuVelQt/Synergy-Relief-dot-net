using Synergy.ReliefCenter.Core.Models;

namespace Synergy.ReliefCenter.Core.Models.Dtos
{
    public class ContractReviewerDto
    {
        public long Id { get; set; }

        public ReviewerRole Role { get; set; }

        //public int ReviewingOrder { get; set; }
    }
}
