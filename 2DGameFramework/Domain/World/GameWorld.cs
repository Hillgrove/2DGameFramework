using _2DGameFramework.Configuration;
using _2DGameFramework.Core;
using _2DGameFramework.Domain.Creatures;
using _2DGameFramework.Interfaces;
using _2DGameFramework.Logging;
using System.Diagnostics;

namespace _2DGameFramework.Domain.World
{
    /// <summary>
    /// Represents the 2D game world, managing its dimensions, creatures, and environment objects.
    /// </summary>
    public class GameWorld
    {
        public int WorldWidth { get; }
        public int WorldHeight { get; }
        public GameLevel GameLevel { get; }

        private readonly ILogger _logger;
        private readonly List<ICreature> _creatures = new();
        private readonly List<EnvironmentObject> _objects = new();

        /// <summary>
        /// Initializes a new instance of the <see cref="GameWorld"/> class using the specified settings and logger.
        /// </summary>
        /// <param name="settings">The configuration settings for the world, including dimensions and game level.</param>
        /// <param name="logger">The logger to record world events.</param>
        /// Initializes a new instance of the <see cref="GameWorld"/> class.
        public GameWorld(WorldSettings settings, ILogger logger)
        {
            WorldWidth = settings.WorldWidth;
            WorldHeight = settings.WorldHeight;
            GameLevel = settings.GameLevel;

            _logger = logger;
            _logger.Log(
                TraceEventType.Information,
                LogCategory.World,
                $"World created: {WorldWidth}x{WorldHeight}, Level={GameLevel}");
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
        public void AddCreature(ICreature creature)
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
        /// Removes the specified creature from the world.
        /// </summary>
        public void RemoveCreature(ICreature creature)
        {
            _creatures.Remove(creature);
            _logger.Log(TraceEventType.Information, LogCategory.World,
            $"Creature '{creature.Name}' removed from world.");
        }

        /// <summary>
        /// Returns all creatures currently in the world.
        /// </summary>
        /// <returns>An enumerable of <see cref="Creature"/> instances.</returns>
        public IEnumerable<ICreature> GetCreatures() => _creatures;

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
