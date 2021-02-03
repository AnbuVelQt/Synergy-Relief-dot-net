using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Synergy.ReliefCenter.Core.Domain.Models
{
    public class ContractReviewers
    {
        public long Id { get; set; }
        public long ContractId { get; set; }
        public string ReviewerId { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Role { get; set; }
        public bool Approved { get; set; }
        public DateTime ApprovedOn { get; set; }
    }
}
