using System.Collections.Generic;

namespace Synergy.ReliefCenter.Core.Models
{
    public class AccessPolicyModel
    {

        public string Identifier { get; set; }
        public string Name { get; set; }
        public List<string> AllowedRoles { get; set; }
        public List<string> AllowedUsers { get; set; }
    }
}
