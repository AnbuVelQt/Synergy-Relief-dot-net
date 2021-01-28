using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Synergy.ReliefCenter.Core.Models.Dtos
{
    public class ContractDTO
    {
        public long Id { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public long VesselId { get; set; }
        public long SeafarerId { get; set; }
        public ContractStatus Status { get; set; }

        public decimal? Salary { get; set; }

        public string ImoNumber { get; set; }
        public string CdcNumber { get; set; }

        public DateTime? VerifyDate { get; set; }

        public string? VerifierName { get; set; }

        public string? VerifierEmail { get; set; }

        public ContractFormDTO ContractForm { get; set; }
    }
}
