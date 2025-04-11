namespace _2DGameFramework.Core.Objects
{
    public class AttackItem : WorldObject
    {
        public int HitDamage { get; init; }
        public int Range { get; init; }
        public ItemType ItemType { get; init; }
        public Element DamageType { get; init; }
        public int SlotUsage { get; init; } = 1;
    }
}
