using _2DGameFramework.Core.Base;

namespace _2DGameFramework.Core.Objects
{
    public class ConfigurableArmor : ArmorBase
    {
        public ConfigurableArmor(
            string name, 
            string description, 
            int damageReduction,
            ItemSlot itemSlot,
            DamageType damageType = DamageType.Physical)
            : base(name, description, damageReduction, itemSlot, damageType)
        {
        }
    }
}
