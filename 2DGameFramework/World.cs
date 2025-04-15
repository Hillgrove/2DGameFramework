using _2DGameFramework.Models;
using _2DGameFramework.Models.Base;

namespace _2DGameFramework
{
    public class World
    {
        public int WorldWidth { get; }
        public int WorldHeight { get; }

        private readonly List<Creature> _creatures = new();
        private readonly List<EnvironmentObject> _objects = new();

        public World(int width, int height)
        {
            WorldWidth = width;
            WorldHeight = height;
        }

        public void AddObject(EnvironmentObject obj)
        {
            _objects.Add(obj);
        }

        public void AddCreature(Creature creature)
        {
            _creatures.Add(creature);
        }

        public IEnumerable<Creature> GetCreatures() => _creatures;
        
        public IEnumerable<EnvironmentObject> GetObjects() => _objects;

        public void RemoveObject(EnvironmentObject obj)
        {
            if (obj.IsRemovable)
            {
                _objects.Remove(obj);
            }
        }
    }
}
