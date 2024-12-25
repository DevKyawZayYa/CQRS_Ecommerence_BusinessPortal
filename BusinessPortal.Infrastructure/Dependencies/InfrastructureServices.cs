using Microsoft.Extensions.DependencyInjection;
using BusinessPortal.Application.Interface.Persistence;
using BusinessPortal.Persistence.Contexts;
using BusinessPortal.Persistence.Repositories;

namespace BusinessPortal.Infrastructure
{
    public static class InfrastructureServices
    {
        public static IServiceCollection AddInfrastructureServices(this IServiceCollection services)
        {
            // Register Persistence Layer
            services.AddSingleton<DapperContext>();
            services.AddScoped<ICustomerRepository, CustomerRepository>();
            services.AddScoped<IUserRepository, UserRepository>(); // Add this line
            services.AddScoped<IUnitOfWork, UnitOfWork>();

            // Register Other Services (e.g., Email, Logging)
            // services.AddScoped<IEmailService, EmailService>();
            // services.AddSingleton<ILoggerService, LoggerService>();

            return services;
        }
    }
}
