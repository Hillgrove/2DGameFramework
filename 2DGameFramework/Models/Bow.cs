using _2DGameFramework.Models.Base;

namespace _2DGameFramework.Models
{
    public class Bow : WeaponBase
    {
        public Bow(string name, string? description, int hitdamage, int range) 
            : base(name, description, hitdamage, range, WeaponType.TwoHanded)
        {
        }
    }
}
