using _2DGameFramework.Core.Objects;

namespace _2DGameFramework.Core.Factories
{
    /// <summary>
    /// Factory for creating various types of weapons.
    /// </summary>
    public interface IWeaponFactory
    {
        /// <summary>
        /// Creates a new sword with the specified parameters.
        /// </summary>
        /// <param name="name">The name of the sword.</param>
        /// <param name="hitdamage">The base damage this sword inflicts.</param>
        /// <param name="range">The attack range of the sword.</param>
        /// <param name="description">An description of the sword.</param>
        /// <returns>A new instance of the <see cref="Sword"/> class.</returns>
        Sword CreateSword(string name, string description, int hitdamage, int range);

        /// <summary>
        /// Creates a new bow with the specified parameters.
        /// </summary>
        /// <param name="name">The name of the bow.</param>
        /// <param name="hitdamage">The base damage this bow inflicts.</param>
        /// <param name="range">The maximum range at which this bow can hit.</param>
        /// <param name="description">An description of the bow.</param>
        /// <returns>A new instance of the <see cref="Bow"/> class.</returns>
        Bow CreateBow(string name, string description, int hitdamage, int range);
    }
}
