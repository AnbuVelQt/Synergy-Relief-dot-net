using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Synergy.ReliefCenter.Data.Entities.Master
{
    public class PolicyUser
    {
        [Key]
        public int Id { get; set; }
        [ForeignKey("Access_Policy")]
        public int AccessPolicyId { get; set; }

        public AccessPolicy AccessPolicy { get; set; }

        public string UserId { get; set; }

        public string[] Actions { get; set; }

        public string[] AllowedRanks { get; set; }
    }
}
