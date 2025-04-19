using _2DGameFramework.Core.Objects;


namespace _2DGameFramework.Core.Factories
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
        Trap CreateTrap(string name, int damageAmount, Position position, string? description = null, bool isRemovable = false);
    }
}