using BusinessPortal.Application.Interface.Persistence;
using BusinessPortal.Application.UseCases.Services;
using BusinessPortal.Application.UseCases.Users.Commands.CreateUserCommand;
using BusinessPortal.Application.UseCases.Users.Commands.LoginUser;
using BusinessPortal.Application.UseCases.Users.Commands.RegisterUser;
using BusinessPortal.Persistence.Contexts;
using BusinessPortal.Persistence.Repositories;
using FluentValidation;
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
            services.AddScoped<IUnitOfWork, UnitOfWork>();

            services.AddSingleton<DapperContext>();
            services.AddTransient<JwtTokenService>();

            // Consolidate MediatR registration
            services.AddMediatR(cfg =>
            {
                cfg.RegisterServicesFromAssembly(typeof(CreateUserHandler).Assembly);
                cfg.RegisterServicesFromAssembly(typeof(RegisterUserHandler).Assembly);
                cfg.RegisterServicesFromAssembly(typeof(LoginUserHandler).Assembly);
            });



            return services;
        }
    }
}
