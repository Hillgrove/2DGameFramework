using _2DGameFramework.Core;

namespace _2DGameFramework.Interfaces
{
    /// <summary>
    /// Defines an immutable source of damage (e.g. a weapon, spell, trap, environmental hazard).
    /// </summary>
    public interface IDamageSource
    {
        /// <summary>
        /// The amount of damage this source contributes.
        /// </summary>
        int BaseDamage { get; }

        /// <summary>
        /// The type of damage this source inflicts.
        /// </summary>
        DamageType DamageType { get; }
    }
}
