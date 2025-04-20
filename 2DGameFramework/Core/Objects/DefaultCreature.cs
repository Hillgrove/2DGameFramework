using _2DGameFramework.Core.Creatures;
using _2DGameFramework.Services;

namespace _2DGameFramework.Core.Objects
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
        /// No extra checks by default before attacking.
        /// </summary>
        protected override void PreAttack(ICreature target)
        {
            base.PreAttack(target);
        }

        /// <summary>
        /// Core attack logic: delegate to injected CombatService.
        /// </summary>
        protected override void DoAttack(ICreature target)
        {
            _combatService.Attack(this, target);
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
