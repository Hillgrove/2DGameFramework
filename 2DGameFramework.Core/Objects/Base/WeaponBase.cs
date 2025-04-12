namespace _2DGameFramework.Objects.Base
{
    public abstract class WeaponBase : ItemBase
    {
        protected WeaponBase(string name, WeaponType weaponType, int range, int hitdamage, string? description = null, bool isLootable = true, Position? position = null) 
            : base(name, description, isLootable, position)
        {
            HitDamage = hitdamage;
            Range = range;
            WeaponType = weaponType;
        }

        public int HitDamage { get; }
        public int Range { get; }
        public WeaponType WeaponType { get; }
    }
}
