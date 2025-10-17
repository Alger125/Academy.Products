using Academy.Products.Infrastructure.InjectionManager;
using Microsoft.Extensions.DependencyInjection;

namespace Academy.Products.Infrastructure;

public static class DependencyInjection
{
    // Acceder a las configuraciones del appsettings.json
    public static IServiceCollection AddInfrastructureService(this IServiceCollection services)
    {
        RepositoriesManager.AddRepositories(services);
        return services;
    }
}
