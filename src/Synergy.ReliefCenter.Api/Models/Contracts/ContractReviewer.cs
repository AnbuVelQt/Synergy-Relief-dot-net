﻿using Synergy.ReliefCenter.Core.Models;

namespace Synergy.ReliefCenter.Api.Models
{
    public class ContractReviewer
    {
        public long Id { get; set; }

        public ReviewerRole Role { get; set; }

        //public int ReviewingOrder { get; set; }
    }
}
