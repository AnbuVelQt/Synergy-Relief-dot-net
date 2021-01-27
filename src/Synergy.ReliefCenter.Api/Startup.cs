using AutoMapper;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Synergy.ReliefCenter.Api.Helpers;
using Synergy.ReliefCenter.Api.Mappers;
using Synergy.ReliefCenter.Api.Validations;
using Synergy.ReliefCenter.Data.Models;
using Synergy.ReliefCenter.Services.Mappers;
//using Synergy.Core.EmailService;
using Microsoft.Extensions.Logging;
using System;
using Synergy.ReliefCenter.Api.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace Synergy.ReliefCenter.Api
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllersWithViews()
               .AddNewtonsoftJson(options =>
               options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore
            );

            services.AddCors(o => o.AddPolicy("MyPolicy", builder =>
            {
                builder.AllowAnyOrigin()
                    .AllowAnyMethod()
                    .AllowAnyHeader();
            }));
            using var loggerFactory = LoggerFactory.Create(b => Console.WriteLine());
            services.AddAllServices(Configuration);
            //services.AddSingleton<ILogger<EmailSender>>(loggerFactory.CreateLogger<EmailSender>());
            //var emailLogger = loggerFactory.CreateLogger<IEmailService>();

            //services.AddSingleton<ILogger<IEmailService>>(emailLogger);
            //services.AddEmailService(Configuration);
            var mapperConfig = new MapperConfiguration(mc =>
            {
                mc.AddProfile(new EntityMappingProfile());
                mc.AddProfile(new MappingProfile());
            });

            IMapper mapper = mapperConfig.CreateMapper();
            services.AddSingleton(mapper);
            services.AddAllValidators();

            // For JwtBearerConfiguration

            var jwtBearerConfiguration = Configuration.GetSection(nameof(JwtBearerConfiguration)).Get<JwtBearerConfiguration>();

            services.AddAuthentication(AuthenticationSchemas.ShoreIdp)
                .AddJwtBearer(AuthenticationSchemas.ShoreIdp, options =>
                {
                    options.Authority = jwtBearerConfiguration.ShoreIdp.AuthorityUrl;
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidIssuer = jwtBearerConfiguration.ShoreIdp.ValidIssuer,
                        ValidateIssuer = true,
                        ValidateLifetime = true,
                        ValidateAudience = false,
                        ValidateIssuerSigningKey = true,
                        //ValidAudiences = new string[] { "Manning_app", "waveapi" }
                    };
                })
                .AddJwtBearer(AuthenticationSchemas.SeafarerIdp, options =>
                {
                    options.Authority = jwtBearerConfiguration.SeafarerIdp.AuthorityUrl;
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidIssuer = jwtBearerConfiguration.SeafarerIdp.ValidIssuer,
                        ValidateIssuer = true,
                        ValidateLifetime = false,
                        ValidateAudience = true,
                        ValidateIssuerSigningKey = true,
                        ValidAudiences = new string[] {  "waveapi" }
                    };
                });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env,synergy_manningContext manningContext)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();
            app.UseMiddleware<ExceptionMiddleware>();
            app.UseSwagger();

            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint(url: "/swagger/v1/swagger.json", name: "Relief Center Api");
            });

            //For DB creation
            manningContext.Database.Migrate();

            app.UseRouting();

            app.UseCors("MyPolicy");

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
