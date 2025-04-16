using _2DGameFramework.Models.Base;
using System.Diagnostics;

namespace _2DGameFramework.Logging
{
    public static class GameLogger
    {
        private static readonly TraceSource _ts = new("2DGameFramework", SourceLevels.All);

        static GameLogger()
        {
            var consoleLogger = new ConsoleTraceListener();
            consoleLogger.Filter = new EventTypeFilter(SourceLevels.All);
            _ts.Listeners.Add(consoleLogger);
        }

        public static void Log(TraceEventType level, LogCategory category, string message, int offset = 1)
        {
            int id = (int)category + offset;
            _ts.TraceEvent(level, id, message);
            _ts.Flush();
        }

        public static TraceSource Trace => _ts;
    }
}
