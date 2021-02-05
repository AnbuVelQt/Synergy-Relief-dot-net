using FluentValidation;
using Synergy.ReliefCenter.Api.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Synergy.ReliefCenter.Api.Validations
{
    public class WageComponentValidator : AbstractValidator<WageComponent>
    {
        public WageComponentValidator()
        {
            RuleFor(x => x.Amount).GreaterThanOrEqualTo(0);
            RuleFor(x => x.EffectiveDate).GreaterThanOrEqualTo(DateTime.UtcNow);
            RuleFor(x => x.Frequency).IsInEnum();
        }

    }
}
