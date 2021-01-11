using Synergy.ReliefCenter.Data.Contexts;
using Synergy.ReliefCenter.Data.Entities.Seafarer;
using Synergy.ReliefCenter.Data.Interfaces.SeafarerRepository;

namespace Synergy.CrewWage.Data.Repositories.SeafarerRepository
{
    public class SeaExperienceRepository: BaseSeafarerRepository<SeaExperience>, ISeaExperienceRepository
    {
        protected SeafarerContext seafarerContext;

        public SeaExperienceRepository(SeafarerContext SeafarerContext) : base(SeafarerContext)
        {
            seafarerContext = SeafarerContext;
        }
    }
}
