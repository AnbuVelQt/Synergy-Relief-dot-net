﻿using System.Collections.Generic;

namespace Synergy.ReliefCenter.Api.Models
{
    public class ContractReviewerSet
    {
        //public IList<ContractReviewer> Reviewers { get; set; }
        public ContractReviewer[] Reviewers { get; set; }
        // public bool SentForVerification { get; set; }
    }
}
