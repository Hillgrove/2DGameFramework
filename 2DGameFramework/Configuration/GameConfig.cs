using _2DGameFramework.Models.Base;


namespace _2DGameFramework.Configuration
{
    public class GameConfig
    {
        public int WorldWidth { get; set; } = 50;
        public int WorldHeight { get; set; } = 50;
        public GameLevel GameLevel { get; set; } = GameLevel.Normal;
    }
}
