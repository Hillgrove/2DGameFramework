using System.Diagnostics;

namespace _2DGameFramework.Logging
{
    /// <summary>
    /// Adapts a <see cref="TraceSource"/> to the <see cref="ILogger"/> interface,
    /// allowing framework logging to use custom trace sources.
    /// </summary>
    public class GameLoggerAdapter : ILogger
    {
        private readonly TraceSource _trace;

        /// <summary>
        /// Initializes a new instance of the <see cref="GameLoggerAdapter"/> class
        /// using the specified <see cref="TraceSource"/>.
        /// </summary>
        /// <param name="trace">The trace source to which log events will be forwarded.</param>
        public GameLoggerAdapter(TraceSource trace)
        {
            _trace = trace;
        }

        /// <summary>
        /// Logs a message to the underlying <see cref="TraceSource"/> at the specified
        /// <see cref="TraceEventType"/> and <see cref="LogCategory"/>.
        /// </summary>
        /// <param name="level">The severity level of the log event.</param>
        /// <param name="category">A category enum used to distinguish message types.</param>
        /// <param name="message">The textual content of the log entry.</param>
        /// <param name="offset">
        /// An integer offset added to the <c>category</c> value to form the event ID.
        /// Defaults to <c>1</c>.
        /// </param>
        public void Log(TraceEventType level, LogCategory category, string message, int offset = 1)
        {
            int id = (int)category + offset;
            _trace.TraceEvent(level, id, message);
            _trace.Flush();
        }
    }
}
