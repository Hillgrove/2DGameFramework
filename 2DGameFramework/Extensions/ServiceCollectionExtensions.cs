using _2DGameFramework.Combat;
using _2DGameFramework.Core;
using _2DGameFramework.Core.Factories;
using _2DGameFramework.Services;
using Microsoft.Extensions.DependencyInjection;

namespace _2DGameFramework.Extensions
{
    /// <summary>
    /// Provides an extension method to register all 2DGameFramework services.
    /// </summary>
    internal static class ServiceCollectionExtensions
    {
        /// <summary>
        /// Adds all necessary framework services to the service collection.
        /// </summary>
        /// <param name="services">The service collection to which services will be added.</param>
        /// <returns>The updated service collection.</returns>
        public static IServiceCollection Add2DGameFramework(this IServiceCollection services)
        {
            // Core services
            services.AddSingleton<IDamageCalculator, DamageCalculator>();
            services.AddTransient<IInventory, InventoryService>();

            // Factories
            services.AddSingleton<ICreatureFactory, CreatureFactory>();
            services.AddSingleton<IConsumableFactory, ConsumableFactory>();
            services.AddSingleton<ITrapFactory, TrapFactory>();

            // World
            services.AddSingleton<World>();

            return services;
        }
    }
}
