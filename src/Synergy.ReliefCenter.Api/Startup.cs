using AutoMapper;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Synergy.AdobeSign;
using Synergy.ReliefCenter.Api.Helpers;
using Synergy.ReliefCenter.Api.Mappers;
using Synergy.ReliefCenter.Api.Validations;
using Synergy.ReliefCenter.Data.Models;
using Synergy.ReliefCenter.Services.Mappers;
using Synergy.Core.EmailService;
using Microsoft.Extensions.Logging;
using System;
using Synergy.ReliefCenter.Data;
using Microsoft.EntityFrameworkCore;
using Synergy.ReliefCenter.Api.Filter;
using FluentValidation.AspNetCore;

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

            services.AddSingleton<ILogger<EmailSender>>(loggerFactory.CreateLogger<EmailSender>());
            var emailLogger = loggerFactory.CreateLogger<IEmailService>();

            services.AddSingleton<ILogger<IEmailService>>(emailLogger);
            services.AddEmailService(Configuration);

            var mapperConfig = new MapperConfiguration(mc =>
            {
                mc.AddProfile(new EntityMappingProfile());
                mc.AddProfile(new MappingProfile(Configuration));
            });

            IMapper mapper = mapperConfig.CreateMapper();
            services.AddSingleton(mapper);
            services.AddSingleton(Configuration);
            services.AddScoped<IAdobeSignRestClient, AdobeSignRestClient>();

            services.AddMvc(Options => {
                Options.Filters.Add(new ValidationFilter());
            }).AddFluentValidation(Options =>
            {
                Options.RegisterValidatorsFromAssemblyContaining<Startup>();
            });

            services.AddAdobeSign(Configuration);   //For Adobe Sign
            services.AddExternalApi(Configuration);   //For ExternalApi 
            services.AddHttpClient();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, synergy_manningContext manningContext)
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

            app.UseAuthentication();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
