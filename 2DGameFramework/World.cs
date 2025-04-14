using _2DGameFramework.Models;

namespace _2DGameFramework
{
    public class World
    {
        public int WorldWidth { get; }
        public int WorldHeight { get; }

        private readonly List<Creature> _creatures = new();
        private readonly List<WorldObject> _objects = new();

        public World(int width, int height)
        {
            WorldWidth = width;
            WorldHeight = height;
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

        public void RemoveObject(WorldObject obj)
        {
            if (obj.IsRemovable)
            {
                _objects.Remove(obj);
            }
        }
    }
}
