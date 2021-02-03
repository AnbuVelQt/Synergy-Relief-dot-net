using FluentValidation;
using Synergy.ReliefCenter.Api.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Synergy.ReliefCenter.Api.Validations
{
    public class ContractReviewerValidator : AbstractValidator<ContractReviewerSet>
    {
        public ContractReviewerValidator()
        {
            RuleFor(x => x.Reviewers).NotEmpty();
        }

    }
}
