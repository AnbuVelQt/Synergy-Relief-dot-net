using System.Collections.Generic;

namespace Synergy.ReliefCenter.Api.Models.Contracts
{
    public class ContractReviewerSet
    {
        public IList<ContractReviewer> Reviewers { get; set; }

        public bool SentForVerification { get; set; }
    }
}
