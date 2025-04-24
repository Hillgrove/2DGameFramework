using _2DGameFramework.Core;
using _2DGameFramework.Domain.World;
using _2DGameFramework.Interfaces;
using _2DGameFramework.Logging;
using System.Diagnostics;

namespace _2DGameFramework.Services
{
    public class MovementService : IMovementService
    {
        private readonly ILogger _logger;

        public MovementService(ILogger logger)
        {
            _logger = logger;
        }

        ///<inheritdoc/>
        public Position Move(ICreature mover, Position current, int dx, int dy, GameWorld world)
        {
            // Capture origin and intended positions
            var origin = current;
            var intended = new Position(current.X + dx, current.Y + dy);
            
            // Clamp into world bounds
            var clamped = new Position(
                Math.Clamp(intended.X, 0, world.WorldWidth),
                Math.Clamp(intended.Y, 0, world.WorldHeight)
            );


            var level = (intended != clamped)
                ? TraceEventType.Warning
                : TraceEventType.Information;

            // Log actual movement
            _logger.Log(
              level,
              LogCategory.Movement,
              $"[{mover.Name}] moved from {origin} to {clamped}" +
                (level == TraceEventType.Warning ? " (out‐of‐bounds clamped)" : "")
            );

            return clamped;
        }
    }
}
