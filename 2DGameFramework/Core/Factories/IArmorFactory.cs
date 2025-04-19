using _2DGameFramework.Core.Base;

namespace _2DGameFramework.Core.Factories
{
    /// <summary>
    /// Factory interface for creating various types of armor.
    /// </summary>
    public interface IArmorFactory
    {
        /// <summary>
        /// Creates a new helmet with the specified parameters.
        /// </summary>
        /// <param name="name">The name of the helmet.</param>
        /// <param name="damageReduction">The damage reduction this helmet provides.</param>
        /// <param name="description">An description of the helmet.</param>
        /// <param name="damageType">The type of damage the helmet protects against (default is Physical).</param>
        /// <returns>A new instance of the <see cref="Helmet"/> class.</returns>
        ArmorBase CreateHelmet(string name, string description, int damageReduction, DamageType damageType = DamageType.Physical);

        /// <summary>
        /// Creates a new chest armor with the specified parameters.
        /// </summary>
        /// <param name="name">The name of the chest armor.</param>
        /// <param name="damageReduction">The damage reduction this chest armor provides.</param>
        /// <param name="description">An description of the chest armor.</param>
        /// <param name="damageType">The type of damage the chest armor protects against (default is Physical).</param>
        /// <returns>A new instance of the <see cref="Chestplate"/> class.</returns>
        ArmorBase CreateChestArmor(string name, string description, int damageReduction, DamageType damageType = DamageType.Physical);

        /// <summary>
        /// Creates a new boots item with the specified parameters.
        /// </summary>
        /// <param name="name">The name of the boots.</param>
        /// <param name="damageReduction">The damage reduction this boots provide.</param>
        /// <param name="description">An description of the boots.</param>
        /// <param name="damageType">The type of damage the boots protect against (default is Physical).</param>
        /// <returns>A new instance of the <see cref="Boots"/> class.</returns>
        ArmorBase CreateBoots(string name, string description, int damageReduction, DamageType damageType = DamageType.Physical);

        /// <summary>
        /// Creates a new leg armor with the specified parameters.
        /// </summary>
        /// <param name="name">The name of the leg armor.</param>
        /// <param name="damageReduction">The damage reduction this leg armor provides.</param>
        /// <param name="description">An description of the leg armor.</param>
        /// <param name="damageType">The type of damage the leg armor protects against (default is Physical).</param>
        /// <returns>A new instance of the <see cref="LegArmor"/> class.</returns>
        ArmorBase CreateLegArmor(string name, string description, int damageReduction, DamageType damageType = DamageType.Physical);
    }
}
