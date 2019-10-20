using coreApi.Domain.Services;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace coreApi.IoC.Registries
{
    public static class CommonRegistry
    {
        public static void AddCommonServices(this IServiceCollection services)
        {
            //Services
            RegisterDependenciesFromAssemblyOf<IFinancialProductService>(services);
        }

        private static void RegisterDependenciesFromAssemblyOf<TType>(IServiceCollection services)
        {
            services.Scan(scan => scan
                .FromAssemblyOf<TType>()
                .AddClasses()
                .AsImplementedInterfaces()
                .AsSelf() //needs for BC.ClientValidation.Core
                .WithTransientLifetime());
        }
    }
}