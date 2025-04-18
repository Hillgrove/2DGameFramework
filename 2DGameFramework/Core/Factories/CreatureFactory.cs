using _2DGameFramework.Combat;
using _2DGameFramework.Core.Creatures;
using _2DGameFramework.Logging;
using _2DGameFramework.Services;

namespace _2DGameFramework.Core.Factories
{
    /// <summary>
    /// Concrete factory for creating Creature instances.
    /// </summary>
    public class CreatureFactory : ICreatureFactory
    {
        private readonly ILogger _logger;
        private readonly IInventory _inventory;
        private readonly IDamageCalculator _damageCalculator;

        /// <summary>
        /// Initializes a new instance of the <see cref="CreatureFactory"/> class.
        /// </summary>
        public CreatureFactory(
            ILogger logger,
            IInventory inventory,
            IDamageCalculator damageCalculator)
        {
            _logger = logger;
            _inventory = inventory;
            _damageCalculator = damageCalculator;
        }

        /// <inheritdoc/>
        public Creature Create(string name, Position position, int hitpoints, string? description = null)
        {
            return new Creature(name, description, hitpoints, position, _inventory, _logger, _damageCalculator);
        }
    }
}
