namespace _2DGameFramework.Models.Base
{
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

        public override string ToString() => 
            $"{base.ToString()} (Slot: {ItemSlot}, Reduces: {DamageReduction}, Type: {DamageType})";


        // TODO: Could be a possibility if I want to make armor that takes up several slots like a chainhauberk (torso, legs)
        //public IReadOnlyCollection<ItemSlot> RequiredSlots { get; init; } = new[] { ItemSlot.Head };
    }
}
