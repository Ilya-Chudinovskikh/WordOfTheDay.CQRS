using MassTransit;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace Application
{
    public static class DependencyInjection
    {
        public static void AddApplication(this IServiceCollection services)
        {
            services.AddMediatR(Assembly.GetExecutingAssembly());
        }
        public static void AddConfiguredMassTransit(this IServiceCollection services, string host)
        {
            services.AddMassTransit(Configuration =>
            {
                Configuration.UsingRabbitMq((context, config) =>
                {
                    config.Host(host);
                });
            });

            services.AddMassTransitHostedService();
        }
    }
}
