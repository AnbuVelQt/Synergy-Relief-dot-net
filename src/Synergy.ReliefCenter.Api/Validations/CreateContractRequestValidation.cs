using FluentValidation;
using Synergy.ReliefCenter.Api.Models;
using System;
using System.ComponentModel.DataAnnotations;

namespace Synergy.ReliefCenter.Api.Validations
{
    public class CreateContractRequestValidation : AbstractValidator<CreateContractRequest>
    {
        public CreateContractRequestValidation()
        {
            RuleFor(x => x.VesselImoNumber).NotNull().NotEmpty();
            RuleFor(x => x.SeafarerCdcNumber).NotNull().NotEmpty();
        }
    
    }
}
