using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Cooker.Domain.IocTag;
using Microsoft.Extensions.Configuration;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class IServiceCollectionExtensions
    {
        public static IServiceCollection AddService(this IServiceCollection services, string serviceName)
        {
            var assemblies = new[]
            {
        Assembly.Load($"{serviceName}.Application"),
        Assembly.Load("Cooker.Domain"),
        Assembly.Load($"{serviceName}.Domain"),
    };

            // 生命周期标记接口，不参与“业务接口”映射
            var markerInterfaces = new[]
            {
        typeof(IocTag), typeof(ITransient), typeof(IScoped), typeof(ISingleton)
    };

            foreach (var assembly in assemblies)
            {
                var implementTypes = assembly.GetTypes().Where(m =>
                    !m.IsAbstract &&
                    !m.IsInterface &&
                    m.IsAssignableTo(typeof(IocTag)));

                foreach (var implementType in implementTypes)
                {
                    // 业务接口 = 排除标记接口后的所有接口，去重
                    var serviceTypes = implementType.GetInterfaces()
                        .Where(i => !markerInterfaces.Contains(i))
                        .Distinct()
                        .ToList();

                    // 没有业务接口 → 注册具体类自身（关键：让 UserManager 这类领域服务可解析）
                    if (serviceTypes.Count == 0)
                    {
                        Registerr(services, implementType, implementType);
                        continue;
                    }

                    // 有业务接口 → 每个接口都映射到实现
                    foreach (var serviceType in serviceTypes)
                    {
                        Registerr(services, serviceType, implementType);
                    }
                }
            }

            return services;
        }
        private static void Registerr(IServiceCollection services, Type serviceType, Type implType)
        {
            if (implType.IsAssignableTo(typeof(ITransient)))
                services.AddTransient(serviceType, implType);
            else if (implType.IsAssignableTo(typeof(IScoped)))
                services.AddScoped(serviceType, implType);
            else if (implType.IsAssignableTo(typeof(ISingleton)))
                services.AddSingleton(serviceType, implType);
        }
    }

}