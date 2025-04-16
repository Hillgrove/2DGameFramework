using _2DGameFramework.Models.Base;

namespace _2DGameFramework.Models
{
    public class Sword : WeaponBase
    {
        public Sword(string name, int hitdamage, int range, string? description) 
            : base(name, description, hitdamage, range, WeaponType.OneHanded)
        {
        }
    }
}
