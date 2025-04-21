using _2DGameFramework.Core;


namespace _2DGameFramework.Configuration
{
    /// <summary>
    /// Represents the configuration settings for initializing the game world,
    /// including its dimensions and difficulty level.
    /// </summary>
    public class WorldSettings
    {
        public int WorldWidth { get; init; } = 50;
        public int WorldHeight { get; init; } = 50;
        public GameLevel GameLevel { get; init; } = GameLevel.Normal;
    }
}
