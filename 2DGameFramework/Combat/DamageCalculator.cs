namespace _2DGameFramework.Combat
{
    /// <summary>
    /// Default implementation: Attacker total attack - Defender total defense.
    /// </summary>
    public class DamageCalculator : IDamageCalculator
    {
        public int CalculateDamage(ICombatStats attacker, ICombatStats defender)
        {
            int attack = attacker.GetTotalBaseDamage();
            int defense = defender.GetTotalDamageReduction();
            return Math.Max(0, attack - defense);
        }
    }
}
