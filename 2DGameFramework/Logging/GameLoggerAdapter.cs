using _2DGameFramework.Interfaces;
using _2DGameFramework.Models.Base;
using System.Diagnostics;

namespace _2DGameFramework.Logging
{
    public class GameLoggerAdapter : ILogger
    {
        private readonly TraceSource _trace;

        public GameLoggerAdapter(TraceSource trace)
        {
            _trace = trace;
        }

        public void Log(TraceEventType level, LogCategory category, string message, int offset = 1)
        {
            int id = (int)category + offset;
            _trace.TraceEvent(level, id, message);
            _trace.Flush();
        }
    }
}
