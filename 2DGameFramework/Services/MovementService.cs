using _2DGameFramework.Core;
using _2DGameFramework.Domain.World;
using _2DGameFramework.Interfaces;

namespace _2DGameFramework.Services
{
    public class MovementService : IMovementService
    {
        ///<inheritdoc/>
        public Position Move(Position current, int dx, int dy, GameWorld world)
        {
            int newX = Math.Clamp(current.X + dx, 0, world.WorldWidth);
            int newY = Math.Clamp(current.Y + dy, 0, world.WorldHeight);
            return current with { X = newX, Y = newY };
        }
    }
}
