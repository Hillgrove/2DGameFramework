using _2DGameFramework.Models.Base;

namespace _2DGameFramework.Models
{
    public class ArmorBase : WorldObject
    {
        public ArmorBase(string name, ItemSlot itemSlot, int damageReduction, DamageType damageType, string? description = null)
            : base(name, description, position: null, isLootable: false, isRemovable: false)
        {
            ItemSlot = itemSlot;
            DamageReduction = damageReduction;
            DamageType = damageType;
        }

        public ItemSlot ItemSlot { get; }
        public int DamageReduction { get; }
        public DamageType DamageType { get; }


        // TODO: Could be a possibility if I want to make armor that takes up several slots like a chainhauberk (torso, legs)
        //public IReadOnlyCollection<ItemSlot> RequiredSlots { get; init; } = new[] { ItemSlot.Head };

    }
}
