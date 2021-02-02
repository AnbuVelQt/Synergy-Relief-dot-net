using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Synergy.ReliefCenter.Data.Entities.Master
{
    public class AccessPolicies
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public string Identifier { get; set; }
        public PolicyRoles PolicyRoles { get; set; }
        public PolicyUsers PolicyUsers { get; set; }
    }
}
