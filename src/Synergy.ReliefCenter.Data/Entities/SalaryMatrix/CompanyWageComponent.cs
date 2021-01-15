using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Synergy.ReliefCenter.Data.Entities.SalaryMatrix
{
    public class CompanyWageComponent : WageComponent
    {
        public int MinExperience { get; set; }

        public int MaxExperience { get; set; }
    }   
}
