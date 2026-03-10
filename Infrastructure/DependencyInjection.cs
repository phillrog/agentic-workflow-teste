using CleanArch.Application.Services;
using CleanArch.Domain.Repositories;
using CleanArch.Infrastructure.Persistence;
using CleanArch.Infrastructure.Persistence.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;

namespace CleanArch.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<ApplicationDbContext>(options =>
            options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));

        services.AddScoped<IUsuarioRepository, UsuarioRepository>();
        services.AddScoped<UsuarioService>();

        return services;
    }
}