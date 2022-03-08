using MediatR;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;
using Application.Interfaces;
using Application.LocationService;
using Application.DateService;

namespace Application
{
    public static class DependencyInjection
    {
        public static void AddApplication(this IServiceCollection services)
        {
            services.AddMediatR(Assembly.GetExecutingAssembly());
        }
        public static void AddMockLocation(this IServiceCollection services)
        {
            services.AddSingleton<IMockLocation, MockLocation>();
        }
        public static void AddDateToday(this IServiceCollection services)
        {
            services.AddScoped<IDateTodayService, DateTodayService>();
        }
    }
}
