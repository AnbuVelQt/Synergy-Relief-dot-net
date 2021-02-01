using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Synergy.ReliefCenter.Api.Filter
{
    internal class AuthorizationPolicyAttribute : TypeFilterAttribute
    {
        public AuthorizationPolicyAttribute() : base(typeof(AuthorizationPolicyFilter))
        {

        }
        public AuthorizationPolicyAttribute(string policy) : base(typeof(AuthorizationPolicyFilter))
        {
            Arguments = new object[] { policy };
        }
    }
}
