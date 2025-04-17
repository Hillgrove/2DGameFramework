using _2DGameFramework.Models.Base;
using System.Diagnostics;


namespace _2DGameFramework.Configuration
{
    public class ListenerConfig
    {
        public required string Type { get; set; }
        public SourceLevels? FilterLevel { get; set; }
        public Dictionary<string, string> Settings { get; set; } = new Dictionary<string, string>();
    }


    public class GameConfig
    {
        public int WorldWidth { get; set; } = 50;
        public int WorldHeight { get; set; } = 50;
        public GameLevel GameLevel { get; set; } = GameLevel.Normal;

        public SourceLevels LogLevel { get; set; } = SourceLevels.All;
        public List<ListenerConfig> Listeners { get; set; } = new List<ListenerConfig>();
    }
}
