using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Synergy.ReliefCenter.Data.Entities.SalaryMatrix
{
    public class WageComponent
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string AccountCode { get; set; }

        public string Frequency { get; set; }

        public string Type { get; set; }

        public decimal Amount { get; set; }
    }
}
