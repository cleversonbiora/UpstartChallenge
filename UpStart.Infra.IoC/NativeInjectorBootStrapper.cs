using AutoMapper;
using UpStart.Application.Service.Services;
using UpStart.Domain.Interfaces.Service;
using UpStart.CrossCutting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace UpStart.Infra.IoC
{
    public class NativeInjectorBootStrapper
    {
        private const string NAMESPACEBASE = "UpStart";
        public static void RegisterServices(IServiceCollection services)
        {
            var Assemblies = AppDomain.CurrentDomain.GetAssemblies().Where(x => x.ManifestModule.Name.Contains(NAMESPACEBASE));


            #region [ Por algum motivo o IoC so enxerga as DLLs que ele esta utilizando no .NET 5. ]
            services.AddTransient<ILocationService, LocationService>();
            #endregion

            AutoInjector(services, Assemblies, "Application.Service");

            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            //services.AddSingleton(Mapper.Configuration);
            services.AddSingleton<IMapper>(sp => new Mapper(sp.GetRequiredService<IConfigurationProvider>(), sp.GetService));

        }

        private static void AutoInjector(IServiceCollection services, IEnumerable<Assembly> Assemblies, string prefix)
        {
            var serviceAssembly = Assemblies.Where(x => x.ManifestModule.Name.Contains(prefix)).FirstOrDefault();

            foreach (var type in serviceAssembly.ExportedTypes)
            {
                var interfaces = type.GetInterfaces().Where(x => x.Namespace.Contains(NAMESPACEBASE));
                if (type.Name.StartsWith("Base") || interfaces.Count() == 0)
                    continue;
                services.AddTransient(interfaces.First(), type);
            }
        }
    }
}
