namespace _2DGameFramework.Combat
{
    /// <summary>
    /// Provides total attack and defense power for a creature.
    /// </summary>
    public interface ICombatStats
    {
        int GetTotalBaseDamage();
        int GetTotalDamageReduction();
    }
}
