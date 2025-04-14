using _2DGameFramework.Models.Base;
using System;

namespace _2DGameFramework.Models
{
    public class WeaponBase : WorldObject
    {
        public WeaponBase(string name, WeaponType weaponType, int range, int hitdamage, string? description = null)
            : base(name, description, position: null, isLootable: false, isRemovable: false)
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
