using _2DGameFramework.Configuration;
using _2DGameFramework.Extensions;
using _2DGameFramework.Logging;
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

            // Load config and set up logging
            var (loggerAdapter, worldsettings) = InitializeLoggingAndConfiguration();

            // register services
            services.Add2DGameFramework();
            services.AddSingleton<ILogger>(loggerAdapter);
            services.AddSingleton(worldsettings);

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
