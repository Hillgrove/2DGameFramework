namespace _2DGameFramework.Combat
{
    /// <summary>
    /// Calculates the amount of damage one creature deals to another.
    /// </summary>
    public interface IDamageCalculator
    {
        int CalculateDamage(ICombatStats attacker, ICombatStats defender);
    }
}
