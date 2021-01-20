using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Synergy.AdobeSign.Configurations;

namespace Synergy.AdobeSign
{
    public static class ServiceCollectionExtensions
    {
        private const string DEFAULT_CONFIG_SECTION = "AbodeSign";

        public static void AddAdobeSign(this IServiceCollection services, IConfiguration configuration)
        {
            var adobeConfiguration = configuration.GetSection(DEFAULT_CONFIG_SECTION).Get<AdobeSignConfiguration>();

            services.AddSingleton(adobeConfiguration);

            services.AddScoped<IAdobeSignRestClient, AdobeSignRestClient>();
        }
    }
}
