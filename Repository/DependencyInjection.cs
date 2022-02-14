using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Interfaces;
using Repository.Context;
using Microsoft.EntityFrameworkCore;

namespace Repository
{
    public static class DependencyInjection
    {
        public static void AddRepository(this IServiceCollection services, string connectionString)
        {
            services.AddDbContext<WordsDbContext>(options =>
                options.UseSqlServer(connectionString).LogTo(Console.WriteLine));

            services.AddScoped<IWordsDbContext, WordsDbContext>();
        }
    }
}
