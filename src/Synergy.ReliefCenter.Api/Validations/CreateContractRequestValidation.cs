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
            //RuleFor(x => x.VesselId).GreaterThan(1);
            //RuleFor(x => x.SeafarerId).GreaterThan(1);
        }
    
    }
}
