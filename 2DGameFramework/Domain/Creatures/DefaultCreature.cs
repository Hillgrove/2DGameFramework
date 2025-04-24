using _2DGameFramework.Core;
using _2DGameFramework.Domain.Combat;
using _2DGameFramework.Interfaces;
using _2DGameFramework.Services;

namespace _2DGameFramework.Domain.Creatures
{
    public class DefaultCreature : Creature
    {
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
        /// Shortcut to register an attack under a convenient key.
        /// </summary>
        public void AddAttackAction(string key, IAttackAction action)
        {
            RegisterAttackAction(key, action);
        }

        /// <summary>
        /// Pre-attack hook; no-op by default.
        /// </summary>
        protected override void PreAttack(IAttackAction action, ICreature target)
        {
            // e.g. consume stamina or check range
        }

        /// <summary>
        /// Executes exactly the chosen IAttackAction.
        /// </summary>
        protected override void DoAttack(IAttackAction action, ICreature target)
        {
            action.Execute(this, target);
        }

        /// <summary>
        /// Post-attack hook; no-op by default.
        /// </summary>
        protected override void PostAttack(IAttackAction action, ICreature target)
        {
            // e.g. apply status effects
        }
    }
}
