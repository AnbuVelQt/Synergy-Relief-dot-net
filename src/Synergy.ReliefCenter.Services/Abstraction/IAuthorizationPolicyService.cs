using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Synergy.ReliefCenter.Services.Abstraction
{
    public interface IAuthorizationPolicyService
    {
        bool ValidateRole(string token, string policy);
    }
}
