using _2DGameFramework.Core;
using _2DGameFramework.Interfaces;

namespace _2DGameFramework.Domain.Items.Base
{
    /// <summary>
    /// Serves as the abstract base for all armor items, defining the equipment slot,
    /// damage reduction amount, and type of damage this armor mitigates.
    /// </summary>
    public abstract class ArmorBase : ItemBase, IArmor
    {
        public ItemSlot ItemSlot { get; }
        public int DamageReduction { get; }
        public DamageType DamageType { get; }

        protected ArmorBase(string name, string description, int damageReduction, ItemSlot itemSlot, DamageType damageType = DamageType.Physical)
            : base(name, description)
        {
            ItemSlot = itemSlot;
            DamageReduction = damageReduction;
            DamageType = damageType;
        }

        /// <summary>
        /// Returns a textual representation of this armor, including its base item info,
        /// the slot it occupies, its damage reduction value, and damage type.
        /// </summary>
        /// <returns>A formatted string describing this armor.</returns>
        public override string ToString() =>
            $"{base.ToString()} (Slot: {ItemSlot}, Reduces: {DamageReduction}, Type: {DamageType})";


        // TODO: Could be a possibility if I want to make armor that takes up several slots like a chainhauberk (torso, legs)
        //public IReadOnlyCollection<ItemSlot> RequiredSlots { get; init; } = new[] { ItemSlot.torso, ItemSlot.legs };
    }
}
