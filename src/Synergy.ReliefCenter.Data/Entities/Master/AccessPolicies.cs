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

        public string Description { get; set; }

        public string Identifier { get; set; }

        public DateTime CreatedAt { get; set; }

        public DateTime UpdatedAt { get; set; }

        public IList<PolicyRoles> PolicyRoles { get; set; }
        public IList<PolicyUsers> PolicyUsers { get; set; }
    }
}
