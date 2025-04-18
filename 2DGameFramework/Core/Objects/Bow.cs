using _2DGameFramework.Core.Base;

namespace _2DGameFramework.Core.Objects
{
    /// <summary>
    /// Represents a two‑handed ranged weapon that can be used to attack at a distance.
    /// </summary>
    public class Bow : WeaponBase
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Bow"/> class.
        /// </summary>
        /// <param name="name">The name of the bow.</param>
        /// <param name="description">An optional description of the bow.</param>
        /// <param name="hitdamage">The base damage this bow inflicts.</param>
        /// <param name="range">The maximum range at which this bow can hit.</param>
        public Bow(string name, string? description, int hitdamage, int range)
            : base(name, description, hitdamage, range, WeaponType.TwoHanded)
        {
        }
    }
}
