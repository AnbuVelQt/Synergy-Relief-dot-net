using FluentValidation;
using Synergy.ReliefCenter.Api.Models;

namespace Synergy.ReliefCenter.Api.Validations
{
    public class UpdateContractRequestValidator : AbstractValidator<UpdateContractRequest>
    {
        public UpdateContractRequestValidator()
        {
            RuleFor(x => x.Wages).SetValidator(new UpdateContractWagesValidator());
            RuleFor(x => x.TravelInfo).SetValidator(new TravelInfoValidator());
            RuleForEach(x => x.RevisedSalaries).SetValidator(new RevisedSalaryValidator());
        }

    }
}
