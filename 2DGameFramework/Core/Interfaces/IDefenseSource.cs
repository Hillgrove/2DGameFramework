namespace _2DGameFramework.Core.Interfaces
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
