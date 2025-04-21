using _2DGameFramework.Services;

namespace _2DGameFramework.Interfaces
{
    /// <summary>
    /// Calculates the amount of damage one creature deals to another.
    /// </summary>
    public interface IDamageCalculator
    {
        /// <summary>
        /// Calculates the damage dealt from one combatant to another based on their stats.
        /// </summary>
        /// <param name="attacker">The combatant attacking (e.g., a creature or character).</param>
        /// <param name="defender">The combatant being attacked (e.g., a creature or character).</param>
        /// <returns>
        /// The amount of damage that the attacker deals to the defender after considering both combatants' stats.
        /// </returns>
        int CalculateDamage(ICombatStats attacker, ICombatStats defender);
    }
}
