namespace _2DGameFramework.Models.Base
{
    public abstract class WeaponBase : ItemBase
    {
        public int HitDamage { get; }
        public int Range { get; }
        public WeaponType WeaponType { get; }

        protected WeaponBase(string name, string? description, int hitdamage, int range, WeaponType weaponType) 
            : base(name, description)
        {
            HitDamage = hitdamage;
            Range = range;
            WeaponType = weaponType;
        }
    }
}
