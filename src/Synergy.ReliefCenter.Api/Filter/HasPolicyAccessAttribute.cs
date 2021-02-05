using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Synergy.ReliefCenter.Api.Filter
{
    internal class HasPolicyAccessAttribute : TypeFilterAttribute
    {
        public HasPolicyAccessAttribute() : base(typeof(AuthorizationPolicyFilter))
        {

        }

        public HasPolicyAccessAttribute(string policy) : base(typeof(AuthorizationPolicyFilter))
        {
            Arguments = new object[] { policy };
        }
    }
}
