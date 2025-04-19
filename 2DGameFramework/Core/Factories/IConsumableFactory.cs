using _2DGameFramework.Core.Creatures;
using _2DGameFramework.Core.Objects;

namespace _2DGameFramework.Core.Factories
{
    public interface IConsumableFactory
    {
        /// <summary>
        /// Creates a new consumable item with the specified parameters.
        /// </summary>
        /// <param name="name">The name of the consumable item.</param>
        /// <param name="description">A description of the consumable's effect or use.</param>
        /// <param name="effect">The action applied to the creature when the consumable is used.</param>
        /// <returns>A new <see cref="Consumable"/> object configured with the provided parameters.</returns>
        Consumable CreateConsumable(string name, string description, Action<ICreature> effect);
    }
}
