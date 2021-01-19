using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Synergy.ReliefCenter.Data.Contexts;
using Synergy.ReliefCenter.Data.Models;
using Synergy.ReliefCenter.Data.Repositories;
using Synergy.ReliefCenter.Data.Repositories.Abstraction;
using Synergy.ReliefCenter.Data.Repositories.Abstraction.ReliefRepository;
using Synergy.ReliefCenter.Data.Repositories.ReliefRepository;
using Synergy.ReliefCenter.Services.Abstraction;

namespace Synergy.ReliefCenter.Services
{
    public static class ServiceCollectionExtensions
    {
        private const string ReliefDbConnectionString = "ReliefDB";
        private const string ManningDbConnectionString = "ManningDB";
        private const string VesselDBConnectionString = "VesselDB";
        private const string SeafarerDBConnectionString = "SeafarerDB";
        private const string MasterDBConnectionString = "MasterDB";
        public static void AddReliefServices(this IServiceCollection services)
        {
            services.AddScoped<IContractService, ContractService>();
        }

        public static void AddReliefRepositories(this IServiceCollection services)
        {
            services.AddScoped<IVesselDataRepository, VesselDataRepository>();
            services.AddScoped<ISeafarerDataRepository, SeafarerDataRepository>();
            services.AddScoped<IContractRepository, ContractRepository>();
            services.AddScoped<IContractFormRepository, ContractFormRepository>();
            services.AddScoped<IExternalSalaryMatrixRepository, ExternalSalaryMatrixRepository>();
        }

        public static void AddEFContext(this IServiceCollection services, IConfiguration configuration)
        {
            // All DB Connection Strings
            var ReliefDbString = configuration.GetConnectionString(ReliefDbConnectionString);
            var ManningDbString = configuration.GetConnectionString(ManningDbConnectionString);
            var VesselString = configuration.GetConnectionString(VesselDBConnectionString);
            var SeafarerString = configuration.GetConnectionString(SeafarerDBConnectionString);
            var MasterString = configuration.GetConnectionString(MasterDBConnectionString);

            // Context Register
            services.AddDbContext<ReliefDbContext>(opt =>
                opt.UseNpgsql(ReliefDbString));

            services.AddDbContext<VesselDbContext>(opt =>
                opt.UseNpgsql(VesselString).UseSnakeCaseNamingConvention());

            services.AddDbContext<SeafarerDbContext>(opt =>
                opt.UseNpgsql(SeafarerString).UseSnakeCaseNamingConvention());

            services.AddDbContext<MasterDbContext>(opt =>
                opt.UseNpgsql(MasterString).UseSnakeCaseNamingConvention());

            services.AddDbContext<synergy_manningContext>(opt =>
                opt.UseNpgsql(ManningDbString).UseSnakeCaseNamingConvention());
        }
    }
}
