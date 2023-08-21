using Microsoft.Extensions.DependencyInjection;
using NativeAppStore.Core;
using System.Reflection;

namespace NativeAppStore.Extensions;

public static class StoreExtensions
{
    internal static StoreOptions StoreOptions { get; set; } = new StoreOptions();

    public static void AddStores(this IServiceCollection services, IEnumerable<Type> storeTypes,
        Action<StoreOptions>? storeOptions = null)
    {
        storeOptions?.Invoke(StoreOptions);

        foreach (var type in storeTypes)
        {
            if (!typeof(IStore).IsAssignableFrom(type))
            {
                throw new ArgumentException($"Type {type} is not assignable from {typeof(IStore)}");
            }

            services.AddSingleton(typeof(IStore), type);

            services.AddSingleton(type, s =>
            {
                var store = (s.GetServices<IStore>().FirstOrDefault(x => x.GetType() == type) as StoreBase)!;

                if (StoreOptions.EnabledCreatorStoreLoad)
                    store.LoadStore();
                return store;
            });
        }
    }

    public static void AddStores(this IServiceCollection services, IEnumerable<Assembly> storeAssemblies,
        Action<StoreOptions>? storeOptions = null)
    {
        var storeTypes = new List<Type>();

        var assemblies = storeAssemblies;

        foreach (var assembly in assemblies)
        {
            foreach (var type in assembly.GetTypes())
            {
                if (typeof(StoreBase).IsAssignableFrom(type) && !type.IsInterface && !type.IsAbstract)
                {
                    storeTypes.Add(type);
                }
            }
        }

        services.AddStores(storeTypes, storeOptions);
    }

    public static void AddStores(this IServiceCollection services, Assembly storeAssembly,
        Action<StoreOptions>? storeOptions = null)
    {
        services.AddStores(new[] {storeAssembly}, storeOptions);
    }
}