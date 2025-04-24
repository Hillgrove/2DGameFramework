using _2DGameFramework.Core;
using _2DGameFramework.Domain.Creatures;
using _2DGameFramework.Interfaces;
using _2DGameFramework.Observers;
using _2DGameFramework.Services;

namespace _2DGameFramework.Factories
{
    /// <summary>
    /// Concrete factory for creating Creature instances.
    /// </summary>
    internal class CreatureFactory : ICreatureFactory
    {
        private readonly IStatsService _statsService;
        private readonly ICombatService _combatService;
        private readonly IMovementService _movementService;
        private readonly IInventoryService _inventoryService;

        private readonly HealthObserver _healthObserver;
        private readonly DeathObserver _deathObserver;

        /// <summary>
        /// Initializes a new instance of the <see cref="CreatureFactory"/> class.
        /// </summary>
        public CreatureFactory(
            IStatsService statsService,
            ICombatService combatService,
            IMovementService movementService,
            IInventoryService inventoryService,
            HealthObserver healthObserver,
            DeathObserver deathObserver)
        {
            _statsService = statsService;
            _combatService = combatService;
            _movementService = movementService;
            _inventoryService = inventoryService;
            _healthObserver = healthObserver;
            _deathObserver = deathObserver;
        }

        /// <inheritdoc/>
        public Creature Create(string name, string description, int hitpoints, Position position, double autoHealThreshold)
        {
            var creature = new DefaultCreature(
                name,
                description,
                hitpoints,
                position,
                _statsService,
                _combatService,
                _movementService,
                _inventoryService);

            _healthObserver.Subscribe(creature, autoHealThreshold);
            _deathObserver.Subscribe(creature);

            return creature;
        }
    }
}
