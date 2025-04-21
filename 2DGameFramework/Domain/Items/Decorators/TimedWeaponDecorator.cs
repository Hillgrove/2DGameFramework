using _2DGameFramework.Interfaces;

namespace _2DGameFramework.Domain.Items.Decorators
{
    /// <summary>
    /// Buffs a weapon’s BaseDamage via the provided modifier for a fixed number of attacks.
    /// Exposes full IWeapon surface (Range, WeaponType, Name, Description).
    /// </summary>
    public class TimedWeaponDecorator : WeaponDecorator
    {
        private int _remainingUses;
        private readonly Func<int, int> _modifier;

        /// <param name="inner">The underlying weapon to buff.</param>
        /// <param name="modifier">Function mapping inner.BaseDamage to modified damage.</param>
        /// <param name="uses">Number of attacks this buff applies to before expiring.</param>
        public TimedWeaponDecorator(
            IWeapon inner,
            Func<int, int> modifier,
            int uses)
                : base(inner)
        {
            _modifier = modifier;
            _remainingUses = uses;
        }

        public override int BaseDamage
        {
            get
            {
                if (_remainingUses > 0)
                {
                    _remainingUses--;
                    return _modifier(_innerWeapon.BaseDamage);
                }

                return _innerWeapon.BaseDamage;
            }
        }
    }
}
