using _2DGameFramework.Core;
using _2DGameFramework.Domain.Combat;
using _2DGameFramework.Interfaces;
using _2DGameFramework.Services;

namespace _2DGameFramework.Domain.Creatures
{
    public class DefaultCreature : Creature
    {
        private readonly List<IAttackAction> _attackActions = new();

        public DefaultCreature(
            string name,
            string description,
            int hitpoints,
            Position startPosition,
            IStatsService statsService,
            ICombatService combatService,
            IMovementService movementService,
            IInventoryService inventoryService)
                : base(name, description, hitpoints, startPosition, statsService, combatService, movementService, inventoryService)
        {
        }


        /// <summary>
        /// Register an attack action (e.g. DamageSourceAttack or CompositeAttackAction).
        /// </summary>
        public void AddAttackAction(IAttackAction action)
            => _attackActions.Add(action);

        /// <summary>
        /// No extra checks by default before attacking.
        /// </summary>
        protected override void PreAttack(ICreature target)
        {
            base.PreAttack(target);
        }

        /// <summary>
        /// Uses any registered IAttackAction(s); falls back to simple CombatService otherwise.
        /// </summary>
        protected override void DoAttack(ICreature target)
        {
            if (_attackActions.Count > 0)
            {
                foreach (var action in _attackActions)
                    action.Execute(this, target);
            }
            else
            {
                // original behavior
                _combatService.Attack(this, target);
            }
        }

        /// <summary>
        /// No extra behavior by default after attacking.
        /// </summary>
        protected override void PostAttack(ICreature target)
        {
            base.PostAttack(target);
        }
    }
}
