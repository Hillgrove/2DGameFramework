using _2DGameFramework.Interfaces;
using _2DGameFramework.Models.Base;
using _2DGameFramework.Models.Core;
using _2DGameFramework.Models.Objects;
using System.Diagnostics;

namespace _2DGameFramework.Models.Creatures
{
    /// <summary>
    /// Represents a creature in the world that can move, attack, receive damage, heal, and loot items.
    /// </summary>
    public class Creature : WorldObject, IPositionable
    {
        public int Hitpoints { get; private set; }
        public Position Position { get; private set; }

        private readonly int _maxhitpoints;
        private readonly List<WeaponBase> _attackItems = new();
        private readonly List<ArmorBase> _defenseItems = new();
        private readonly List<IUsable> _usables = new();
        private readonly ILogger _logger;

        /// <summary>
        /// Initializes a new instance of the <see cref="Creature"/> class.
        /// </summary>
        /// <param name="name">The name of the creature.</param>
        /// <param name="description">An optional description of the creature.</param>
        /// <param name="hitpoints">The starting and maximum hit points of the creature.</param>
        /// <param name="startPosition">The initial position of the creature in the world.</param>
        /// <param name="logger">The logger used to record game events.</param>
        public Creature(string name, string? description, int hitpoints, Position startPosition, ILogger logger)
            : base(name, description)
        {
            Hitpoints = hitpoints;
            _maxhitpoints = hitpoints;
            Position = startPosition;

            _logger = logger;
        }

        /// <summary>
        /// Gets the list of items that this creature can use.
        /// </summary>
        /// <returns>A sequence of <see cref="IUsable"/> items.</returns>
        public IEnumerable<IUsable> GetUsables() => _usables;

        /// <summary>
        /// Attacks the specified target creature, dealing damage based on equipped weapons.
        /// </summary>
        /// <param name="target">The creature to attack.</param>
        public void Attack(Creature target)
        {
            int damage = TotalDamage();

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
            int damageReduction = _defenseItems.Sum(i => i.DamageReduction);
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
        /// Retrieves items from the given loot source, equips or stores them,
        /// and if the source is an <see cref="ItemWrapper"/>, removes it from the world.
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

            // Grab and equip or store all items
            var loot = source.GetLoot();
            EquipLoot(loot);

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


        #region Private Functions
        private void EquipLoot(IEnumerable<ItemBase> loot)
        {
            foreach (var item in loot)
            {
                EquipSingleItem(item);
            }
        }

        private void EquipSingleItem(ItemBase item)
        {
            switch (item)
            {
                case WeaponBase weapon:
                    EquipWeapon(weapon);
                    break;

                case ArmorBase armor:
                    EquipArmor(armor);
                    break;

                case IUsable usable:
                    AddUsable(usable);
                    break;

                default:
                    _logger.Log(
                        TraceEventType.Warning,
                        LogCategory.Inventory,
                        $"{Name} ignored item '{item.Name}' – unsupported item type.");
                    break;
            }
        }

        private void EquipWeapon(WeaponBase weapon)
        {
            _attackItems.Add(weapon);

            _logger.Log(
                TraceEventType.Information,
                LogCategory.Inventory,
                $"{Name} equipped weapon: {weapon.Name}");
        }

        private void EquipArmor(ArmorBase armor)
        {
            _defenseItems.Add(armor);

            _logger.Log(
                TraceEventType.Information,
                LogCategory.Inventory,
                $"{Name} equipped armor: {armor.Name}");
        }

        private void AddUsable(IUsable usable)
        {
            _usables.Add(usable);

            _logger.Log(
                TraceEventType.Information,
                LogCategory.Inventory,
                $"{Name} added usable item to backpack: {((ItemBase)usable).Name}");
        }

        private int TotalDamage()
        {
            // If no weapons equipped, do 1 HP damage with “fists”
            return _attackItems.Count != 0
                ? _attackItems.Sum(i => i.BaseDamage)
                : 1;
        }
        #endregion
    }
}
