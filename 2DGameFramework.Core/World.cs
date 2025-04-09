namespace _2DGameFramework.Core
{
    public class World
    {
        public int MaxX { get; init; }
        public int MaxY { get; init; }

        private List<Creature> _creatures;
        private List<WorldObject> _objects;

        public World(int maxX, int maxY)
        {
            MaxX = maxX;
            MaxY = maxY;
        }
    }
}
