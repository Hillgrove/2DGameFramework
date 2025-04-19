using _2DGameFramework.Core.Creatures;

namespace _2DGameFramework.Services
{
    /// <summary>
    /// Default implementation: Attacker total attack - Defender total defense.
    /// </summary>
    public class DamageCalculator : IDamageCalculator
    {
        ///<inheritdoc/>
        public int CalculateDamage(ICombatStats attacker, ICombatStats defender)
        {
            int attack = attacker.GetTotalBaseDamage();
            int defense = defender.GetTotalDamageReduction();
            return Math.Max(0, attack - defense);
        }
    }
}
