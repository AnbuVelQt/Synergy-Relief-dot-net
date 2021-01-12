using Synergy.ReliefCenter.Data.Entities.Seafarer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Synergy.ReliefCenter.Data.Repositories.Abstraction
{
    public interface ISeafarerDataRepository
    {
        ValueTask<Seafarer> GetSeafarerByIdAsync(long id);
        ValueTask<SeafarerContactDetails> GetSeafarerContactDetailsByIdAsync(long id);
    }
}
