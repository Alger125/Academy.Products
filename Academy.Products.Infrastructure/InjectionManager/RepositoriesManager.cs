
using Academy.Products.Domain.Entities.ProductEntity.Repositories;
using Academy.Products.Infrastructure.Persistence.Repositories;
using Microsoft.Extensions.DependencyInjection;

namespace Academy.Products.Infrastructure.InjectionManager;

public static class RepositoriesManager
{
    public static IServiceCollection AddRepositories(this IServiceCollection services)
    {
        services.AddScoped<IProductRepository, ProductRepository>();

        return services;
    }
}
