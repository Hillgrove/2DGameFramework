namespace _2DGameFramework.Models.Base
{
    /// <summary>
    /// Serves as the abstract base for all armor items, defining the equipment slot,
    /// damage reduction amount, and type of damage this armor mitigates.
    /// </summary>
    public abstract class ArmorBase : ItemBase
    {
        public ItemSlot ItemSlot { get; }
        public int DamageReduction { get; }
        public DamageType DamageType { get; }

        protected ArmorBase(string name, string? description, ItemSlot itemSlot, int damageReduction, DamageType damageType) 
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
        //public IReadOnlyCollection<ItemSlot> RequiredSlots { get; init; } = new[] { ItemSlot.Head };
    }
}
