﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Synergy.ReliefCenter.Api.Models
{
    public class Reviewers
    {
        public long Id { get; set; }
        
        public long ContractId { get; set; }

        public long? ReviewerId { get; set; }

        public string Role { get; set; }

        public bool Approved { get; set; }
    }
}