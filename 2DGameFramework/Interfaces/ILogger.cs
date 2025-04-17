using _2DGameFramework.Models.Base;
using System.Diagnostics;

namespace _2DGameFramework.Interfaces
{
    public interface ILogger
    {
        public void Log(TraceEventType level, LogCategory category, string message)
            => Log(level, category, message);
    }
}