using _2DGameFramework.Core.Base;

namespace _2DGameFramework.Core.Objects
{
    /// <summary>
    /// Represents a one‑handed melee weapon that can be used to attack at close range.
    /// </summary>
    public class Sword : WeaponBase
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Sword"/> class.
        /// </summary>
        /// <param name="name">The name of the sword.</param>
        /// <param name="hitdamage">The base damage this sword inflicts.</param>
        /// <param name="range">The attack range of the sword.</param>
        /// <param name="description">An optional description of the sword.</param>
        public Sword(string name, int hitdamage, int range, string? description)
            : base(name, description, hitdamage, range, WeaponType.OneHanded)
        {
        }
    }
}
