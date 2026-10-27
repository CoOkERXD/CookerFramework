using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Cooker.Core.MySql;
using Cooker.Core.WorkUnit;
using Cooker.Domain.Repository;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class IServiceCollectionExtensions
    {
        public static IServiceCollection AddCookerDbContext<TDbContext>(this IServiceCollection services, IConfiguration configuration) where TDbContext : CookerDbContext
        {
            var mysqlConnection = configuration.GetConnectionString("Default");
            services.AddMySql<TDbContext>(mysqlConnection, ServerVersion.AutoDetect(mysqlConnection));
            services.AddScoped<CookerDbContext>(provider => provider.GetRequiredService<TDbContext>());
            return services;
        }
        public static IServiceCollection AddWorkUnit(this IServiceCollection services)
        {
            services.AddScoped(typeof(IWorkUnit), typeof(WorkUnit));
            return services;
        }
    }
}