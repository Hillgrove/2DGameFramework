using _2DGameFramework.Core.Base;

namespace _2DGameFramework.Core.Objects
{
    /// <summary>
    /// Represents leg armor (e.g., pants) that can reduce physical damage.
    /// </summary>
    public class LegArmor : ArmorBase
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="LegArmor"/> class.
        /// </summary>
        /// <param name="name">The name of the leg armor.</param>
        /// <param name="damageReduction">The damage reduction this leg armor provides.</param>
        /// <param name="description">An optional description of the leg armor.</param>
        public LegArmor(string name, string description, int damageReduction, DamageType damageType = DamageType.Physical)
            : base(name, description, damageReduction, ItemSlot.legs, damageType)
        {
        }
    }
}
