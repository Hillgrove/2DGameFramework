using _2DGameFramework.Interfaces;

namespace _2DGameFramework.Domain.Combat
{
    /// <summary>
    /// Wraps an IDamageSource into an IAttackAction using the combat service.
    /// </summary>
    public class DamageSourceAttack : IAttackAction
    {
        private readonly IDamageSource _source;
        private readonly ICombatService _combat;

        public DamageSourceAttack(IDamageSource source, ICombatService combat)
        {
            _source = source;
            _combat = combat;
        }

        public void Execute(ICreature attacker, ICreature target)
        {
            _combat.AttackWithSource(attacker, target, _source);
        }
    }
}
