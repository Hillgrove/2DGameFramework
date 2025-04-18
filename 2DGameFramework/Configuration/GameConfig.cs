using _2DGameFramework.Core;
using System.Diagnostics;


namespace _2DGameFramework.Configuration
{
    /// <summary>
    /// Holds the settings needed to configure a single log listener
    /// (type, optional filter level, and custom key/value settings).
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
    public class GameConfig
    {
        public int WorldWidth { get; set; } = 50;
        public int WorldHeight { get; set; } = 50;
        public GameLevel GameLevel { get; set; } = GameLevel.Normal;

        public SourceLevels LogLevel { get; set; } = SourceLevels.All;
        public List<ListenerConfig> Listeners { get; set; } = new List<ListenerConfig>();
    }
}
