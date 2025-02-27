using Caching;
using Core;
using Data.Mongo;
using HealthChecks.UI.Client;
using Identity;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Serilog;
using Services.Interfaces;
using WebApi.Extensions;
using WebApi.Helpers;
using WebApi.Services;

namespace WebApi
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
            services.AddMongo(Configuration);
            services.AddLogging(o => o.AddSerilog());
            services.AddIdentity(Configuration);
            services.AddSharedServices(Configuration);
            services.AddApplicationSqlServer(Configuration);
            services.AddRepoServices(Configuration);
            services.AddAppServices(Configuration);
            services.AddRedis(Configuration);
            services.AddScoped<IAuthenticatedUserService, AuthenticatedUserService>();
            services.AddMappingProfiles();
            services.AddValidators();
            services.AddCustomSwagger(Configuration);

            services.AddControllers();
            //.AddNewtonsoftJson(options =>
            //options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore
            //);

            services.AddHealthChecks()
                .AddRedis(Configuration.GetSection("RedisSettings:RedisConnectionString").Value,
                    name: "RedisHealt-check",
                    failureStatus: HealthStatus.Unhealthy,
                    tags: new string[] { "api", "Redis" })
                .AddSqlServer(Configuration.GetConnectionString("IdentityConnection"),
                    name: "identityDb-check",
                    failureStatus: HealthStatus.Unhealthy,
                    tags: new string[] { "api", "SqlDb" })
                .AddSqlServer(Configuration.GetConnectionString("DefaultConnection"),
                    name: "applicationDb-check",
                    failureStatus: HealthStatus.Unhealthy,
                    tags: new string[] { "api", "SqlDb" });


            services.AddAuthorization(options =>
            {
                options.AddPolicy("OnlyAdmins", policy => policy.RequireRole("SuperAdmin", "Admin"));
                options.AddPolicy("ShopOwner", policy => policy.RequireRole("Shopper"));
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, ILoggerFactory loggerFactory)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            //app.UseSerilogRequestLogging();
            loggerFactory.AddSerilog();

            app.UseHttpsRedirection();

            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();

            //error middleware
            app.UseErrorHandlingMiddleware();

            app.UseSwagger();
            app.UseSwaggerUI(c => { c.SwaggerEndpoint("/swagger/v1/swagger.json", "Service Api V1"); });

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
                endpoints.MapHealthChecks("/health", new HealthCheckOptions()
                {
                    Predicate = _ => true,
                    ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse
                });
            });
        }
    }
}