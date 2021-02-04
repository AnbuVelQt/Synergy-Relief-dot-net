using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Synergy.ReliefCenter.Core.Models
{
    public class IdentityServerConfiguration
    {
        public string ShoreAuthorityUrl { get; set; }
        public string ShoreApiKey { get; set; }
        public string SeafarerAuthorityUrl { get; set; }
    }
}
