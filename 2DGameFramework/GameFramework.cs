﻿using _2DGameFramework.Configuration;
using _2DGameFramework.Extensions;
using _2DGameFramework.Factories;
using _2DGameFramework.Interfaces;
using _2DGameFramework.Logging;
using Microsoft.Extensions.DependencyInjection;
using System.Diagnostics;

namespace _2DGameFramework
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
        public static ServiceProvider Start(string configFilePath, string traceSourceName)
        {
            var services = new ServiceCollection();

            // 1) config + logging
            var (loggerAdapter, worldsettings) = InitializeLoggingAndConfiguration(configFilePath, traceSourceName);
            services.AddSingleton(loggerAdapter);
            services.AddSingleton(worldsettings);

            // 2) Core framework services (including ICombatService, IDamageCalculator, etc.)
            services.Add2DGameFramework();

            services.AddSingleton<IFactory<IConsumable>>(sp =>
            {
                var combat = sp.GetRequiredService<ICombatService>();

                var factory = new Factory<IConsumable>();

                return factory;
            });

            // 3) Expose generic factories via DI
            services.AddSingleton<IFactory<IWeapon>>(new Factory<IWeapon>());
            services.AddSingleton<IFactory<IArmor>>(new Factory<IArmor>());
            services.AddSingleton<IFactory<IConsumable>>(new Factory<IConsumable>());

            return services.BuildServiceProvider();
        }


        private static (ILogger loggerAdapter, WorldSettings worldSettings) InitializeLoggingAndConfiguration(
            string configFilePath,
            string traceSourceName)
        {
            var loader = new ConfigurationLoader();
            var (worldSettings, loggerSettings) = loader.Load(configFilePath);

            var trace = new TraceSource(traceSourceName)
            {
                Switch = { Level = loggerSettings.LogLevel } // uses that global setting
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

                    listener.Filter = new EventTypeFilter(ListenerConfig.FilterLevel);
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
