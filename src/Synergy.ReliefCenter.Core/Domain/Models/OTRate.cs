using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Synergy.ReliefCenter.Core.Domain.Models
{
    public class OTRate
    {
        public int MinHours { get; set; }

        public int MaxHours { get; set; }

        public decimal PerHourRate { get; set; }

        public decimal TotalAmount { get; set; }
    }
}
