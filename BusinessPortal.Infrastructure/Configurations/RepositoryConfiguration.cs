using BusinessPortal.Application.Interface.Persistence;
using BusinessPortal.Persistence.Contexts;
using BusinessPortal.Persistence.Repositories;
using Microsoft.Extensions.DependencyInjection;

namespace BusinessPortal.Persistence.Configurations
{
    public static class RepositoryConfiguration
    {
        public static IServiceCollection ConfigureRepositories(this IServiceCollection services)
        {
            // Register generic repositories
            services.AddScoped(typeof(IGenericReadRepository<>), typeof(GenericReadRepository<>));
            services.AddScoped(typeof(IGenericWriteRepository<>), typeof(GenericWriteRepository<>));

            services.AddSingleton<DapperContext>();
            // Register UnitOfWork
            services.AddScoped<IUnitOfWork, UnitOfWork>();

            return services;
        }
    }
}
