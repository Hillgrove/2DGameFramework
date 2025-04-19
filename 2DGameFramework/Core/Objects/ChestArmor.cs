using _2DGameFramework.Core.Base;

namespace _2DGameFramework.Core.Objects
{
    /// <summary>
    /// Represents a chestplate that can reduce damage.
    /// </summary>
    public class ChestArmor : ArmorBase
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ChestArmor"/> class.
        /// </summary>
        /// <param name="name">The name of the chestplate.</param>
        /// <param name="damageReduction">The damage reduction this chestplate provides.</param>
        /// <param name="description">An optional description of the chestplate.</param>
        public ChestArmor(string name, string description, int damageReduction, DamageType damageType = DamageType.Physical)
            : base(name, description, damageReduction, ItemSlot.torso, damageType)
        {
        }
    }
}
