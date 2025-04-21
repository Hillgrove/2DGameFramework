using _2DGameFramework.Core;
using _2DGameFramework.Interfaces;

namespace _2DGameFramework.Domain.Items.Decorators
{
    /// <summary>
    /// Base decorator for any IDamageSource (weapons, traps, etc.).
    /// </summary>
    public abstract class DamageSourceDecorator : IDamageSource, IItem
    {
        protected readonly IDamageSource _inner;
        
        public DamageType DamageType => _inner.DamageType;
        public virtual int BaseDamage => _inner.BaseDamage;
        public string Name => ((IItem)_inner).Name;
        public string Description => ((IItem)_inner).Description;

        protected DamageSourceDecorator(IDamageSource inner)
        {
            _inner = inner;
        }
    }
}
