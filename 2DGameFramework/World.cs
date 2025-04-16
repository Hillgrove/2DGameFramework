using _2DGameFramework.Logging;
using _2DGameFramework.Models;
using _2DGameFramework.Models.Base;
using System.Diagnostics;

namespace _2DGameFramework
{
    public class World
    {
        public int WorldWidth { get; }
        public int WorldHeight { get; }
        public GameLevel GameLevel { get; }

        private readonly List<Creature> _creatures = new();
        private readonly List<EnvironmentObject> _objects = new();

        public World(int width, int height, GameLevel level = GameLevel.Normal)
        {
            WorldWidth = width;
            WorldHeight = height;
            GameLevel = level;

            GameLogger.Log(
                TraceEventType.Information, 
                LogCategory.World, 
                $"World created: {width}x{height}, Level={level}");
        }

        public void AddObject(EnvironmentObject obj)
        {
            _objects.Add(obj);

            GameLogger.Log(
                TraceEventType.Information,
                LogCategory.World,
                $"Object '{obj.Name}' added at {obj.Position}");
        }

        public void AddCreature(Creature creature)
        {
            _creatures.Add(creature);

            GameLogger.Log(
                TraceEventType.Information, 
                LogCategory.World, 
                $"Creature '{creature.Name}' added at {creature.Position}");
        }

        public IEnumerable<Creature> GetCreatures() => _creatures;
        
        public IEnumerable<EnvironmentObject> GetObjects() => _objects;

        public void RemoveObject(EnvironmentObject obj)
        {
            if (obj.IsRemovable)
            {
                _objects.Remove(obj);
                GameLogger.Log(
                    TraceEventType.Information, 
                    LogCategory.World, 
                    $"Removable object '{obj.Name}' removed from world");
            }
        }
    }
}
