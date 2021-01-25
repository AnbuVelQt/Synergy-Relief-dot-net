using System.Collections.Generic;

namespace Synergy.ReliefCenter.Core.Models.Dtos
{
    public class ContractReviewerSetDto
    {
        //public IList<ContractReviewer> Reviewers { get; set; }
        public ContractReviewerDto[] Reviewers { get; set; }

        // public bool SentForVerification { get; set; }
    }
}
