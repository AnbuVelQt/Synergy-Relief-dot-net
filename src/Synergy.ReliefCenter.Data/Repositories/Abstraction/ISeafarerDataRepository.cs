﻿using Synergy.ReliefCenter.Data.Entities.SeafarerCenter;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Synergy.ReliefCenter.Data.Repositories.Abstraction
{
    public interface ISeafarerDataRepository
    {
        ValueTask<Seafarer> GetSeafarerByIdAsync(string id);
        ValueTask<SeafarerContactDetails> GetSeafarerContactDetailsByIdAsync(long id);

        ValueTask<Seafarer> GetSeafarerByIdentityAsync(string userId);

        ValueTask<SeafarerDocuments> GetSeafarerDocumentsByIdAsync(long seafarerId, long id);
    }
}
