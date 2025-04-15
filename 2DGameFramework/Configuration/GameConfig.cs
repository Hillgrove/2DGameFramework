using _2DGameFramework.Models;


namespace _2DGameFramework.Configuration
{
    public class GameConfig
    {
        public int WorldWidth { get; set; }
        public int WorldHeight { get; set; }
        public GameLevel GameLevel { get; set; } = GameLevel.Normal;
    }
}
