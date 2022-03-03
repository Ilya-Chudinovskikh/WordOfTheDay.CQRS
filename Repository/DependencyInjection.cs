using Microsoft.Extensions.DependencyInjection;
using Application.Interfaces;
using Repository.Context;
using Microsoft.EntityFrameworkCore;
using System;
using MassTransit;

namespace Repository
{
    public static class DependencyInjection
    {
        public static void AddWordsMongoDb(this IServiceCollection services)
        {
            services.AddScoped<IWordsMongoDb, WordsMongoDb>();

            services.AddScoped<ConfigureWordsMongoDbIndexesService>();
        }
        public static void AddWordsDbContext(this IServiceCollection services, string connectionString)
        {
            services.AddDbContext<WordsDbContext>(options =>
                options.UseSqlServer(connectionString).LogTo(Console.WriteLine));

            services.AddScoped<IWordsDbContext, WordsDbContext>();
        }
        public static void AddConfiguredMassTransitConsumer(this IServiceCollection services, string host)
        {
            services.AddMassTransit(configuration =>
            {
                configuration.AddConsumer<WordConsumer>();

                configuration.UsingRabbitMq((context, config) =>
                {
                    config.Host(host);

                    config.ReceiveEndpoint("word-queue", cnfg =>
                    {
                        cnfg.ConfigureConsumer<WordConsumer>(context);
                    });
                });
            });

            services.AddMassTransitHostedService();
        }
    }
}
