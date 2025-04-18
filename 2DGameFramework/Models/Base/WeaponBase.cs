using _2DGameFramework.Interfaces;

namespace _2DGameFramework.Models.Base
{
    /// <summary>
    /// Abstract base class for all weapons, defining their base damage value, range,
    /// weapon category, and damage type.
    /// </summary>
    public abstract class WeaponBase : ItemBase, IDamageSource
    {
        public int BaseDamage { get; }
        public int Range { get; }
        public WeaponType WeaponType { get; }
        public DamageType DamageType { get; } = DamageType.Physical;

        protected WeaponBase(string name, string? description, int hitdamage, DamageType damageType, int range, WeaponType weaponType) 
            : base(name, description)
        {
            BaseDamage      = hitdamage;
            DamageType  = damageType;
            Range       = range;
            WeaponType  = weaponType;
        }

        protected WeaponBase(string name, string? description, int hitdamage, int range, WeaponType weaponType)
            : base(name, description)
        {
            BaseDamage      = hitdamage;
            Range       = range;
            WeaponType  = weaponType;
        }

        /// <summary>
        /// Returns a formatted string describing this weapon’s base information,
        /// including its damage, range, type, and damage category.
        /// </summary>
        public override string ToString() =>
            $"{base.ToString()} (Dmg: {BaseDamage}, DmgType: {DamageType},Range: {Range}, Type: {WeaponType})";

    }
}
