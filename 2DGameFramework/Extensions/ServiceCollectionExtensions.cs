using _2DGameFramework.Combat;
using _2DGameFramework.Core;
using _2DGameFramework.Logging;
using _2DGameFramework.Services;
using Microsoft.Extensions.DependencyInjection;

namespace _2DGameFramework.Extensions
{
    /// <summary>
    /// Provides an extension method to register all 2DGameFramework services.
    /// </summary>
    public static class ServiceCollectionExtensions
    {
        /// <summary>
        /// Adds all necessary framework services to the service collection.
        /// </summary>
        /// <param name="services">The service collection to which services will be added.</param>
        /// <returns>The updated service collection.</returns>
        public static IServiceCollection Add2DGameFrameWork(this IServiceCollection services)
        {
            // Logging
            services.AddSingleton<ILogger>(new GameLoggerAdapter(GameLogger.Trace));

            // Core services
            services.AddSingleton<IDamageCalculator, DamageCalculator>();
            services.AddTransient<IInventory, InventoryService>();

            // Factories
            // TODO: Fix
            //services.AddSingleton<ICreatureFactory, CreatureFactory>();

            // World
            services.AddSingleton<World>();

            return services;
        }
    }
}
