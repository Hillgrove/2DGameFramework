using _2DGameFramework.Core.Base;

namespace _2DGameFramework.Core.Objects
{
    /// <summary>
    /// Represents a pair of boots that can reduce damage.
    /// </summary>
    public class Boots : ArmorBase
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Boots"/> class.
        /// </summary>
        /// <param name="name">The name of the boots.</param>
        /// <param name="damageReduction">The damage reduction this boots provide.</param>
        /// <param name="description">An optional description of the boots.</param>
        public Boots(string name, string description, int damageReduction, DamageType damageType = DamageType.Physical) 
            : base(name, description, damageReduction, ItemSlot.feet, damageType)
        {
        }
    }
}