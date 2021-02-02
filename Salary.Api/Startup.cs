using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Salary.Api.Extensions;
using Salary.Business.Extensions;
using Salary.Data.Extensions;

namespace Salary.Api
{
    public class Startup
    {
        public IConfiguration Configuration { get; }
        public IWebHostEnvironment HostEnvironment { get; }

        public Startup(IWebHostEnvironment environment)
        {
            HostEnvironment = environment;

            var builder = new ConfigurationBuilder()
                .SetBasePath(HostEnvironment.ContentRootPath)
                .AddJsonFile("appsettings.json", false, true)
                .AddJsonFile($"appsettings.{HostEnvironment.EnvironmentName}.json", false, true)
                .AddEnvironmentVariables();

            if (HostEnvironment.IsDevelopment())
            {
                builder.AddUserSecrets<Startup>();
            }

            Configuration = builder.Build();
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.ConfigureDbContext(Configuration);

            services.ConfigureSwagger();

            services.ConfigureAspNetIdentity();
            services.AddJwtBearerAuthentication(Configuration);

            services.BuildCors();
            services.AddControllers()
                .AddJsonOptions(o => o.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter()))
                .AddFluentValidation(o =>
                {
                    o.RegisterValidatorsFromAssembly(typeof(Startup).Assembly);
                    o.RunDefaultMvcValidationAfterFluentValidationExecutes = false;
                    o.ValidatorOptions.LanguageManager.Enabled = false;
                });

            services.ConfigureHttpClient(Configuration);
            services.AddBusinessServices(Configuration);
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("swagger/v1/swagger.json", "Api V1 Docs");
                c.RoutePrefix = string.Empty;
            });

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();

            app.UseCors();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapGet("/ping", async context => { await context.Response.WriteAsync("Pong!"); });
                endpoints.MapControllers();
            });
        }
    }
}