using _2DGameFramework.Core;
using _2DGameFramework.Domain.Items.Base;

namespace _2DGameFramework.Domain.Items.Defaults
{
    public class DefaultArmor : ArmorBase
    {
        public DefaultArmor(
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
