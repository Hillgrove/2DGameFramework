using _2DGameFramework.Configuration;
using _2DGameFramework.Extensions;
using _2DGameFramework.Logging;
using Microsoft.Extensions.DependencyInjection;

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
        public static ServiceProvider Start(ILogger logger, WorldSettings settings)
        {
            var services = new ServiceCollection();

            // Add all 2DGameFramework services through the extension
            services.Add2DGameFramework();
            services.AddSingleton<ILogger>(logger);
            services.AddSingleton(settings);

            return services.BuildServiceProvider();
        }
    }
}
