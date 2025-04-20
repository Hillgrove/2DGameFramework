using _2DGameFramework.Core;
using _2DGameFramework.Core.Factories;
using _2DGameFramework.Core.Interfaces;
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
            services.AddSingleton<IStatsService, StatsService>();
            services.AddSingleton<ICombatService, CombatService>();
            services.AddSingleton<IMovementService, MovementService>();
            services.AddSingleton<IDamageCalculator, DamageCalculator>();
            services.AddTransient<IInventoryService, InventoryService>();

            // Factories
            services.AddSingleton<ITrapFactory, TrapFactory>();
            services.AddSingleton<ICreatureFactory, CreatureFactory>();

            // World
            services.AddSingleton<World>();

            return services;
        }
    }
}
