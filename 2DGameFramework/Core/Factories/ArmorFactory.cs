using _2DGameFramework.Core.Base;
using _2DGameFramework.Core.Objects;

namespace _2DGameFramework.Core.Factories
{
    /// <summary>
    /// Factory for creating various types of armor, including helmets, chestplates, boots, and leg armor.
    /// </summary>
    public class ArmorFactory : IArmorFactory
    {
        ///<inheritdoc/>
        public ArmorBase CreateHelmet(string name, string description, int damageReduction, DamageType damageType = DamageType.Physical)
        {
            return new Helmet(name, description, damageReduction, damageType);
        }

        ///<inheritdoc/>
        public ArmorBase CreateChestArmor(string name, string description, int damageReduction, DamageType damageType = DamageType.Physical)
        {
            return new ChestArmor(name, description, damageReduction, damageType);
        }

        ///<inheritdoc/>
        public ArmorBase CreateBoots(string name, string description, int damageReduction, DamageType damageType = DamageType.Physical)
        {
            return new Boots(name, description, damageReduction, damageType);
        }
        ///<inheritdoc/>
        public ArmorBase CreateLegArmor(string name, string description, int damageReduction, DamageType damageType = DamageType.Physical)
        {
            return new LegArmor(name, description, damageReduction, damageType);
        }
    }
}
