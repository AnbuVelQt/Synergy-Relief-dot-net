using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Synergy.ReliefCenter.Api.Configuration
{
    public class JwtBearerConfiguration
    {
        public IdentityProviderProperties ShoreIdp { get; set; }
        public IdentityProviderProperties SeafarerIdp { get; set; }
    }

    public class IdentityProviderProperties
    {
        public string AuthorityUrl { get; set; }

    }

    public static class AuthenticationSchemas
    {
        public const string ShoreIdp = "ShoreIdp";
        public const string SeafarerIdp = "SeafarerIdp";

    }
}
