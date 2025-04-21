using _2DGameFramework.Core;
using _2DGameFramework.Domain.Objects;

namespace _2DGameFramework.Factories
{
    public interface ITrapFactory
    {
        /// <summary>
        /// Creates a new trap instance with the specified parameters.
        /// </summary>
        /// <param name="name">The name of the trap.</param>
        /// <param name="description">A description of the trap's effect or purpose.</param>
        /// <param name="damageAmount">The amount of damage the trap inflicts when triggered.</param>
        /// <param name="position">The position in the world where the trap is placed.</param>
        /// <returns>A new <see cref="Trap"/> object configured with the provided parameters.</returns>
        Trap CreateTrap(string name, string description, int damageAmount, DamageType damageType, Position position, bool isRemovable = false);
    }
}