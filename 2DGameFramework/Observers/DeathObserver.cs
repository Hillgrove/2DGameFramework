using _2DGameFramework.Domain.Creatures;
using _2DGameFramework.Domain.Objects;
using _2DGameFramework.Domain.World;
using _2DGameFramework.Interfaces;
using _2DGameFramework.Logging;
using System.Diagnostics;


namespace _2DGameFramework.Observers
{
    /// <summary>
    /// Listens for creature deaths and spawns a lootable corpse container.
    /// </summary>
    public class DeathObserver
    {
        private readonly GameWorld _world;
        private readonly ILogger _logger;
        private readonly IInventoryService _inventory;

        public DeathObserver(IEnumerable<Creature> creatures, GameWorld world, ILogger logger, IInventoryService inventory)
        {
            _world = world;
            _logger = logger;
            _inventory = inventory;

            foreach (var c in creatures)
                c.OnDeath += HandleDeath;
        }

        /// <summary>Subscribe a single creature’s HealthChanged event.</summary>
        public void Subscribe(ICreature creature)
        {
            creature.OnDeath += HandleDeath;
        }

        private void HandleDeath(object? sender, DeathEventArgs e)
        {
            var dead = e.DeadCreature;

            // 1) Unsubscribe to observer
            dead.OnDeath -= HandleDeath;

            _logger.Log(TraceEventType.Information, LogCategory.Inventory,
                        $"Spawning corpse for {dead.Name}.");

            // 2) Create corpse
            var corpse = new Container(
                name: $"Corpse of {dead.Name}",
                description: "Lootable remains",
                position: dead.Position,
                logger: _logger,
                isLootable: true,
                isRemovable: true);

            // 3) Transfer items
            foreach (var item in _inventory.RemoveAllItems(dead))
                corpse.AddItem(item);

            // 4) Insert into world
            _world.AddObject(corpse);

            // 5) Remove creature
            _world.RemoveCreature(dead);
        }
    }
}
