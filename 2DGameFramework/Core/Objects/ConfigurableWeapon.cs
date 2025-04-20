using _2DGameFramework.Core.Base;

namespace _2DGameFramework.Core.Objects
{
    public class ConfigurableWeapon : WeaponBase
    {
        public ConfigurableWeapon(
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
