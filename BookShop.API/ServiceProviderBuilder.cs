using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace BookShop.API
{
    public static class ServiceProviderBuilder
    {
        public static IServiceCollection AddBookShopServices(this IServiceCollection servcieCollection,
          string interfaceAssemblyShortName,
          string implementationAssemblyShortName)
        {
            var entryAssembly = Assembly.GetEntryAssembly();
            var refAssemblies = entryAssembly.GetReferencedAssemblies().ToList();

            // find all interface types
            var interfaceAssemblyName = refAssemblies.Single(o => o.Name.Equals(interfaceAssemblyShortName, StringComparison.OrdinalIgnoreCase));
            var interfaceAssembly = Assembly.Load(interfaceAssemblyName);
            var interfaceTypes = interfaceAssembly.DefinedTypes
                .Where(o => o.IsInterface)
                .Select(o => o.AsType())
                .ToList();

            // find all implementation types
            var implementationAssemblyName = refAssemblies.Single(o => o.Name.Equals(implementationAssemblyShortName, StringComparison.OrdinalIgnoreCase));
            var implementationAssembly = Assembly.Load(implementationAssemblyName);
            var implementationTypes = implementationAssembly.DefinedTypes
                .Where(o => o.IsPublic && o.IsClass && o.ImplementedInterfaces.Any())
                .ToList();

            // iterate through implementation types
            foreach (var implementationType in implementationTypes)
            {
                foreach (var implementedInterface in implementationType.ImplementedInterfaces)
                {
                    var matchingInterface = interfaceTypes.SingleOrDefault(o => o == implementedInterface);
                    if (matchingInterface != null && matchingInterface.Name == $"I{implementationType.Name}")
                    {
                        servcieCollection.AddScoped(implementedInterface, implementationType.AsType());
                    }
                }
            }

            return servcieCollection;
        }
    }
}
