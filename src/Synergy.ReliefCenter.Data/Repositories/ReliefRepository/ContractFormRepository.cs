using Synergy.ReliefCenter.Data.Contexts;
using Synergy.ReliefCenter.Data.Entities;
using Synergy.ReliefCenter.Data.Repositories.Abstraction.ReliefRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Synergy.ReliefCenter.Data.Repositories.ReliefRepository
{
    public class ContractFormRepository : BaseReliefRepository<ContractForm>, IContractFormRepository
    {
        protected ReliefContext reliefContext;

        public ContractFormRepository(ReliefContext ReliefContext) : base(ReliefContext)
        {
            reliefContext = ReliefContext;
        }
    }
}
