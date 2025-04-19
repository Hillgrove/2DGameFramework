using _2DGameFramework.Core.Base;

namespace _2DGameFramework.Core.Objects
{
    /// <summary>
    /// Represents a helmet that can reduce damage.
    /// </summary>
    public class Helmet : ArmorBase
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Helmet"/> class.
        /// </summary>
        /// <param name="name">The name of the helmet.</param>
        /// <param name="damageReduction">The damage reduction this helmet provides.</param>
        /// <param name="description">An optional description of the helmet.</param>
        public Helmet(string name, string description, int damageReduction, DamageType damageType = DamageType.Physical)
            : base(name, description, damageReduction, ItemSlot.head, damageType)
        {
        }
    }
}
