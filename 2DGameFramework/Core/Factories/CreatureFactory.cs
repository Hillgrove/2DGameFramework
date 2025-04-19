using _2DGameFramework.Core.Creatures;
using _2DGameFramework.Services;

namespace _2DGameFramework.Core.Factories
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

        /// <summary>
        /// Initializes a new instance of the <see cref="CreatureFactory"/> class.
        /// </summary>
        public CreatureFactory(
            IStatsService statsService,
            ICombatService combatService,
            IMovementService movementService,
            IInventoryService inventoryService)
        {
            _statsService = statsService;
            _combatService = combatService;
            _movementService = movementService;
            _inventoryService = inventoryService;
        }

        /// <inheritdoc/>
        public Creature Create(string name, string description, int hitpoints, Position position)
        {
            return new Creature(
                name,
                description,
                hitpoints,
                position,
                _statsService,
                _combatService,
                _movementService,
                _inventoryService);
        }
    }
}
