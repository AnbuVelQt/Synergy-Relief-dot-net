using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Synergy.ReliefCenter.Services.Abstraction
{
    public interface IApiRequestContext
    {
       public string UserId { get; }
       public List<string> AllowedRoles { get; }
    }
}
