using _2DGameFramework.Models.Base;
using System.Diagnostics;

namespace _2DGameFramework.Interfaces
{
    public interface ILogger
    {
        /// <summary>
        /// Logs a message under a specific TraceEventType and category.
        /// The <paramref name="offset"/> is added to the base event ID.
        /// </summary>
        /// <param name="level">TraceEventType like Error, Warning, etc.</param>
        /// <param name="category">Your custom LogCategory enum.</param>
        /// <param name="message">The message text.</param>
        /// <param name="offset">
        /// Number added to the base event ID (default 0 → ID 5000, offset 1 → 5001, etc.).
        /// </param>
        void Log(
            TraceEventType  level,
            LogCategory     category,
            string          message,
            int             offset = 1);
    }
}