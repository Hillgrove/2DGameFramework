using _2DGameFramework.Core;
using _2DGameFramework.Interfaces;

namespace _2DGameFramework.Domain.Items.Decorators
{
    public abstract class WeaponDecorator : DamageSourceDecorator, IWeapon
    {
        protected readonly IWeapon _innerWeapon;

        public int Range => _innerWeapon.Range;

        public WeaponType WeaponType => _innerWeapon.WeaponType;


        public WeaponDecorator(IWeapon inner) : base(inner)
        {
            _innerWeapon = inner;
        }


    }
}
