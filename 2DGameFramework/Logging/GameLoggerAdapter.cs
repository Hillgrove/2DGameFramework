using _2DGameFramework.Interfaces;
using _2DGameFramework.Models.Base;
using System.Diagnostics;

namespace _2DGameFramework.Logging
{
    public class GameLoggerAdapter : ILogger
    {
        public void Log(TraceEventType level, LogCategory category, string message)
            => GameLogger.Log(level, category, message);
    }
}
