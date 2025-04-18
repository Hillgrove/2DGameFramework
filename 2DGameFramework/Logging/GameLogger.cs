using System.Diagnostics;

namespace _2DGameFramework.Logging
{
    /// <summary>
    /// Provides centralized logging functionality for the 2DGameFramework,
    /// wrapping a <see cref="TraceSource"/> configured with default listeners.
    /// </summary>
    public static class GameLogger
    {
        private static readonly TraceSource _ts = new("2DGameFramework", SourceLevels.All);

        static GameLogger()
        {
            var consoleLogger = new ConsoleTraceListener();
            consoleLogger.Filter = new EventTypeFilter(SourceLevels.All);
            _ts.Listeners.Add(consoleLogger);
        }

        /// <summary>
        /// Logs a message at the specified <see cref="TraceEventType"/> and <see cref="LogCategory"/>.
        /// </summary>
        /// <param name="level">The severity level of the log event.</param>
        /// <param name="category">A category enum used to distinguish message types.</param>
        /// <param name="message">The textual content of the log entry.</param>
        /// <param name="offset">
        /// An integer offset added to the <c>category</c> value to form the event ID.
        /// Defaults to <c>1</c>.
        /// </param>
        public static void Log(TraceEventType level, LogCategory category, string message, int offset = 1)
        {
            int id = (int)category + offset;
            _ts.TraceEvent(level, id, message);
            _ts.Flush();
        }

        public static TraceSource Trace => _ts;
    }
}
