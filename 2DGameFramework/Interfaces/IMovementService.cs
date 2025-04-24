using _2DGameFramework.Core;
using _2DGameFramework.Domain.World;

namespace _2DGameFramework.Interfaces
{
    /// <summary>
    /// Provides movement logic for creatures within a world boundary.
    /// </summary>
    public interface IMovementService
    {
        /// <summary>
        /// Calculates the new position of a creature based on deltas and world constraints.
        /// </summary>
        /// <param name="current">Current position.</param>
        /// <param name="dx">Change in X coordinate.</param>
        /// <param name="dy">Change in Y coordinate.</param>
        /// <param name="world">The world providing boundary limits.</param>
        /// <returns>The clamped new position.</returns>
        Position Move(ICreature mover, Position current, int dx, int dy, GameWorld world);
    }
}
