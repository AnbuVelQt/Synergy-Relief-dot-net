using FluentValidation;
using Synergy.ReliefCenter.Api.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Synergy.ReliefCenter.Api.Validations
{
    public class RevisedSalaryValidator : AbstractValidator<RevisedSalary>
    {
        public RevisedSalaryValidator()
        {
            RuleFor(x => x.EffectiveFromDate).GreaterThanOrEqualTo(DateTime.UtcNow);
            RuleFor(x => x.ReasonForRevision).MaximumLength(100);
            RuleFor(x => x.TotalMonthlyWage).GreaterThanOrEqualTo(0);
        }

    }
}
