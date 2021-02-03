using FluentValidation;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Synergy.ReliefCenter.Api.Configuration;
using Synergy.ReliefCenter.Api.Models;
using Synergy.ReliefCenter.Api.Validations;
using Synergy.ReliefCenter.Services;
using System;
using System.IO;
using System.Reflection;
using AuthenticationScheme = Synergy.ReliefCenter.Api.Configuration.AuthenticationScheme;

namespace Synergy.ReliefCenter.Api
{
    public static class ServiceCollectionExtensions
    {
        public static void AddAllServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddCustomSwagger();

            services.AddEFContext(configuration);
            services.AddReliefServices();
            services.AddReliefRepositories();
            services.AddAuthentication(configuration);
            services.AddAllValidators();
        }
        private static void AddAuthentication(this IServiceCollection services, IConfiguration configuration)
        {

            // For JwtBearerConfiguration

            var authenticationScheme = configuration.GetSection(nameof(AuthenticationScheme)).Get<AuthenticationScheme>();

            services.AddAuthentication(AuthenticationSchemas.ShoreIdp)
                    .AddJwtBearer(AuthenticationSchemas.ShoreIdp, options =>
                    {
                        options.Authority = authenticationScheme.ShoreIdp.AuthorityUrl;
                        options.TokenValidationParameters = new TokenValidationParameters
                        {
                            ValidIssuer = authenticationScheme.ShoreIdp.AuthorityUrl,
                            ValidateIssuer = true,
                            ValidateLifetime = true,
                            ValidateIssuerSigningKey = true,
                            ValidateAudience = false
                        };
                    })
                    .AddJwtBearer(AuthenticationSchemas.SeafarerIdp, options =>
                    {
                        options.Authority = authenticationScheme.SeafarerIdp.AuthorityUrl;
                        options.TokenValidationParameters = new TokenValidationParameters
                        {
                            ValidIssuer = authenticationScheme.SeafarerIdp.AuthorityUrl,
                            ValidateIssuer = true,
                            ValidateLifetime = true,
                            ValidateIssuerSigningKey = true,
                            ValidateAudience = false
                        };
                    });

        }

        private static void AddCustomSwagger(this IServiceCollection services)
        {
            services.AddSwaggerGen(options =>
            {
                options.SwaggerDoc("v1", new OpenApiInfo { Title = "Relief Center Api", Version = "v1" });

                options.AddSecurityDefinition("api_key", new OpenApiSecurityScheme
                {
                    Name = "Authorization",
                    Type = SecuritySchemeType.ApiKey,
                    Scheme = "api_key",
                    BearerFormat = "JWT",
                    In = ParameterLocation.Header,
                    Description = "Relief Center API With Identity Authentication"
                });
                options.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = "api_key"
                            }
                        },
                        new string[] {}
                    }
                });

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

        public static void AddAllValidators(this IServiceCollection services)
        {
            services.AddTransient<IValidator<CreateContractRequest>, CreateContractRequestValidator>();
            services.AddTransient<IValidator<UpdateContractRequest>, UpdateContractRequestValidator>();
            services.AddTransient<IValidator<UpdateContractWages>, UpdateContractWagesValidator>();
            services.AddTransient<IValidator<RevisedSalary>, RevisedSalaryValidator>();
            services.AddTransient<IValidator<WageComponent>, WageComponentValidator>();
            services.AddTransient<IValidator<TravelDetail>, TravelInfoValidator>();
            services.AddTransient<IValidator<ContractReviewerSet>, ContractReviewerValidator>();
        }
    }
}
