﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Synergy.ReliefCenter.Core.Models.Dtos
{
    public class ContractFormDTO
    {
        public ContractFormDataDTO Data { get; set; }
        public long Id { get; set; }

        public long ContractId { get; set; }
    }
}
