using _2DGameFramework.Core;

namespace _2DGameFramework.Interfaces
{
    /// <summary>
    /// Defines an defense source (e.g. shield, armor) reducing incoming damage.
    /// </summary>
    public interface IDefenseSource
    {
        /// <summary>
        /// The amount of damage this source mitigates.
        /// </summary>
        int DamageReduction { get; }

        /// <summary>
        /// The category of damage this source mitigates.
        /// </summary>
        DamageType DamageType { get; }
    }
}
