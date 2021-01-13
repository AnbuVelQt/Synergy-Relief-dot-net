using Synergy.ReliefCenter.Data.Entities.VesselCenter;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Synergy.ReliefCenter.Data.Repositories.Abstraction
{
    public interface IVesselDataRepository
    {
        ValueTask<Vessel> GetVesselByIdAsync(long id);
    }
}
