using FluentValidation;
using Synergy.ReliefCenter.Api.Models;
using System;
using System.ComponentModel.DataAnnotations;

namespace Synergy.ReliefCenter.Api.Validations
{
    public class CreateContractRequestValidator : AbstractValidator<CreateContractRequest>
    {
        public CreateContractRequestValidator()
        {
            RuleFor(x => x.VesselImoNumber).NotEmpty();
            RuleFor(x => x.SeafarerCdcNumber).NotEmpty();
        }
    
    }
}
