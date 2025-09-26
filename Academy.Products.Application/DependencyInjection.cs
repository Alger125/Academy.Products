using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace Academy.Products.Application
{
    public static class DependencyInjection
    {
        // Extension method para registrar servicios de la capa de aplicación
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            // Registrar MediatR y escanear el ensamblado actual para handlers, requests, etc.
            // MediatR es una librería que implementa el patrón Mediator, facilitando la comunicación entre objetos
            services.AddMediatR(config =>
            {
                config.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly());
            });

            return services;
        }
    }
}
