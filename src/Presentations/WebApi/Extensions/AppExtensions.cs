using FluentValidation;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SharpGrip.FluentValidation.AutoValidation.Mvc.Extensions;
using WebApi.Helpers;
using WebApi.Helpers.Validators;
using WebApi.Middlewares;

namespace WebApi.Extensions
{
    public static class AppExtensions
    {
        public static void UseErrorHandlingMiddleware(this IApplicationBuilder app)
        {
            app.UseMiddleware<ErrorHandlerMiddleware>();
        }
        
        public static void AddMappingProfiles(this IServiceCollection services)
        {
            services.AddAutoMapper(typeof(MappingProfiles));
            services.AddAutoMapper(typeof(ShopMappingProfile));
        }
        
        public static void AddValidators(this IServiceCollection services)
        {
            services.AddValidatorsFromAssemblyContaining<Program>();
            services.AddFluentValidationAutoValidation(configuration =>
            {
                configuration.OverrideDefaultResultFactoryWith<ValidationResultFactory>();
            });
            
        }
    }
}