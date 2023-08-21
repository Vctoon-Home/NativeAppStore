using System.Reflection;
using Microsoft.Extensions.DependencyInjection;
using NativeAppStore.Core;

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

            services.AddSingleton(type);
        }
    }

    public static void AddStores(this IServiceCollection services, IEnumerable<Assembly> storeAssemblies,
        Action<StoreOptions>? storeOptions = null)
    {
        var storeTypes = new List<Type>();

        // 添加正在执行的程序集
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