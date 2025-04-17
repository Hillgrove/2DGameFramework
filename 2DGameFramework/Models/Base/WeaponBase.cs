namespace _2DGameFramework.Models.Base
{
    /// <summary>
    /// Abstract base class for all weapons, defining their damage value, attack range,
    /// and weapon category.
    /// </summary>
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

        /// <summary>
        /// Returns a formatted string describing this weapon’s base information,
        /// including its damage, range, and type.
        /// </summary>
        /// <returns>A string representation of the weapon.</returns>
        public override string ToString() =>
            $"{base.ToString()} (Dmg: {HitDamage}, Range: {Range}, Type: {WeaponType})";

    }
}
