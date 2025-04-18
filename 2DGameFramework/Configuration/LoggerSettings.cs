using System.Diagnostics;


namespace _2DGameFramework.Configuration
{
    /// <summary>
    /// Represents the configuration settings for initializing the game world,
    /// including its dimensions and difficulty level.
    /// </summary>
    public class ListenerConfig
    {
        public required string Type { get; set; }
        public SourceLevels? FilterLevel { get; set; }
        public Dictionary<string, string> Settings { get; set; } = new Dictionary<string, string>();
    }

    /// <summary>
    /// Represents all adjustable parameters loaded from the XML configuration file,
    /// including world dimensions, difficulty level, and logging listeners.
    /// </summary>
    public class LoggerSettings
    {
        public SourceLevels LogLevel { get; set; } = SourceLevels.All;
        public List<ListenerConfig> Listeners { get; set; } = new List<ListenerConfig>();
    }
}
