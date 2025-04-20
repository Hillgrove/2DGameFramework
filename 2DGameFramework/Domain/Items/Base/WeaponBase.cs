using _2DGameFramework.Core;
using _2DGameFramework.Interfaces;

namespace _2DGameFramework.Domain.Items.Base
{
    /// <summary>
    /// Abstract base class for all weapons, defining their base damage value, range,
    /// weapon category, and damage type.
    /// </summary>
    public abstract class WeaponBase : ItemBase, IWeapon
    {
        public int BaseDamage { get; }
        public int Range { get; }
        public WeaponType WeaponType { get; }
        public DamageType DamageType { get; }

        protected WeaponBase(string name, string description, int hitdamage, int range, WeaponType weaponType, DamageType damageType = DamageType.Physical)
            : base(name, description)
        {
            BaseDamage = hitdamage;
            DamageType = damageType;
            Range = range;
            WeaponType = weaponType;
        }

        /// <summary>
        /// Returns a formatted string describing this weapon’s base information,
        /// including its damage, range, type, and damage category.
        /// </summary>
        public override string ToString() =>
            $"{base.ToString()} (Dmg: {BaseDamage}, DmgType: {DamageType},Range: {Range}, Type: {WeaponType})";
    }
}
