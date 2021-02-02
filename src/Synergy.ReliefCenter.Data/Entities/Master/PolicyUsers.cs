using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Synergy.ReliefCenter.Data.Entities.Master
{
    public class PolicyUsers
    {
        [Key]
        public int Id { get; set; }
        [ForeignKey("Access_Policy")]
        public int AccessPolicyId { get; set; }

        public AccessPolicies AccessPolicy { get; set; }

        public string UserId { get; set; }

        public string[] Actions { get; set; }

        public string[] AllowedRanks { get; set; }
    }
}
