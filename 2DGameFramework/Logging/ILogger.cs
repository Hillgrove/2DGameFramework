using System.Diagnostics;

namespace _2DGameFramework.Logging
{
    /// <summary>
    /// Defines a logger capable of writing tracing events with various levels and categories.
    /// </summary>
    public interface ILogger
    {
        /// <summary>
        /// Logs a message under a specific <see cref="TraceEventType"/> and <see cref="LogCategory"/>.
        /// The <paramref name="offset"/> is added to the base event ID.
        /// </summary>
        /// <param name="level">The severity level of the event (e.g., Error, Warning, Informational).</param>
        /// <param name="category">A custom category for grouping related log entries.</param>
        /// <param name="message">The content of the log entry.</param>
        /// <param name="offset">
        /// An optional value to add to the base event ID (default 1).  
        /// For example, offset 0 → event ID 5000, offset 1 → 5001, etc.
        /// </param>
        void Log(
            TraceEventType level,
            LogCategory category,
            string message,
            int offset = 1);
    }
}