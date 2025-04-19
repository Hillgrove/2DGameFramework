namespace _2DGameFramework.Core.Creatures
{
    /// <summary>
    /// Provides total attack and defense power for a creature.
    /// </summary>
    public interface ICombatStats
    {
        /// <summary>
        /// Calculates the total base damage from all equipped attack items.
        /// </summary>
        /// <returns>The combined base damage of all equipped damage sources.</returns>
        int GetTotalBaseDamage();

        /// <summary>
        /// Calculates the total damage reduction from all equipped defense items.
        /// </summary>
        /// <returns>The combined damage reduction value of all equipped defense sources.</returns>
        int GetTotalDamageReduction();
    }
}
