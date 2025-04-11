namespace _2DGameFramework.Core.Objects
{
    public class DefenseItem : WorldObject
    {
        public int DamageReduction { get; init; }
        public ItemType ItemType { get; init; }
        public Element DefenseType { get; init; }
        public ItemSlot ItemSlot { get; init; }
        
        // TODO: Could be a possibility if I want to make armor that takes up several slots
        //public IReadOnlyCollection<ItemSlot> RequiredSlots { get; init; } = new[] { ItemSlot.Head };

    }
}
