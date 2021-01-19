using Synergy.ReliefCenter.Core.Models.Dtos;
using System.Collections.Generic;

namespace Synergy.ReliefCenter.Core.Models.Dtos
{
    public class UpdateContractWagesDto
    {
        public decimal SpecialAllowance { get; set; }

        public IList<WageComponentDto> OtherEarningComponents { get; set; }
    }
}
