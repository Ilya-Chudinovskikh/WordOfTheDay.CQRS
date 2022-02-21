using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Repository;
using Application;
using MediatR;
using System.Reflection;
using Application.Interfaces;
using Repository.QuriesMongoDb;
using Microsoft.Extensions.Options;

namespace WebApi
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();

            services.AddApplication();
            services.AddWordsDbContext(Configuration.GetConnectionString("WordsDbContext"));
            services.AddWordsMongoDb();
            services.AddConfiguredMassTransitConsumer(Configuration.GetConnectionString("RabbitMQHost"));

            services.AddMediatR(Assembly.GetExecutingAssembly());

            services.Configure<IQueriesMongoDbSettings>(
                Configuration.GetSection(nameof(QueriesMongoDbSettings)));

            services.AddSingleton<IQueriesMongoDbSettings>(sp =>
                sp.GetRequiredService<IOptions<QueriesMongoDbSettings>>().Value);
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseDefaultFiles();
            app.UseStaticFiles();

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
