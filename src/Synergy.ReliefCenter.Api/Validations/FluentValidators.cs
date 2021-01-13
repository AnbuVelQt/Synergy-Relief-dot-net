using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using Synergy.ReliefCenter.Api.Models;

namespace Synergy.ReliefCenter.Api.Validations
{
    public static class FluentValidators
    {
     
        public static void AddAllValidators(this IServiceCollection services)
        {
            services.AddTransient<IValidator<CreateContractRequest>, CreateContractRequestValidation>();
        }
    }
}
