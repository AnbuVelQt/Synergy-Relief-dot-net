using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Synergy.ReliefCenter.Core.Models
{
    public class AccessPolicyModel
    {

        public string Identifier { get; set; }
        public string Name { get; set; }
        public string[] AllowedRoles { get; set; }
        public string[] AllowedUsers { get; set; }
    }
}
