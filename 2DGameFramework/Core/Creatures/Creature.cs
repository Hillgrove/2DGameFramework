using _2DGameFramework.Combat;
using _2DGameFramework.Core.Interfaces;
using _2DGameFramework.Core.Objects;
using _2DGameFramework.Logging;
using _2DGameFramework.Services;
using System.Diagnostics;

namespace _2DGameFramework.Core.Creatures
{
    /// <summary>
    /// Represents a creature in the world that can move, attack, receive damage, heal, and loot items.
    /// </summary>
    public class Creature : WorldObject, IPositionable, ICombatStats
    {
        public int Hitpoints { get; private set; }
        public Position Position { get; private set; }

        private readonly int _maxhitpoints;
        private readonly IDamageCalculator _damageCalculator;
        private readonly IInventory _inventory;
        
        private readonly ILogger _logger;

        /// <summary>
        /// Initializes a new instance of the <see cref="Creature"/> class.
        /// </summary>
        /// <param name="name">The name of the creature.</param>
        /// <param name="description">An optional description of the creature.</param>
        /// <param name="hitpoints">The starting and maximum hit points of the creature.</param>
        /// <param name="startPosition">The initial position of the creature in the world.</param>
        /// <param name="logger">The logger used to record game events.</param>
        public Creature(
            string name, 
            string? description, 
            int hitpoints, 
            Position startPosition, 
            IInventory inventory,
            
            ILogger logger,
            IDamageCalculator damageCalculator)
            : base(name, description)
        {
            Hitpoints = hitpoints;
            _maxhitpoints = hitpoints;
            Position = startPosition;
            _inventory = inventory;
            
            _logger = logger;
            _damageCalculator = damageCalculator;
        }

        /// <summary>
        /// Gets the list of items that this creature can use from its inventory.
        /// </summary>
        /// <returns>A sequence of <see cref="IUsable"/> items.</returns>
        public IEnumerable<IUsable> GetUsables() => _inventory.GetUsables();

        /// <summary>
        /// Attacks the specified target creature, dealing damage based on equipped weapons.
        /// </summary>
        /// <param name="target">The creature to attack.</param>
        public void Attack(Creature target)
        {
            int damage = _damageCalculator.CalculateDamage(this, target);

            _logger.Log(
                TraceEventType.Information,
                LogCategory.Combat,
                $"{Name} is attacking {target.Name} with total damage {damage}"
            );

            target.ReceiveDamage(damage);
        }

        /// <summary>
        /// Applies incoming damage to this creature, reducing hit points by the net amount after defense.
        /// Logs if the creature dies when hit points reach zero or below.
        /// </summary>
        /// <param name="hitdamage">The raw damage attempted against this creature.</param>
        public void ReceiveDamage(int hitdamage)
        {
            int damageReduction = GetTotalDamageReduction();
            int actualDamage = Math.Max(0, hitdamage - damageReduction);
            Hitpoints -= actualDamage;

            _logger.Log(
                TraceEventType.Information,
                LogCategory.Combat,
                $"{Name} received {actualDamage} damage after {damageReduction} reduction. HP now {Hitpoints}");

            if (Hitpoints <= 0)
            {
                _logger.Log(TraceEventType.Critical, LogCategory.Combat, $"{Name} has died.");
            }
        }

        /// <summary>
        /// Heals the creature by the specified amount, not exceeding its maximum hit points.
        /// </summary>
        /// <param name="amount">The amount of hit points to restore.</param>
        public void Heal(int amount)
        {
            int before = Hitpoints;
            Hitpoints = Math.Min(Hitpoints + amount, _maxhitpoints);
            int actualHealed = Hitpoints - before;

            _logger.Log(
                TraceEventType.Information,
                LogCategory.Combat,
                $"{Name} healed for {actualHealed}. HP now {Hitpoints}");
        }

        /// <summary>
        /// Retrieves and processes items from the given loot source. 
        /// The inventory service automatically handles equipping or storing them.
        /// If the source is an <see cref="ItemWrapper"/>, it is removed from the world.
        /// </summary>
        /// <param name="source">The loot source to retrieve items from.</param>
        /// <param name="world">The world instance for potential object removal.</param>
        public void Loot(ILootSource source, World world)
        {
            // Ensure it’s a valid, lootable EnvironmentObject
            if (source is not EnvironmentObject container || source is not (Container or ItemWrapper))
            {
                _logger.Log(
                    TraceEventType.Warning,
                    LogCategory.Inventory,
                    $"{Name} attempted to loot an invalid source.");
                return;
            }

            if (!container.IsLootable)
            {
                _logger.Log(
                    TraceEventType.Information,
                    LogCategory.Inventory,
                    $"{Name} attempted to loot '{container.Name}', but it is currently not lootable.");
                return;
            }

            // Grab and process loot
            var loot = source.GetLoot();
            _inventory.ProcessLoot(loot);

            // Only remove from world if it was an ItemWrapper
            if (container is ItemWrapper)
            {
                world.RemoveObject(container);

                _logger.Log(
                    TraceEventType.Information,
                    LogCategory.Inventory,
                    $"{container.Name} (ItemWrapper) removed from world after looting.");
            }
        }

        /// <summary>
        /// Uses the specified world object if it implements <see cref="IUsable"/>, otherwise logs a warning.
        /// </summary>
        /// <param name="item">The world object to use.</param>
        public void UseItem(WorldObject item)
        {
            if (item is IUsable usable)
            {
                usable.UseOn(this);
            }

            else
            {
                _logger.Log(
                    TraceEventType.Information,
                    LogCategory.Inventory,
                    $"{item.Name} cannot be used by {Name}.");
            }
        }

        /// <summary>
        /// Moves the creature by the given delta, clamped to the world boundaries, and logs the attempt.
        /// </summary>
        /// <param name="dx">The change in the X coordinate.</param>
        /// <param name="dy">The change in the Y coordinate.</param>
        /// <param name="world">The world instance used for boundary limits.</param>
        public void MoveBy(int dx, int dy, World world)
        {
            var from = Position;
            Position = Position with
            {
                X = Math.Clamp(from.X + dx, 0, world.WorldWidth),
                Y = Math.Clamp(from.Y + dy, 0, world.WorldHeight)
            };

            _logger.Log(
                TraceEventType.Information,
                LogCategory.Game,
                $"{Name} attempted move from {from} to ({from.X + dx},{from.Y + dy}), clamped to {Position}");
        }

        /// <summary>
        /// Returns a string representation of the creature’s current state, including position and hit points.
        /// </summary>
        /// <returns>A formatted string describing this creature.</returns>
        public override string ToString() =>
            $"{Name} at {Position} ({Hitpoints}/{_maxhitpoints} HP)";

        /// <summary>
        /// Calculates the total base damage from all equipped attack items.
        /// </summary>
        /// <returns>The combined hit points of all equipped attack sources.</returns>
        public int GetTotalBaseDamage() => _inventory.GetTotalBaseDamage();

        /// <summary>
        /// Calculates the total damage reduction from all equipped defense items.
        /// </summary>
        /// <returns>The combined damage reduction value of all equipped defense sources.</returns>
        public int GetTotalDamageReduction() => _inventory.GetTotalDamageReduction();

        /// <summary>
        /// Equips a new attack item to this creature’s inventory.
        /// </summary>
        /// <param name="weapon">The attack item to equip.</param>
        public void EquipWeapon(IAttackSource weapon) => _inventory.EquipAttackItem(weapon);

        /// <summary>
        /// Equips a new defense item to this creature’s inventory.
        /// </summary>
        /// <param name="armor">The defense item to equip.</param>
        public void EquipArmor(IDefenseSource armor) => _inventory.EquipDefenseItem(armor);
    }
}
