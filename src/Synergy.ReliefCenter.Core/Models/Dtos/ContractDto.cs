using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Synergy.ReliefCenter.Core.Models.Dtos
{
    public class ContractDto
    {
        public long Id { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public long VesselId { get; set; }
        public long SeafarerId { get; set; }
        public ContractStatus Status { get; set; }

        public decimal? Salary { get; set; }
        public ContractFormDto ContractForm { get; set; }
    }
}
