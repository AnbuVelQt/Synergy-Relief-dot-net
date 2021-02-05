using System.Collections.Generic;

namespace Synergy.ReliefCenter.Services.Abstraction
{
    public interface IApiRequestContext
    {
       public string UserId { get; }
       public List<string> AllowedRoles { get; }
    }
}
