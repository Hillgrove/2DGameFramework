using _2DGameFramework.Core.Objects;

namespace _2DGameFramework.Core
{
    public class World
    {
        public int MaxX { get; init; }
        public int MaxY { get; init; }
        public Position? Position { get; private set; }

        private readonly List<Creature> _creatures = new();
        private readonly List<WorldObject> _objects = new();

        public World(int maxX, int maxY)
        {
            MaxX = maxX;
            MaxY = maxY;
        }

        public void AddObject(WorldObject obj)
        {
            _objects.Add(obj);
        }

        public void AddCreature(Creature creature)
        {
            _creatures.Add(creature);
        }


        public IEnumerable<Creature> GetCreatures() => _creatures;
        public IEnumerable<WorldObject> GetObjects() => _objects;
    }
}
