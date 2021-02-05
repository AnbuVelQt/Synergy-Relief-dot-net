using FluentValidation;
using Synergy.ReliefCenter.Api.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Synergy.ReliefCenter.Api.Validations
{
    public class TravelInfoValidator : AbstractValidator<TravelDetail>
    {
        public TravelInfoValidator()
        {
            RuleFor(x => x.StartDate).GreaterThanOrEqualTo(DateTime.UtcNow).LessThan(x=>x.EndDate);
            RuleFor(x => x.EndDate).GreaterThanOrEqualTo(DateTime.UtcNow).GreaterThan(x=>x.StartDate);
            RuleFor(x => x.PlaceOfEnagement).MinimumLength(1);
            RuleFor(x => x.ContractTerms).MinimumLength(1);
        }

    }
}
