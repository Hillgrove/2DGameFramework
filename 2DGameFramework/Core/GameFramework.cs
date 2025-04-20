using _2DGameFramework.Configuration;
using _2DGameFramework.Core.Factories;
using _2DGameFramework.Core.Interfaces;
using _2DGameFramework.Extensions;
using _2DGameFramework.Logging;
using _2DGameFramework.Services;
using Microsoft.Extensions.DependencyInjection;
using System.Diagnostics;

namespace _2DGameFramework.Core
{
    /// <summary>
    /// Provides a simple entry point for starting the 2DGameFramework.
    /// Hides all internal DI setup from the end user.
    /// </summary>
    public static class GameFramework
    {
        /// <summary>
        /// Starts the 2DGameFramework by configuring and building a ServiceProvider.
        /// </summary>
        /// <returns>A ServiceProvider containing all registered framework services.</returns>
        public static ServiceProvider Start()
        {
            var services = new ServiceCollection();

            // 1) config + logging
            var (loggerAdapter, worldsettings) = InitializeLoggingAndConfiguration();
            services.AddSingleton<ILogger>(loggerAdapter);
            services.AddSingleton(worldsettings);

            // 2) Core framework services (including ICombatService, IDamageCalculator, etc.)
            services.Add2DGameFramework();

            services.AddSingleton<IFactory<IUsable>>(sp =>
            {
                var combat = sp.GetRequiredService<ICombatService>();

                var factory = new Factory<IUsable>();

                return factory;
            });

            // 3) create and populate generic factories
            var weaponFactory = new Factory<IWeapon>();
            var armorFactory = new Factory<IArmor>();
            var usableFactory = new Factory<IUsable>();

            // expose via DI
            services.AddSingleton<IFactory<IWeapon>>(weaponFactory);
            services.AddSingleton<IFactory<IArmor>>(armorFactory);
            services.AddSingleton<IFactory<IUsable>>(usableFactory);

            return services.BuildServiceProvider();
        }

        
        private static (ILogger loggerAdapter, WorldSettings worldSettings) InitializeLoggingAndConfiguration()
        {
            var loader = new ConfigurationLoader();
            var (worldSettings, loggerSettings) = loader.Load("config.xml");

            var trace = new TraceSource("2DGameFramework")
            {
                Switch = { Level = loggerSettings.LogLevel }
            };

            if (loggerSettings.Listeners.Count > 0)
            {
                foreach (var ListenerConfig in loggerSettings.Listeners)
                {
                    TraceListener listener = ListenerConfig.Type switch
                    {
                        "Console" => new ConsoleTraceListener(),
                        "File" when ListenerConfig.Settings.TryGetValue("Path", out var path) => new TextWriterTraceListener(path),
                        _ => throw new InvalidOperationException($"Unknown listener type '{ListenerConfig.Type}'")
                    };
                    listener.Filter = new EventTypeFilter(loggerSettings.LogLevel);
                    trace.Listeners.Add(listener);
                }
            }
            else
            {
                trace.Listeners.Add(new ConsoleTraceListener());
            }

            var loggerAdapter = new GameLoggerAdapter(trace);

            return (loggerAdapter, worldSettings);
        }
    }
}
