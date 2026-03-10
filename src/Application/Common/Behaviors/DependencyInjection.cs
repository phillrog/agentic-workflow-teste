using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace Application.Common.Behaviors;

public static class DependencyInjection
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services)
    {
        // No .NET 10 / MediatR 12+, a forma correta de registrar é via RegisterServicesFromAssembly
        services.AddMediatR(cfg => {
            cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly());
            // Adicionando Validation Behaviors se houver
        });

        return services;
    }
}