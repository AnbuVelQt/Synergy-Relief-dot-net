﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Synergy.ReliefCenter.Data.Entities.Master
{
    public class PolicyRole
    {
        [Key]
        public int Id { get; set; }
        [ForeignKey("Access_Policy")]
        public int AccessPolicyId { get; set; }
        public AccessPolicy AccessPolicy { get; set; }
        public string Role { get; set; } 
        public string[] Actions { get; set; }
        public string[] AllowedRanks { get; set; }
    }
}
