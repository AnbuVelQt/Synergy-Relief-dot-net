using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using Synergy.ReliefCenter.Services;
using System;
using System.IO;
using System.Reflection;

namespace Synergy.ReliefCenter.Api
{
    public static class ServiceCollectionExtensions
    {
        public static void AddAllServices(this IServiceCollection services)
        {
            services.AddCustomSwagger();

            services.AddReliefServices();
        }

        private static void AddCustomSwagger(this IServiceCollection services)
        {
            services.AddSwaggerGen(options =>
            {
                options.SwaggerDoc("v1", new OpenApiInfo { Title = "Relief Center Api", Version = "v1" });

                // options.ExampleFilters();
                options.DescribeAllParametersInCamelCase();

                // integrate xml comments
                options.IncludeXmlComments(XmlCommentsFilePath);
                options.CustomSchemaIds(x => x.FullName);

                //options.AddSecurityDefinition("oauth2", new OpenApiSecurityScheme()
                //{
                //    Type = SecuritySchemeType.OAuth2,
                //    Flows = new OpenApiOAuthFlows()
                //    {
                //        AuthorizationCode = new OpenApiOAuthFlow()
                //        {
                //            AuthorizationUrl = new Uri($"{configuration.IdentityServerBaseUrl.TrimEnd('/')}/{configuration.AuthorizationUrl.Trim('/')}"),
                //            TokenUrl = new Uri($"{configuration.IdentityServerBaseUrl.TrimEnd('/')}/{configuration.TokenUrl.Trim('/')}"),
                //            Scopes = AuthorizationConstants.Scopes.MapScopeToDescription,
                //        }
                //    }
                //});
            });

            services.AddSwaggerGenNewtonsoftSupport();
            // services.AddSwaggerExamplesFromAssemblyOf<>();
        }

        private static string XmlCommentsFilePath
        {
            get
            {
                var fileName = typeof(Startup).GetTypeInfo().Assembly.GetName().Name + ".xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, fileName);

                return xmlPath;
            }
        }
    }
}
