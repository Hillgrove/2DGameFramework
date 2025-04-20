using _2DGameFramework.Core;
using _2DGameFramework.Domain.Items.Base;

namespace _2DGameFramework.Domain.Items.Defaults
{
    public class DefaultWeapon : WeaponBase
    {
        public DefaultWeapon(
            string name,
            string description,
            int hitdamage,
            int range,
            WeaponType weaponType,
            DamageType damagetype = DamageType.Physical)
                : base(name, description, hitdamage, range, weaponType, damagetype)
        {
        }
    }
}
