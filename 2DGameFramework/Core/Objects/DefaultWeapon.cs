using _2DGameFramework.Core.Base;

namespace _2DGameFramework.Core.Objects
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
