using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Cooker.Domain.IocTag;
using Cooker.Domain.Repository;
using Microsoft.Extensions.DependencyInjection;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class IServiceCollectionExtensions
    {
        public static IServiceCollection AddRepository(this IServiceCollection services)
        {
            var instance = Assembly.Load("Cooker.Core.MySql");
            var intfer = Assembly.Load("Cooker.Domain");
            var implementType = instance.GetTypes().FirstOrDefault(m => m.Name == "Repository`2");
            // var interfaceType = implementType.GetInterface("IRepository`1");
            var interfaceType = intfer.GetTypes().FirstOrDefault(m => m.Name == "IRepository`2").GetGenericTypeDefinition();
            if (implementType != null && interfaceType != null)
            {
                services.AddScoped(interfaceType, implementType);
            }
            return services;
        }
    }
}