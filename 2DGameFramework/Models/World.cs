using _2DGameFramework.Interfaces;
using _2DGameFramework.Models.Base;
using System.Diagnostics;

namespace _2DGameFramework.Models
{
    /// <summary>
    /// Represents the 2D game world, managing its dimensions, creatures, and environment objects.
    /// </summary>
    public class World
    {
        public int WorldWidth { get; }
        public int WorldHeight { get; }
        public GameLevel GameLevel { get; }

        private readonly ILogger _logger;
        private readonly List<Creature> _creatures = new();
        private readonly List<EnvironmentObject> _objects = new();

        /// <summary>
        /// Initializes a new instance of the <see cref="World"/> class.
        /// </summary>
        /// <param name="width">The width of the world.</param>
        /// <param name="height">The height of the world.</param>
        /// <param name="logger">The logger to record world events.</param>
        /// <param name="level">The game difficulty level.</param>
        public World(int width, int height, ILogger logger, GameLevel level = GameLevel.Normal)
        {
            WorldWidth = width;
            WorldHeight = height;
            GameLevel = level;

            _logger = logger;
            _logger.Log(
                TraceEventType.Information,
                LogCategory.World,
                $"World created: {width}x{height}, Level={level}");
        }

        /// <summary>
        /// Adds an environment object to the world after validating its position.
        /// </summary>
        /// <param name="obj">The environment object to add.</param>
        public void AddObject(EnvironmentObject obj)
        {
            ValidatePositionWithinBounds(obj);

            _objects.Add(obj);

            _logger.Log(
                TraceEventType.Information,
                LogCategory.World,
                $"Object '{obj.Name}' added at {obj.Position}");
        }

        /// <summary>
        /// Adds a creature to the world after validating its position.
        /// </summary>
        /// <param name="creature">The creature to add.</param>
        public void AddCreature(Creature creature)
        {
            ValidatePositionWithinBounds(creature);

            _creatures.Add(creature);

            _logger.Log(
                TraceEventType.Information,
                LogCategory.World,
                $"Creature '{creature.Name}' added at {creature.Position}");
        }

        /// <summary>
        /// Removes the specified environment object from the world if it is removable.
        /// </summary>
        /// <param name="obj">The object to remove.</param>
        public void RemoveObject(EnvironmentObject obj)
        {
            if (obj.IsRemovable)
            {
                _objects.Remove(obj);
                _logger.Log(
                    TraceEventType.Information,
                    LogCategory.World,
                    $"Removable object '{obj.Name}' removed from world");
            }
        }

        /// <summary>
        /// Returns all creatures currently in the world.
        /// </summary>
        /// <returns>An enumerable of <see cref="Creature"/> instances.</returns>
        public IEnumerable<Creature> GetCreatures() => _creatures;

        /// <summary>
        /// Returns all environment objects currently in the world.
        /// </summary>
        /// <returns>An enumerable of <see cref="EnvironmentObject"/> instances.</returns>
        public IEnumerable<EnvironmentObject> GetObjects() => _objects;

        private void ValidatePositionWithinBounds(IPositionable entity)
        {
            var pos = entity.Position;
            if (pos.X < 0 || pos.X > WorldWidth || pos.Y < 0 || pos.Y > WorldHeight)
            {
                throw new ArgumentOutOfRangeException(
                    nameof(entity),
                    pos,
                    $"Position {pos} is outside world bounds (0,0) to ({WorldWidth},{WorldHeight})");
            }
        }



    }
}
