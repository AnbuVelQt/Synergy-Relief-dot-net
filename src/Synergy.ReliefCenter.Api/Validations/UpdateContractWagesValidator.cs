using FluentValidation;
using Synergy.ReliefCenter.Api.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Synergy.ReliefCenter.Api.Validations
{
    public class UpdateContractWagesValidator : AbstractValidator<UpdateContractWages>
    {
        public UpdateContractWagesValidator()
        {
            RuleForEach(x => x.DeductionComponents).SetValidator(new WageComponentValidator());
            RuleForEach(x => x.OtherEarningComponents).SetValidator(new WageComponentValidator());
            RuleFor(x => x.SpecialAllowance).GreaterThanOrEqualTo(0);
        }

    }
}
