using BusinessPortal.Application.UseCases.Users.Commands.RegisterUser;
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Mvc;

namespace BusinessPortal.Persistence.Configurations
{
    public static class FluentValidationConfiguration
    {
        public static IServiceCollection AddFluentValidationServices(this IServiceCollection services)
        {
            services.AddValidatorsFromAssemblyContaining<RegisterUserValidator>();

            return services;
        }
    }
}
