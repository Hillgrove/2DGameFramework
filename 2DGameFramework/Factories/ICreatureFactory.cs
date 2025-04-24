using _2DGameFramework.Core;
using _2DGameFramework.Domain.Creatures;

namespace _2DGameFramework.Factories
{
    /// <summary>
    /// Creates a new <see cref="Creature"/> instance with the specified initial properties.
    /// Dependencies such as inventory management, damage calculation, and logging are injected automatically.
    /// </summary>
    public interface ICreatureFactory
    {
        /// <summary>
        /// Creates a new <see cref="Creature"/> instance with the specified initial properties.
        /// Dependencies such as inventory management, damage calculation, and logging are injected automatically.
        /// </summary>
        /// <param name="name">The display name of the creature.</param>
        /// <param name="position">The starting position of the creature in the world.</param>
        /// <param name="hitpoints">The initial and maximum hit points of the creature.</param>
        /// <param name="description">An optional description providing additional details about the creature.</param>
        /// <returns>A fully initialized <see cref="Creature"/> instance.</returns>
        Creature Create(string name, string description, int hitpoints, Position position, double autoHealThreshold = 0.25);
    }
}
