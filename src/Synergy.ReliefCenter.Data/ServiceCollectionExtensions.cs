using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Synergy.ReliefCenter.Core.Models;
using Synergy.ReliefCenter.Data.Repositories;
using Synergy.ReliefCenter.Data.Repositories.Abstraction;

namespace Synergy.ReliefCenter.Data
{
    public static class ServiceCollectionExtensions
    {
        private const string DEFAULT_CONFIG_SECTION = "CrewWage";
        private const string DEFAULT_CONFIG_SECTION_USER = "IdentityServerConfiguration";

        public static void AddExternalApi(this IServiceCollection services, IConfiguration configuration)
        {
            var crewWageConfiguration = configuration.GetSection(DEFAULT_CONFIG_SECTION).Get<ExternalApiConfiguration.CrewWageApi>();

            var userInfoConfiguration = configuration.GetSection(DEFAULT_CONFIG_SECTION_USER).Get<IdentityServerConfiguration>();

            services.AddSingleton(crewWageConfiguration);

            services.AddSingleton(userInfoConfiguration);

            services.AddScoped<IExternalUserDetailsRepository, ExternalUserDetailsRepository>();
        }
    }
}
